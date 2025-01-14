using PrintPress.Data.Enum;
using PrintPress.Data;
using PrintPress.UIService.Abstract;
using PrintPress.UIService;

namespace PrintPressUnitTesting.DataController.Tools
{
    internal class MockJournalismService : JournalismService
    {
        public override Department Type => throw new NotImplementedException();
        public MockJournalismService(EmployeeData employee) : base(employee) { }

        public List<ContentData> TestSortContentByDate(List<ContentData> contentList)
        {
            return SortContentByDate(contentList);
        }
    }
}
