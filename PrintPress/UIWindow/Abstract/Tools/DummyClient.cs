using PrintPress.Data.Enum;

namespace PrintPress.UI.Tools
{
    internal partial class DummyClient : ClientWindow
    {
        public DummyClient() : base() { }
        public override Department Type { get { return Department.Marketing; } } // Default 
    }
}