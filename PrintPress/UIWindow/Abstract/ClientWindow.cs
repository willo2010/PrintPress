using PrintPress.Data.Enum;
using System.ComponentModel;

namespace PrintPress.UI
{
    public abstract partial class ClientWindow : Form
    {
        public abstract Department Type { get; }
        public ClientWindow()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }
            InitializeComponent();
        } // TALK ABOUT LOG
    }
}