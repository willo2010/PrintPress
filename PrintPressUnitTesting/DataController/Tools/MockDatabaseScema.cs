using PrintPress.Controller.Data;

namespace PrintPressUnitTesting.DataController.Tools
{
    public class MockDatabaseScema : IDatabaseSchema
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
            "TEST",
            [
                ["TestID", "INT IDENTITY(1,1) PRIMARY KEY"],
                ["TestVarchar", "NVARCHAR(30) NOT NULL"]
            ]);
    }
}
