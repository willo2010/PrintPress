using Microsoft.Data.SqlClient;
using PrintPress.Controller;
using PrintPress.Controller.Data;
using PrintPress.Controller.Enum;
using PrintPress.Data;
using PrintPress.Data.Enum;
using PrintPress.UICommand.ContentCommand;
using PrintPress.UICommand.ContentCommand.Journalism;
using PrintPress.UIService;
using PrintPressUnitTesting.DataController.Tools;
using System.Net.Mail;

namespace PrintPressUnitTesting.DataController
{
    [TestClass]
    public sealed class Tests
    {
        [TestInitialize]
        public void Setup()
        {
            ClassifiedDataController.Instance.Initialise();
            MockDataController.Instance.Initialise();
        }

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
            CommercialDataController.Instance.Initialise();
            JournalismService service = new JournalismService(new EmployeeData(1));
            AddStoryCommand command = new AddStoryCommand(service);

            // Act
            command.Execute();

            // Assert
            Assert.IsTrue(service.ExecuteHistory.Count > 0, "Expected executed command to be added to history.");
            Assert.AreEqual(command, service.ExecuteHistory.Peek(), "Expected last executed command to be on top of the stack.");
        }

        [TestMethod]
        public void SortContentByDate_SortsCorrectly()
        {
            // Arrange
            var service = new MockJournalismService(new EmployeeData());
            var content1 = new StoryData(1, new EmployeeData(), "Text1", "Title1", null, "Notes1", "Comments1", ContentState.IN_PROGRESS, DateTime.Now.AddDays(-2), "Source1");
            var content2 = new StoryData(2, new EmployeeData(), "Text2", "Title2", null, "Notes2", "Comments2", ContentState.IN_PROGRESS, DateTime.Now, "Source2");

            List<ContentData> unsorted = new List<ContentData> { content2, content1 };

            // Act
            List<ContentData> sorted = service.TestSortContentByDate(unsorted);

            // Assert
            Assert.AreEqual(content1, sorted.First(), "Expected oldest content to appear first.");
            Assert.AreEqual(content2, sorted.Last(), "Expected newest content to appear last.");
        }

        [TestMethod]
        public void ExecuteNonQuery_InvalidQuery_ReturnsFalse()
        {
            // Arrange
            MockDataController controller = MockDataController.Instance;

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
