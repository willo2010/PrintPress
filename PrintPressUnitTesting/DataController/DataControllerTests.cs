using PrintPress.Controller;
using PrintPress.Controller.Enum;
using PrintPress.Data;
using PrintPress.Data.Builder;
using PrintPress.Data.Enum;
using PrintPressUnitTesting.DataController.Tools;

namespace PrintPressUnitTesting.DataController
{
    /*
    [TestClass]
    public sealed class DataControllerTests
    {
        [TestMethod]
        public void AllTests()
        {
            FullStoryTableTest();
        }

        //[TestMethod]
        public void FullStoryTableTest()
        {
            CommercialDataController commercialDataController = new CommercialDataController();
            // Run test on advert table
            MockCommercialDataController.Instance.Initialise();

            // Check that adverts mock advert table is empty
            CommandReturnState firstReadState = MockCommercialDataController.Instance.GetStories(0, out StoryData[] _, out string readErrMsg);
            Assert.IsTrue(firstReadState == CommandReturnState.NOTFOUND, $"Unexpected DB state with error: {readErrMsg}");

            // Generate test data
            StoryData storyData = GenerateTestStory();

            // Allocate space on database
            bool allocateSuccess = MockCommercialDataController.Instance.AllocateStory(storyData.Assigned.Id, out int advertAutoId);
            Assert.IsTrue(allocateSuccess, $"Could not allocate space for story for employee: {storyData.Assigned.Id}");

            // Write story data
            bool writeSuccess = MockCommercialDataController.Instance.UpdateStory(storyData);
            Assert.IsTrue(writeSuccess, $"Failed to write story to database");

            // CLEANUP
            // Delete story
            bool deleteSuccess = MockCommercialDataController.Instance.DeleteContent(storyData.ContentID);
            Assert.IsTrue(writeSuccess, $"Failed to delete story from database");

            // Verify cleanup, check mock table is empty
            CommandReturnState secondReadState = MockCommercialDataController.Instance.GetStories(0, out StoryData[] _, out readErrMsg);
            Assert.IsTrue(secondReadState == CommandReturnState.NOTFOUND, $"Unexpected DB state with error: {readErrMsg}");
        }

        private static StoryData GenerateTestStory()
        {
            PersonalData personalData = GenerateTestPersonalData();

            EmployeeData employeeData = new EmployeeData(0, personalData, "test", [Department.Journalism, Department.Editing]);

            StoryBuilder storyBuilder = new StoryBuilder();
            storyBuilder.Text = "test";
            storyBuilder.Notes = "test";
            storyBuilder.State = ContentState.IN_PROGRESS;
            storyBuilder.Assigned = employeeData;
            storyBuilder.ContentID = -1;
            storyBuilder.Comments = "test";
            storyBuilder.Source = "test";

            return storyBuilder.ToStoryData();
        }

        private static PersonalData GenerateTestPersonalData()
        {
            Address testAddress = GenerateTestAddress();

            PersonalDataBuilder personBuilder = new PersonalDataBuilder();

            personBuilder.Address = testAddress;
            personBuilder.Email = "test@test.com";
            personBuilder.FirstNames = "test";
            personBuilder.LastName = "test";
            personBuilder.Phone = "test";

            return personBuilder.ToPersonalData();
        }

        private static Address GenerateTestAddress()
        {
            AddressBuilder addressBuilder = new AddressBuilder();
            addressBuilder.Road = "test";
            addressBuilder.HouseNameOrNumber = "test";
            addressBuilder.Country = "test";
            addressBuilder.City = "test";
            addressBuilder.Country = "test";
            addressBuilder.Postcode = "test";

            return addressBuilder.ToAddress();
        }
    
    }
    */
}
