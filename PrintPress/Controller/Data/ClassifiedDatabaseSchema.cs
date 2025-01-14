namespace PrintPress.Controller.Data
{
    public class ClassifiedDatabaseSchema : IDatabaseSchema
    {
        public TableSchema[] AllTables 
        { 
            get
            {
                return [CredentialTable];
            }
        }

        public TableSchema CredentialTable { get { return credentialTable; } }

        private static readonly TableSchema credentialTable = new TableSchema(
            "CREDENTIAL",
            [
                ["EmployeeID", "INT PRIMARY KEY"],
                ["EncPassword", "NVARCHAR(30) NOT NULL"]
            ]);
    }
}