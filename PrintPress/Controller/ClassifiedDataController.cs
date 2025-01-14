using Microsoft.Data.SqlClient;
using PrintPress.Controller.Data;
using PrintPress.Controller.Enum;
using PrintPress.Data;
using System.Data;
using System.Net.Mail;

namespace PrintPress.Controller
{
    public class ClassifiedDataController : DataController<ClassifiedDataController>
    {
        // Define the table schema type for the DataController specification
        private ClassifiedDatabaseSchema _schema;
        protected override ClassifiedDatabaseSchema Tables { get { return _schema; } }

        /// <summary>
        /// Initialises DataController for the Commercial Database
        /// </summary>
        public override void Initialise()
        {
            _schema = new ClassifiedDatabaseSchema();
            Initialise("ClassifiedData");
        }

        /// <summary>
        /// Checks whether the provided credentials represent a valid employee account
        /// </summary>
        /// <param name="mailAddress"> Email address of the account to match </param>
        /// <param name="password"> Password of the account to match </param>
        /// <param name="message"> Out param of possible error message in case of read failure </param>
        /// <param name="employee"> Out param of EmployeeData if match is successful </param>
        /// <returns> Boolean value indicating whether the write operation was successful </returns>
        public bool ValidateCredentials(MailAddress mailAddress, string password, out string message, out EmployeeData employee)
        {
            message = "database error";
            employee = default;

            string encryptedPass = EncryptPassword(password);

            CommandReturnState readState =  CommercialDataController.Instance.GetEmployee(mailAddress, out employee, out message);

            switch (readState)
            {
                case CommandReturnState.NOTFOUND:
                    message = "invalid credentials";
                    return false;
                case CommandReturnState.FAILED:
                    message = "database error";
                    return false;
                default:
                    break;
            }

            SqlCommandData<int> commandData = new SqlCommandData<int>()
            {
                queryString = "IF EXISTS (SELECT * FROM CREDENTIAL WHERE EmployeeID=@employeeID AND EncPassword=@encPassword) " +
                    "BEGIN SELECT 1 END ELSE BEGIN SELECT 0 END",
                sqlParams = [
                    new SqlParameter("@employeeID", SqlDbType.NVarChar) { Value = employee.Id },
                    new SqlParameter("@encPassword", SqlDbType.NVarChar) { Value = encryptedPass }],
                readerFunc = reader => reader.GetInt32(0)
            };

            if (GetSingleResult(commandData, Tables.CredentialTable, out int result) == CommandReturnState.FAILED)
            {
                return false;
            }

            message = "invalid credentials";
            return Convert.ToBoolean(result);
        }

        /// <summary>
        /// NOT YET IMPLEMENTED - Placeholder function to encrypt input passwords
        /// </summary>
        /// <param name="password"> Raw password string to be encrypted </param>
        /// <returns> Encrypted password string </returns>
        private string EncryptPassword(string password)
        {
            // IMPLEMENT
            return password;
        }

        /// <summary>
        /// Allocates and populates credentials to the database
        /// </summary>
        /// <param name="employee"> Employee data for credential storage </param>
        /// <param name="password"> Encrypted password for credential storage </param>
        /// <returns> Boolean value indicating whether the write operation was successful </returns>
        public bool AddCredentials(EmployeeData employee, string password)
        {
            CommercialDataController com = CommercialDataController.Instance;
            bool commandSuccess = com.AddEmployee(employee, out int employeeID);
            if (!commandSuccess) return commandSuccess;

            return AddItem(Tables.CredentialTable, [employeeID.ToString(), password]);
        }
    }
}
