using PrintPress.Data;

namespace PrintPress.UI
{
#if DEBUG
    /// <summary>
    /// Represents the abstract base class for content-based windows in the application, 
    /// using a dummy client implementation during debugging.
    /// </summary>
    internal abstract partial class ContentWindow : PrintPress.UI.Tools.DummyClient
#else
    /// <summary>
    /// Represents the abstract base class for content-based windows in the application.
    /// </summary>
    internal abstract partial class ContentWindow : PrintPress.UI.ClientWindow
#endif
    {
        #region Properties

        /// <summary>
        /// Indicates whether the content display is currently being updated, preventing recursive updates.
        /// </summary>
        protected bool UpdatingDisplay { get; set; } = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentWindow"/> class and sets up the UI components.
        /// </summary>
        public ContentWindow() : base()
        {
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the selection change in the content list view. 
        /// Can be overridden by derived classes to provide specific functionality.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void contentListView_SelectedIndexChanged(object sender, EventArgs e) { }

        #endregion

        #region Content Display Management

        /// <summary>
        /// Updates the content display with the details of the provided content data.
        /// </summary>
        /// <param name="content">The content data to display.</param>
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

        /// <summary>
        /// Updates the content list display with a list of employee content items.
        /// </summary>
        /// <typeparam name="T">The type of content data.</typeparam>
        /// <param name="employeeContent">The list of employee content to display.</param>
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
                ListViewItem item = new ListViewItem(text, contentListView.Groups[(int)content.State])
                {
                    Tag = content.ContentID
                };
                contentListView.Items.Add(item);
            }

            contentListView.EndUpdate();
        }

        /// <summary>
        /// Updates the save state label based on whether there are unsaved local changes.
        /// </summary>
        /// <param name="localChanges">Indicates whether there are unsaved local changes.</param>
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

        #endregion

        #region Abstract Methods

        /// <summary>
        /// Abstract method to handle text changes in the content text field.
        /// </summary>
        protected abstract void contentText_TextChanged(object sender, EventArgs e);

        /// <summary>
        /// Abstract method to handle text changes in the title text field.
        /// </summary>
        protected abstract void titleText_TextChanged(object sender, EventArgs e);

        /// <summary>
        /// Abstract method to handle text changes in the notes text field.
        /// </summary>
        protected abstract void notesText_TextChanged(object sender, EventArgs e);

        /// <summary>
        /// Abstract method to handle selection changes in the first combo box.
        /// </summary>
        protected abstract void comboBox1_SelectedIndexChanged(object sender, EventArgs e);

        /// <summary>
        /// Abstract method to handle selection changes in the content status combo box.
        /// </summary>
        protected abstract void contentStatusComboBox_SelectedIndexChanged(object sender, EventArgs e);

        #endregion
    }
}
