using Microsoft.Data.SqlClient;
using PrintPress.Controller;
using PrintPress.Controller.Data;
using PrintPress.Controller.Enum;
using PrintPress.Data;
using PrintPress.Data.Enum;
using PrintPress.UICommand.ContentCommand;
using PrintPress.UICommand.ContentCommand.Journalism;
using PrintPress.UIService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PrintPressUnitTesting.DataController
{
    [TestClass]
    public sealed class Tests
    {
        [TestMethod]
        public void ValidateCredentials_InvalidCredentials_ReturnsFalse()
        {
            // Arrange
            ClassifiedDataController controller = ClassifiedDataController.Instance;
            MailAddress invalidEmail = new MailAddress("invalid@example.com");
            string invalidPassword = "wrongpassword";
            string message;
            EmployeeData employee;

            // Act
            bool isValid = controller.ValidateCredentials(invalidEmail, invalidPassword, out message, out employee);

            // Assert
            Assert.IsFalse(isValid, "Expected invalid credentials to return false.");
            //Assert.AreEqual("invalid credentials", message, "Expected error message for invalid credentials.");
        }

        [TestMethod]
        public void UpdateContentTextCommand_Undo_ResetsOldText()
        {
            // Arrange
            JournalismService service = new JournalismService(new EmployeeData(1));
            service.ActiveID = 1;
            StoryData story = new StoryData(1, new EmployeeData(1), "Original text", "Title", null, "Notes", "Comments", ContentState.IN_PROGRESS, DateTime.Now, "Source");
            service.ContentList[1] = story;

            string newText = "Updated text";
            UpdateContentTextCommand<JournalismService> command = new UpdateContentTextCommand<JournalismService>(service, newText);

            // Act
            command.Execute();
            command.UndoExecute();

            // Assert
            Assert.AreEqual("Original text", story.Text, "Expected text to revert to original after undo.");
        }

        [TestMethod]
        public void GetEmployee_ValidEmail_ReturnsEmployeeData()
        {
            // Arrange
            CommercialDataController controller = CommercialDataController.Instance;
            controller.Initialise();

            MailAddress validEmail = new MailAddress("test@example.com");
            EmployeeData employee;
            string message;

            // Act
            CommandReturnState state = controller.GetEmployee(validEmail, out employee, out message);

            // Assert
            Assert.AreEqual(CommandReturnState.FOUND, state, "Expected valid employee to be found.");
            Assert.AreEqual(employee.Id, -1);
        }

        [TestMethod]
        public void ClientService_ExecuteHistory_PushesExecutedCommand()
        {
            // Arrange
            JournalismService service = new JournalismService(new EmployeeData(1));
            AddStoryCommand command = new AddStoryCommand(service);

            // Act
            command.Execute();

            // Assert
            Assert.IsTrue(service.ExecuteHistory.Count > 0, "Expected executed command to be added to history.");
            Assert.AreEqual(command, service.ExecuteHistory.Peek(), "Expected last executed command to be on top of the stack.");
        }

        [TestMethod]
        public void ExecuteNonQuery_InvalidQuery_ReturnsFalse()
        {
            // Arrange
            CommercialDataController controller = CommercialDataController.Instance;
            SqlCommandData<object> invalidCommand = new SqlCommandData<object>
            {
                queryString = "INVALID SQL QUERY",
                sqlParams = Array.Empty<SqlParameter>()
            };

            // Act
            bool success = controller.ExecuteNonQuery(invalidCommand);

            // Assert
            Assert.IsFalse(success, "Expected ExecuteNonQuery to return false for invalid SQL.");
        }


    }
}
