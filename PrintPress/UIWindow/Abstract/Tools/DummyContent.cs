using PrintPress.UIService;
using PrintPress.UIService.Abstract;

namespace PrintPress.UI.Tools
{
    internal partial class DummyContent : ContentWindow
    {
        public DummyContent() : base() { }
        protected override void titleText_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        protected override void contentText_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        protected override void notesText_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        protected override void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        protected override void contentStatusComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}