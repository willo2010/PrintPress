using PrintPress.Controller.Data;

namespace PrintPressUnitTesting.DataController.Tools
{
    /// <summary>
    /// A mock implementation of the <see cref="IDatabaseSchema"/> interface for unit testing.
    /// Provides a test-specific schema to simulate database tables and structure.
    /// </summary>
    public class MockDatabaseSchema : IDatabaseSchema
    {
        /// <summary>
        /// Gets all the table schemas defined in the mock database.
        /// </summary>
        public TableSchema[] AllTables
        {
            get
            {
                return [CredentialTable];
            }
        }

        /// <summary>
        /// Gets the schema for the "Credential" table used in tests.
        /// </summary>
        public TableSchema CredentialTable { get { return credentialTable; } }

        /// <summary>
        /// Static definition of the "Credential" table schema for mock data.
        /// </summary>
        private static readonly TableSchema credentialTable = new TableSchema(
            "TEST",
            [
                ["TestID", "INT IDENTITY(1,1) PRIMARY KEY"],
                ["TestVarchar", "NVARCHAR(30) NOT NULL"]
            ]);
    }
}