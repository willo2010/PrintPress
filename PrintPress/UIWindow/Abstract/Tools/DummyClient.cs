using PrintPress.Data.Enum;
using PrintPress.UIService;
using PrintPress.UIService.Abstract;
using PrintPress.UIWindow.Interface;

namespace PrintPress.UI.Tools
{
    internal partial class DummyClient : ClientWindow
    {
        public DummyClient() : base() { }
        public override Department Type { get { return Department.Marketing; } } // Default 
    }
}