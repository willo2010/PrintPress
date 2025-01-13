using PrintPress.Controller;
using PrintPress.Controller.Enum;
using PrintPress.Data;
using PrintPress.Data.Enum;
using PrintPress.UIService.Abstract;

namespace PrintPress.UIService
{
    public class JournalismService : ContentService<JournalismService>
    {
        // Properties

        public override Department Type { get { return Department.Journalism; } }
        public Dictionary<int, StoryData> ContentList { get; set; } = [];
        public override Dictionary<int, StoryData> GetContentList { get { return ContentList; } }
        public override StoryData? ActiveContent 
        {
            get 
            {
                if (ContentList.ContainsKey(ActiveID))
                {
                    return ContentList[ActiveID];
                }
                return null;
            } 
        }
        public JournalismService(EmployeeData activeEmployee) : base(activeEmployee)
        {
            LoadStoryList();
            if (ContentList.Count == 0)
            {
                return;
            }

            ActiveID = ContentList.ElementAt(0).Key;
        }

        // Public functions

        // Helper functions

        public void LoadStoryList()
        {
            StoryData[] stories;
            string message;

            CommandReturnState crs = CommercialDataController.Instance.GetStories(ActiveEmployee.Id, out stories, out message);
            if (crs == CommandReturnState.FAILED)
            {
                TabStatusMessage = message;
                return;
            }

            ContentList = SortContents(stories);

            OnContentListChanged();
        }
    }
}
