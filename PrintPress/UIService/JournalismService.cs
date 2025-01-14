using PrintPress.Controller;
using PrintPress.Controller.Enum;
using PrintPress.Data;
using PrintPress.Data.Enum;
using PrintPress.UIService.Abstract;

namespace PrintPress.UIService
{
    /// <summary>
    /// Provides services and logic specific to the Journalism department.
    /// </summary>
    public class JournalismService : ContentService<JournalismService>
    {
        #region Properties

        /// <summary>
        /// Gets the department type associated with the service.
        /// </summary>
        public override Department Type { get { return Department.Journalism; } }

        /// <summary>
        /// Dictionary containing all story data managed by the active employee.
        /// </summary>
        public Dictionary<int, StoryData> ContentList { get; set; } = [];

        /// <summary>
        /// Gets the content list as a dictionary for journalism-related content.
        /// </summary>
        public override Dictionary<int, StoryData> GetContentList { get { return ContentList; } }

        /// <summary>
        /// Gets the currently active story based on the active content ID.
        /// </summary>
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

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JournalismService"/> class with the specified active employee.
        /// </summary>
        /// <param name="activeEmployee">The active employee associated with this service.</param>
        public JournalismService(EmployeeData activeEmployee) : base(activeEmployee)
        {
            LoadStoryList();
            if (ContentList.Count == 0)
            {
                return;
            }

            ActiveID = ContentList.ElementAt(0).Key;
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Loads the list of stories associated with the active employee.
        /// </summary>
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

        #endregion
    }
}
