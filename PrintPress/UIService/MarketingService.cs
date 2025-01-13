using PrintPress.Controller;
using PrintPress.Controller.Enum;
using PrintPress.Data;
using PrintPress.Data.Enum;
using PrintPress.UIService.Abstract;

namespace PrintPress.UIService
{
    public class MarketingService : ContentService<MarketingService>
    {
        // Properties

        public override Department Type { get { return Department.Marketing; } }
        public List<AdvertData> EmployeeAdverts { get; set; } = [];
        public override Dictionary<int, StoryData> GetContentList { get; }
        public override AdvertData ActiveContent { get { return EmployeeAdverts[ActiveID]; } }

        // Constructors

        public MarketingService(EmployeeData activeEmployee) : base(activeEmployee)
        {
            LoadAdvertList();
        }

        // Helper functions

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
    }
}
