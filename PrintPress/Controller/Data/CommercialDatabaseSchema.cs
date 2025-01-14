namespace PrintPress.Controller.Data
{
    /// <summary>
    /// Represents the database schema for the Commercial Data Controller.
    /// Defines table schemas required for managing employee, content, and related data.
    /// </summary>
    public class CommercialDatabaseSchema : IDatabaseSchema
    {
        #region Properties

        /// <summary>
        /// Gets all the table schemas defined in the commercial database.
        /// </summary>
        public TableSchema[] AllTables
        {
            get
            {
                return [
                    Employee,
                    Person,
                    Address,
                    Clearance,
                    Content,
                    Story,
                    Advert
                ];
            }
        }

        /// <summary>
        /// Gets the schema for the "EMPLOYEE" table, used for storing employee information.
        /// </summary>
        public TableSchema Employee { get { return employeeTable; } }

        /// <summary>
        /// Gets the schema for the "PERSONALDATA" table, used for storing personal details.
        /// </summary>
        public TableSchema Person { get { return personTable; } }

        /// <summary>
        /// Gets the schema for the "ADDRESS" table, used for storing address details.
        /// </summary>
        public TableSchema Address { get { return addressTable; } }

        /// <summary>
        /// Gets the schema for the "CLEARANCE" table, used for managing employee clearances.
        /// </summary>
        public TableSchema Clearance { get { return clearanceTable; } }

        /// <summary>
        /// Gets the schema for the "CONTENT" table, used for storing general content information.
        /// </summary>
        public TableSchema Content { get { return contentTable; } }

        /// <summary>
        /// Gets the schema for the "STORY" table, used for storing story-specific content.
        /// </summary>
        public TableSchema Story { get { return storyTable; } }

        /// <summary>
        /// Gets the schema for the "ADVERT" table, used for storing advert-specific content.
        /// </summary>
        public TableSchema Advert { get { return advertTable; } }

        #endregion

        #region Static Table Definitions

        /// <summary>
        /// Defines the schema for the "EMPLOYEE" table.
        /// </summary>
        private static readonly TableSchema employeeTable = new TableSchema(
            "EMPLOYEE",
            [
                ["EmployeeID", "INT IDENTITY(1,1) PRIMARY KEY"],
                ["PersonID", "INT NOT NULL"],
                ["JobTitle", "NVARCHAR(20) NOT NULL"]
            ]);

        /// <summary>
        /// Defines the schema for the "PERSONALDATA" table.
        /// </summary>
        private static readonly TableSchema personTable = new TableSchema(
            "PERSONALDATA",
            [
                ["PersonID", "INT IDENTITY(1,1) PRIMARY KEY"],
                ["AddressID", "INT"],
                ["FirstNames", "NVARCHAR(30) NOT NULL"],
                ["LastName", "NVARCHAR(20) NOT NULL"],
                ["Email", "NVARCHAR(50) NOT NULL UNIQUE"],
                ["PhoneNum", "NVARCHAR(20)"]
            ]);

        /// <summary>
        /// Defines the schema for the "ADDRESS" table.
        /// </summary>
        public static readonly TableSchema addressTable = new TableSchema(
            "ADDRESS",
            [
                ["AddressID", "INT IDENTITY(1,1) PRIMARY KEY"],
                ["Number_Name", "NVARCHAR(30) NOT NULL"],
                ["Road", "NVARCHAR(30) NOT NULL"],
                ["City", "NVARCHAR(30) NOT NULL"],
                ["County", "NVARCHAR(30) NOT NULL"],
                ["Country", "NVARCHAR(30) NOT NULL"],
                ["Postcode", "NVARCHAR(30) NOT NULL"]
            ]);

        /// <summary>
        /// Defines the schema for the "CLEARANCE" table.
        /// </summary>
        public static readonly TableSchema clearanceTable = new TableSchema(
            "CLEARANCE",
            [
                ["EmployeeID", "INT NOT NULL"],
                ["DepartmentID", "INT NOT NULL"]
            ]);

        /// <summary>
        /// Defines the schema for the "CONTENT" table.
        /// </summary>
        private static readonly TableSchema contentTable = new TableSchema(
            "CONTENT",
            [
                ["ContentID", "INT IDENTITY(1,1) PRIMARY KEY"],
                ["EmployeeID", "INT NOT NULL"],
                ["Text", "NVARCHAR(4000) NOT NULL"],
                ["Title", "NVARCHAR(100) NOT NULL"],
                ["Image", "VARBINARY(MAX) NOT NULL"],
                ["Notes", "NVARCHAR(2000) NOT NULL"],
                ["Comments", "NVARCHAR(2000) NOT NULL"],
                ["State", "INT NOT NULL"],
                ["LastSaved", "DATETIME NOT NULL"]
            ]);

        /// <summary>
        /// Defines the schema for the "STORY" table.
        /// </summary>
        private static readonly TableSchema storyTable = new TableSchema(
           "STORY",
           [
                ["ContentID", "INT PRIMARY KEY"],
                ["Source", "VARCHAR(100) NOT NULL"]
           ]);

        /// <summary>
        /// Defines the schema for the "ADVERT" table.
        /// </summary>
        private static readonly TableSchema advertTable = new TableSchema(
            "ADVERT",
            [
                ["ContentID", "INT PRIMARY KEY"],
                ["ContactID", "INT NOT NULL"]
            ]);

        #endregion
    }
}
