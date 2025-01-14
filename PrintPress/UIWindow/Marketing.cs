using PrintPress.Data.Enum;
using PrintPress.UIService;

namespace PrintPress.UI
{
#if DEBUG
    internal partial class Marketing : PrintPress.UI.Tools.DummyContent//, IConcreteClientWindow<MarketingService>
#else
    internal partial class Marketing : PrintPress.UI.ContentWindow
#endif
    {
        public MarketingService Service { get; init; }
        public Department Type { get; init; }
        public Marketing(MarketingService marketingService) : base()
        {
            Service = marketingService;
            Type = Department.Marketing;
            InitializeComponent();
        }
    }
}
