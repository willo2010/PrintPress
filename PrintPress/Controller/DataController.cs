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

        /// <summary>
        /// Gets the singleton instance of the data controller.
        /// </summary>
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

        /// <summary>
        /// Abstract method to initialize the data controller, implemented in derived classes.
        /// </summary>
        public abstract void Initialise();

        /// <summary>
        /// Initializes the data controller with a specified class name and sets up the database connection.
        /// </summary>
        /// <param name="className">The name of the class to initialize.</param>
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

        /// <summary>
        /// Gets the name of the database associated with this controller.
        /// </summary>
        private string DatabaseName
        {
            get
            {
                return _className + "DB";
            }
        }

        /// <summary>
        /// Gets the file path for the database.
        /// </summary>
        private string DatabasePath
        {
            get
            {
                return @$"{_rootPath}{DatabaseName}Data.mdf";
            }
        }

        /// <summary>
        /// Executes a non-query SQL command on the database.
        /// </summary>
        /// <typeparam name="C">The type of the command result.</typeparam>
        /// <param name="nonQuery">The SQL command data to execute.</param>
        /// <param name="overrideDb">Optional override database name.</param>
        /// <returns>True if the command executed successfully, otherwise false.</returns>
        protected virtual bool ExecuteNonQuery<C>(SqlCommandData<C> nonQuery, string? overrideDb = null)
        {
            return Execute(nonQuery, out _, command => { command.ExecuteNonQuery(); return []; }, overrideDb);
        }

        /// <summary>
        /// Executes a query on the database and retrieves the results.
        /// </summary>
        /// <typeparam name="C">The type of the query result.</typeparam>
        /// <param name="query">The SQL command data to execute.</param>
        /// <param name="values">The results of the query.</param>
        /// <param name="overrideDb">Optional override database name.</param>
        /// <returns>True if the query executed successfully, otherwise false.</returns>
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

        /// <summary>
        /// Executes a SQL command on the database.
        /// </summary>
        /// <typeparam name="C">The type of the command result.</typeparam>
        /// <param name="query">The SQL command data to execute.</param>
        /// <param name="values">The results of the command.</param>
        /// <param name="executeFunc">The function to execute with the SQL command.</param>
        /// <param name="overrideDb">Optional override database name.</param>
        /// <returns>True if the command executed successfully, otherwise false.</returns>
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

        /// <summary>
        /// Retrieves a database connection string builder, optionally overriding the database name.
        /// </summary>
        /// <param name="overrideDb">Optional override database name.</param>
        /// <returns>A SQL connection string builder.</returns>
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

        /// <summary>
        /// Verifies the existence of the database and creates it if it does not exist.
        /// </summary>
        /// <returns>True if the database exists or was created successfully, otherwise false.</returns>
        protected bool VerifyDatabase()
        {
            string dbName = _defaultConnectionBuilder.InitialCatalog;

            if (DatabaseExists()) return true;
            if (TryCreateDatabase()) return true;

            return false;
        }

        /// <summary>
        /// Checks if the database exists on the server.
        /// </summary>
        /// <returns>True if the database exists, otherwise false.</returns>
        private bool DatabaseExists()
        {
            SqlCommandData<int> commandData = new SqlCommandData<int>()
            {
                queryString = $"IF EXISTS" +
                "(SELECT 1 FROM sys.databases WHERE name = @databaseName AND state_desc = 'ONLINE') " +
                "BEGIN SELECT 1 END " +
                "ELSE BEGIN SELECT 0 END",
                sqlParams = [new SqlParameter("@databaseName", SqlDbType.NVarChar) { Value = DatabaseName }],
                readerFunc = reader => reader.GetInt32(0)
            };

            if (GetSingleResult(commandData, out int result) == CommandReturnState.NOTFOUND)
            {
                return false;
            }

            return Convert.ToBoolean(result);
        }

        /// <summary>
        /// Attempts to create the database if it does not exist.
        /// </summary>
        /// <returns>True if the database was created successfully, otherwise false.</returns>
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

        /// <summary>
        /// Verifies all tables defined in the database schema.
        /// </summary>
        private void VerifyAll()
        {
            foreach (TableSchema table in Tables.AllTables)
            {
                VerifyTable(table);
            }
        }

        /// <summary>
        /// Verifies the existence of a specific table in the database.
        /// </summary>
        /// <param name="schema">The schema of the table to verify.</param>
        /// <returns>True if the table exists or was created successfully, otherwise false.</returns>
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

        /// <summary>
        /// Adds an item to a table and retrieves the auto-generated ID of the new row.
        /// </summary>
        /// <param name="schema">The schema of the table to add the item to.</param>
        /// <param name="values">The values to insert into the table.</param>
        /// <param name="autoId">The auto-generated ID of the new row.</param>
        /// <returns>True if the item was added successfully, otherwise false.</returns>
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

        /// <summary>
        /// Adds an item to a table.
        /// </summary>
        /// <param name="schema">The schema of the table to add the item to.</param>
        /// <param name="values">The values to insert into the table.</param>
        /// <returns>True if the item was added successfully, otherwise false.</returns>
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

        /// <summary>
        /// Deletes an item from a table.
        /// </summary>
        /// <param name="schema">The schema of the table to delete the item from.</param>
        /// <param name="values">The values identifying the row to delete.</param>
        /// <returns>True if the item was deleted successfully, otherwise false.</returns>
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

        /// <summary>
        /// Retrieves a single result from a query, verifying the associated table if necessary.
        /// </summary>
        /// <typeparam name="C">The type of the query result.</typeparam>
        /// <param name="commandData">The query data to execute.</param>
        /// <param name="tableSchema">The schema of the table to verify.</param>
        /// <param name="result">The result of the query.</param>
        /// <returns>A CommandReturnState indicating the result of the query.</returns>
        protected CommandReturnState GetSingleResult<C>(SqlCommandData<C> commandData, TableSchema tableSchema, out C result) where C : new()
        {
            result = new C();

            if (!VerifyTable(tableSchema))
            {
                return CommandReturnState.FAILED;
            }

            return GetSingleResult(commandData, out result);
        }

        /// <summary>
        /// Retrieves a single result from a query.
        /// </summary>
        /// <typeparam name="C">The type of the query result.</typeparam>
        /// <param name="commandData">The query data to execute.</param>
        /// <param name="result">The result of the query.</param>
        /// <returns>A CommandReturnState indicating the result of the query.</returns>
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

        /// <summary>
        /// Retrieves multiple results from a query, verifying the associated table if necessary.
        /// </summary>
        /// <typeparam name="C">The type of the query result.</typeparam>
        /// <param name="commandData">The query data to execute.</param>
        /// <param name="tableSchema">The schema of the table to verify.</param>
        /// <param name="results">The results of the query.</param>
        /// <returns>A CommandReturnState indicating the result of the query.</returns>
        protected CommandReturnState GetMultiResult<C>(SqlCommandData<C> commandData, TableSchema tableSchema, out C[] results)
        {
            if (!VerifyTable(tableSchema))
            {
                results = [];
                return CommandReturnState.FAILED;
            }

            return GetMultiResult(commandData, out results);
        }
        /// <summary>
        /// Executes a query that returns multiple results and categorizes the outcome as FOUND, NOTFOUND, or FAILED.
        /// </summary>
        /// <typeparam name="C">Type of the expected result data.</typeparam>
        /// <param name="commandData">The query command data to execute.</param>
        /// <param name="results">The resulting data array if the query succeeds.</param>
        /// <returns>A CommandReturnState indicating the query result.</returns>
        protected CommandReturnState GetMultiResult<C>(SqlCommandData<C> commandData, out C[] results)
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

        /// <summary>
        /// Executes a SQL query and returns the resulting data as a DataTable object.
        /// </summary>
        /// <param name="sqlCommand">The SQL query string to execute.</param>
        /// <returns>A DataTable containing the query result set.</returns>
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

        /// <summary>
        /// Executes a raw SQL command and returns the results as a single concatenated string.
        /// </summary>
        /// <param name="sqlString">The raw SQL command to execute.</param>
        /// <param name="result">A string containing the concatenated results.</param>
        /// <returns>A CommandReturnState indicating whether the command succeeded, failed, or found no results.</returns>
        public CommandReturnState SendSql(string sqlString, out string result)
        {
            SqlCommandData<string> commandData = new SqlCommandData<string>(
                sqlString,
                [],
                reader =>
                {
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
