using Microsoft.Data.SqlClient;

namespace PrintPress.Controller.Data
{
    public struct SqlCommandData<C>
    {
        public string queryString;
        public SqlParameter[] sqlParams = [];
        public Func<SqlDataReader, C>? readerFunc;

        public SqlCommandData(string queryString, SqlParameter[] sqlParams)
        {
            this.queryString = queryString;
            this.sqlParams = sqlParams;
        }
        public SqlCommandData(string queryString, SqlParameter[] sqlParams, Func<SqlDataReader, C>? readerFunc)
        {
            this.queryString = queryString;
            this.sqlParams = sqlParams;
            this.readerFunc = readerFunc;
        }
    }
}
