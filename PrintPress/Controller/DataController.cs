using Microsoft.Data.SqlClient;
using PrintPress.Controller.Data;
using PrintPress.Controller.Enum;
using PrintPress.Data;
using System.Data;
using System.Text;

namespace PrintPress.Controller
{
    /// <summary>
    /// Abstract Singleton base class, containing generic methods to interact with all PrintPress databases
    /// </summary>
    /// <typeparam name="T1"> DataController specification type, used to define singleton instance type </typeparam>
    public abstract class DataController<T1>
        where T1 : DataController<T1>, new()
    {
        #region Singleton implementation

        protected static bool _initialised = false;
        protected static T1? _instance;

        protected abstract IDatabaseSchema Tables { get; }

        public static T1 Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T1();
                }
                return _instance;
            }
        }

        public abstract void Initialise();
        protected void Initialise(string className)
        {
            _className = className;
            _defaultConnectionBuilder.InitialCatalog = DatabaseName;
            VerifyDatabase();
            VerifyAll();
            _initialised = true;
        }

        #endregion
        #region Abstract database functionality

        private const string _connectionStringBase = "Server=MAINFRAME\\SQLEXPRESS;Integrated security=SSPI;Encrypt=False;TrustServerCertificate=True";
        private const string _rootPath = "C:\\Program Files\\PrintPress\\";
        protected SqlConnectionStringBuilder _defaultConnectionBuilder = new SqlConnectionStringBuilder(_connectionStringBase);
        private string _className = string.Empty;

        private string DatabaseName
        {
            get
            {
                return _className + "DB";
            }
        }
        private string DatabasePath 
        { 
            get
            {
                return @$"{_rootPath}{DatabaseName}Data.mdf";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="C"></typeparam>
        /// <param name="nonQuery"></param>
        /// <param name="overrideDb"></param>
        /// <returns></returns>
        protected virtual bool ExecuteNonQuery<C>(SqlCommandData<C> nonQuery, string? overrideDb = null)
        {
            return Execute(nonQuery, out _, command => { command.ExecuteNonQuery(); return []; }, overrideDb);
        }

        protected bool ExecuteQuery<C>(SqlCommandData<C> query, out C[] values, string? overrideDb = null)
        {
            return Execute(query, out values,
                command =>
                {
                    List<C> valsList = new List<C>();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            valsList.Add(query.readerFunc(reader));
                        }
                    }
                    return valsList.ToArray();
                },
                overrideDb);
        }

        private bool Execute<C>(SqlCommandData<C> query, out C[] values, Func<SqlCommand, C[]> executeFunc, string? overrideDb = null)
        {
            values = [];

            SqlConnectionStringBuilder localConnectionBuilder = GetConnectionBuilder(overrideDb);

            using (SqlConnection connection = new SqlConnection(localConnectionBuilder.ConnectionString))
            {
                SqlCommand command = new SqlCommand(query.queryString, connection);
                foreach (var param in query.sqlParams)
                {
                    command.Parameters.Add(param);
                }

                try
                {
                    connection.Open();
                    values = executeFunc(command);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }

                return true;
            }
        }

        private SqlConnectionStringBuilder GetConnectionBuilder(string? overrideDb = null)
        {
            SqlConnectionStringBuilder localConnectionBuilder = _defaultConnectionBuilder;
            if (overrideDb != null)
            {
                localConnectionBuilder = new SqlConnectionStringBuilder(_connectionStringBase);
                localConnectionBuilder.InitialCatalog = overrideDb;
            }
            return localConnectionBuilder;
        }

        protected bool VerifyDatabase()
        {
            string dbName = _defaultConnectionBuilder.InitialCatalog;
            
            if (DatabaseExists()) return true;
            if (TryCreateDatabase()) return true;

            return false;
        }

        private bool DatabaseExists()
        {
            SqlCommandData<int> commandData = new SqlCommandData<int>()
            {
                queryString = $"IF EXISTS" +
                $"(SELECT 1 FROM sys.databases WHERE name = @databaseName AND state_desc = 'ONLINE') " +
                $"BEGIN SELECT 1 END " +
                $"ELSE BEGIN SELECT 0 END",
                sqlParams = [new SqlParameter("@databaseName", SqlDbType.NVarChar) { Value = DatabaseName }],
                readerFunc = reader => reader.GetInt32(0)
            };

            if (GetSingleResult(commandData, out int result) == CommandReturnState.NOTFOUND)
            {
                return false;
            }

            return Convert.ToBoolean(result);
        }

        private bool TryCreateDatabase()
        {
            // SQL to drop and recreate the database
            string nonQuery = @$"
                IF EXISTS (
                    SELECT 1 
                    FROM sys.databases 
                    WHERE name = '{DatabaseName}'
                    AND state_desc = 'ONLINE')

                BEGIN
                    DROP DATABASE [{DatabaseName}];
                END
        
                CREATE DATABASE [{DatabaseName}] ON PRIMARY 
                (
                    NAME = '{_className + "_Data"}',
                    FILENAME = '{DatabasePath}',
                    SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%
                )
                LOG ON 
                (
                    NAME = '{_className}{"_DBLog"}',
                    FILENAME = '{_rootPath}{_className}{"_DBLog.ldf"}',
                    SIZE = 1MB, MAXSIZE = 5MB, FILEGROWTH = 10%
                )";

            // SQL Command Data
            SqlCommandData<object> nonQueryData = new SqlCommandData<object>
            {
                queryString = nonQuery,
                sqlParams = []
            };

            // Execute the SQL command
            return ExecuteNonQuery(nonQueryData, "master");
        }

        private void VerifyAll()
        {
            foreach (TableSchema table in Tables.AllTables)
            {
                VerifyTable(table);
            }
        }

        protected bool VerifyTable(TableSchema schema)
        {
            SqlCommandData<int> query = new SqlCommandData<int>()
            {
                queryString = $"IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{schema.Name}') " +
                              "BEGIN SELECT 1 END ELSE BEGIN SELECT 0 END",
                sqlParams = [],
                readerFunc = reader => reader.GetInt32(0)
            };

            bool readSuccess = ExecuteQuery(query, out int[] tableExists);

            if (!readSuccess || tableExists.Length < 1)
            {
                return false;
            }
            if (Convert.ToBoolean(tableExists[0]))
            {
                return true;
            }
            SqlCommandData<object> queryData = new SqlCommandData<object>(
                $"IF OBJECT_ID('{schema.Name}', 'U') IS NOT NULL DROP TABLE {schema.Name}; " +
                $"CREATE TABLE {schema.Name} {DataTools.SchemaString(schema.Columns)}",
                [],
                null);
            if (ExecuteNonQuery(queryData))
            {
                return true;
            }
              
            return false;
        }

        protected bool AddItemGetId(TableSchema schema, object[] values, out int autoId)
        {
            int itemCount = values.Length;

            string[] paramPh = DataTools.GenerateParameterPlaceholders(itemCount);
            string valueParams = DataTools.SchemaValuesString(paramPh);

            SqlParameter[] sqlParams = new SqlParameter[itemCount];
            for (int i = 0; i < itemCount; i++)
            {
                sqlParams[i] = new SqlParameter(paramPh[i], schema.Columns[i + 1][1]) { Value = values[i] };
            }

            SqlCommandData<int> commandData = new SqlCommandData<int>(
                @$"INSERT INTO {schema.Name} OUTPUT Inserted.{schema.Columns[0][0]} VALUES {valueParams}",
                sqlParams,
                reader => reader.GetInt32(0));

            if (GetSingleResult(commandData, schema, out autoId) == CommandReturnState.FOUND)
            {
                return true;
            }
            return false;
        }

        protected bool AddItem(TableSchema schema, object[] values)
        {
            int itemCount = schema.Columns.Length;

            string[] paramPh = DataTools.GenerateParameterPlaceholders(schema.Columns.Length);
            string valueParams = DataTools.SchemaValuesString(paramPh);

            SqlParameter[] sqlParams = new SqlParameter[itemCount];
            for (int i = 0; i < itemCount; i++)
            {
                sqlParams[i] = new SqlParameter(paramPh[i], schema.Columns[i]) { Value = values[i] };
            }

            SqlCommandData<Address> commandData = new SqlCommandData<Address>(
                @$"INSERT INTO {schema.Name} VALUES {valueParams}",
                sqlParams);

            return ExecuteNonQuery(commandData);
        }

        protected bool DeleteItem(TableSchema schema, string[] values)
        {
            int itemCount = schema.Columns.Length;

            string[] paramPh = DataTools.GenerateParameterPlaceholders(schema.Columns.Length);
            string valueParams = DataTools.SchemaConditionString(schema.Columns, paramPh);

            SqlParameter[] sqlParams = new SqlParameter[itemCount];
            for (int i = 0; i < itemCount; i++)
            {
                sqlParams[i] = new SqlParameter(paramPh[i], schema.Columns[i]) { Value = values[i] };
            }

            SqlCommandData<Address> commandData = new SqlCommandData<Address>(
                @$"DELETE FROM {schema.Name} WHERE {valueParams}",
                sqlParams);

            return ExecuteNonQuery(commandData);
        }

        protected CommandReturnState GetSingleResult<C>(SqlCommandData<C> commandData, TableSchema tableSchema, out C result) where C : new()
        {
            result = new C();

            if (!VerifyTable(tableSchema))
            {
                return CommandReturnState.FAILED;
            }

            return GetSingleResult(commandData, out result);
        }

        protected CommandReturnState GetSingleResult<C>(SqlCommandData<C> commandData, out C result)
        {
            result = default;

            bool readSuccess = ExecuteQuery(commandData, out C[] results);

            if (!readSuccess)
            {
                return CommandReturnState.FAILED;
            }
            else if (results.Length == 0)
            {
                return CommandReturnState.NOTFOUND;
            }

            result = results[0];
            return CommandReturnState.FOUND;
        }

        protected CommandReturnState GetMultiResult<C>(SqlCommandData<C> commandData, TableSchema tableSchema, out C[] results)
        {
            if (!VerifyTable(tableSchema))
            {
                results = [];
                return CommandReturnState.FAILED;
            }

            return GetMultiResult(commandData, out results);
        }
        protected CommandReturnState GetMultiResult<C>(SqlCommandData<C> commandData,  out C[] results)
        {
            bool readSuccess = ExecuteQuery(commandData, out results);

            if (!readSuccess)
            {
                return CommandReturnState.FAILED;
            }
            else if (results.Length == 0)
            {
                return CommandReturnState.NOTFOUND;
            }
            return CommandReturnState.FOUND;
        }

        public DataTable GetData(string sqlCommand)
        {
            SqlConnection connection = new SqlConnection(_defaultConnectionBuilder.ConnectionString);

            SqlCommand command = new SqlCommand(sqlCommand, connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            adapter.Fill(table);

            return table;
        }

        public CommandReturnState SendSql(string sqlString, out string result)
        {
            SqlCommandData<string> commandData = new SqlCommandData<string>(
                sqlString,
                [],
                reader => {
                    StringBuilder sb = new StringBuilder();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        switch (reader.GetDataTypeName(i))
                        {
                            case "int":
                                sb.Append(reader.GetInt32(i).ToString());
                                break;
                            case "nvarchar":
                                sb.Append(reader.GetString(i));
                                break;

                        }
                        sb.Append(", ");
                    }
                    sb.Remove(sb.Length - 2, 2);
                    return sb.ToString();
                });

            CommandReturnState resultState = GetMultiResult(commandData, out string[] results);

            switch (resultState)
            {
                case CommandReturnState.FAILED:
                case CommandReturnState.NOTFOUND:
                    result = string.Empty;
                    return resultState;
                default:
                    break;
            }

            StringBuilder resultBuilder = new StringBuilder(results[0]);
            for (int i = 1; i < results.Length; i++)
            {
                resultBuilder.Append(results[i]);
            }

            result = resultBuilder.ToString();
            return CommandReturnState.FOUND;
        }
        #endregion
    }
}
