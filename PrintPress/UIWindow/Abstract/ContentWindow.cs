using PrintPress.Data;

namespace PrintPress.UI
{
#if DEBUG
    internal abstract partial class ContentWindow : PrintPress.UI.Tools.DummyClient
#else
    internal abstract partial class ContentWindow : PrintPress.UI.ClientWindow
#endif
    {
        protected bool UpdatingDisplay { get; set; } = false;
        public ContentWindow() : base()
        {
            InitializeComponent();
        }

        protected virtual void contentListView_SelectedIndexChanged(object sender, EventArgs e) { }

        protected virtual void UpdateContentDisplay(ContentData? content)
        {
            if (content == null)
            {
                return;
            }

            UpdatingDisplay = true;

            titleText.Text = content.Title;
            contentText.Text = content.Text;
            contentStatusComboBox.SelectedIndex = (int)content.State;
            notesText.Text = content.Notes;
            commentsText.Text = content.Comments;
            SetSavedStateLabel(content.LocalChanges);

            UpdatingDisplay = false;
        }

        protected virtual void UpdateContentListDisplay<T>(List<T> employeeContent) where T : ContentData
        {
            contentListView.BeginUpdate();
            contentListView.Items.Clear();

            foreach (T content in employeeContent)
            {
                string text = content.Title;
                if (text == string.Empty)
                {
                    text = "_untitled_";
                }
                ListViewItem item = new ListViewItem(text, contentListView.Groups[(int)content.State]);
                item.Tag = content.ContentID;
                contentListView.Items.Add(item);
            }

            contentListView.EndUpdate();
        }

        protected void SetSavedStateLabel(bool localChanges)
        {
            if (!localChanges)
            {
                saveStateLabel.Text = "saved";
                saveStateLabel.ForeColor = Color.Green;
                return;
            }
            saveStateLabel.Text = "unsaved changes";
            saveStateLabel.ForeColor = Color.OrangeRed;

            saveStateTitleLabel.Location = new Point(saveStateLabel.Location.X - 92, saveStateLabel.Location.Y);
        }

        // Abstract event calls, for specific implementation

        protected abstract void contentText_TextChanged(object sender, EventArgs e);
        protected abstract void titleText_TextChanged(object sender, EventArgs e);
        protected abstract void notesText_TextChanged(object sender, EventArgs e);
        protected abstract void comboBox1_SelectedIndexChanged(object sender, EventArgs e);
        protected abstract void contentStatusComboBox_SelectedIndexChanged(object sender, EventArgs e);
    }
}
