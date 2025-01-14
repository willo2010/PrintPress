namespace PrintPress.Controller.Data
{
    /// <summary>
    /// Represents the database schema for the Classified Data Controller.
    /// Provides access to the table schemas defined within the classified database.
    /// </summary>
    public class ClassifiedDatabaseSchema : IDatabaseSchema
    {
        /// <summary>
        /// Gets all the table schemas defined in the classified database.
        /// </summary>
        public TableSchema[] AllTables
        {
            get
            {
                return [CredentialTable];
            }
        }

        /// <summary>
        /// Gets the schema for the "CREDENTIAL" table, used for storing employee credentials.
        /// </summary>
        public TableSchema CredentialTable { get { return credentialTable; } }

        /// <summary>
        /// Static schema definition for the "CREDENTIAL" table.
        /// Contains columns for EmployeeID and EncPassword.
        /// </summary>
        private static readonly TableSchema credentialTable = new TableSchema(
            "CREDENTIAL",
            [
                ["EmployeeID", "INT PRIMARY KEY"],
                ["EncPassword", "NVARCHAR(30) NOT NULL"]
            ]);
    }
}
