using System.Data;

namespace PrintPress.Controller.Data
{
    public class CommercialDatabaseSchema : IDatabaseSchema
    {
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
                    Advert];
            }
        }

        public TableSchema Employee { get { return employeeTable; } }
        public TableSchema Person { get { return personTable; } }
        public TableSchema Address { get { return addressTable; } }
        public TableSchema Clearance { get { return clearanceTable; } }
        public TableSchema Content { get { return contentTable; } }
        public TableSchema Story { get { return storyTable; } }
        public TableSchema Advert { get { return advertTable; } }

        private static readonly TableSchema employeeTable = new TableSchema(
            "EMPLOYEE",
            [
                ["EmployeeID", "INT IDENTITY(1,1) PRIMARY KEY"],
                ["PersonID", "INT NOT NULL"],
                ["JobTitle", "NVARCHAR(20) NOT NULL"]
            ]);

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

        public static readonly TableSchema clearanceTable = new TableSchema(
            "CLEARANCE",
            [
                ["EmployeeID", "INT NOT NULL"],
                ["DepartmentID", "INT NOT NULL"]
            ]);

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

        private static readonly TableSchema storyTable = new TableSchema(
           "STORY",
           [
                ["ContentID", "INT PRIMARY KEY"],
                ["Source", "VARCHAR(100) NOT NULL"]
           ]);

        private static readonly TableSchema advertTable = new TableSchema(
            "ADVERT",
            [
                ["ContentID", "INT PRIMARY KEY"],
                ["ContactID", "INT NOT NULL"]
            ]);
    }
}
