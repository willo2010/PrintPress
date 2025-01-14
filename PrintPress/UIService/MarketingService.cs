using PrintPress.Controller;
using PrintPress.Controller.Enum;
using PrintPress.Data;
using PrintPress.Data.Enum;
using PrintPress.UIService.Abstract;

namespace PrintPress.UIService
{
    /// <summary>
    /// Provides services and logic specific to the Marketing department.
    /// </summary>
    public class MarketingService : ContentService<MarketingService>
    {
        #region Properties

        /// <summary>
        /// Gets the department type associated with the service.
        /// </summary>
        public override Department Type { get { return Department.Marketing; } }

        /// <summary>
        /// List of adverts managed by the active employee.
        /// </summary>
        public List<AdvertData> EmployeeAdverts { get; set; } = [];

        /// <summary>
        /// Gets the content list as a dictionary (not applicable for MarketingService).
        /// </summary>
        public override Dictionary<int, StoryData> GetContentList { get; }

        /// <summary>
        /// Gets the currently active advert.
        /// </summary>
        public override AdvertData ActiveContent { get { return EmployeeAdverts[ActiveID]; } }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MarketingService"/> class with the specified active employee.
        /// </summary>
        /// <param name="activeEmployee">The active employee associated with this service.</param>
        public MarketingService(EmployeeData activeEmployee) : base(activeEmployee)
        {
            LoadAdvertList();
        }

        #endregion

        #region Helper Functions

        /// <summary>
        /// Loads the list of adverts associated with the active employee from the database.
        /// </summary>
        private void LoadAdvertList()
        {
            CommandReturnState crs = CommercialDataController.Instance.GetAdverts(ActiveEmployee.Id, out AdvertData[] ads, out string message);
            if (crs == CommandReturnState.FAILED)
            {
                TabStatusMessage = message;
                return;
            }

            EmployeeAdverts = ads.ToList();
            SortContentByDate(EmployeeAdverts);

            OnContentListChanged();
        }

        #endregion
    }
}
