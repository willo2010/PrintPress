using PrintPress.Data.Enum;

namespace PrintPress.Data
{
    /// <summary>
    /// Abstract base class representing shared properties and functionality for content data such as stories and adverts.
    /// </summary>
    public abstract class ContentData : Data
    {
        #region Properties

        /// <summary>
        /// Gets the unique identifier for the content.
        /// </summary>
        public int ContentID { get; init; } = -1;

        private EmployeeData _assigned = new EmployeeData();
        /// <summary>
        /// Gets or sets the employee assigned to the content. Triggers the OnDataChanged event when updated.
        /// </summary>
        public EmployeeData Assigned
        {
            get { return _assigned; }
            set
            {
                _assigned = value;
                OnDataChanged();
            }
        }

        private string _text = string.Empty;
        /// <summary>
        /// Gets or sets the main textual content. Triggers the OnDataChanged event when updated.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnDataChanged();
            }
        }

        private string _title = string.Empty;
        /// <summary>
        /// Gets or sets the title of the content. Triggers the OnDataChanged event when updated.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnDataChanged();
            }
        }

        private Image? _image = null;
        /// <summary>
        /// Gets or sets the associated image for the content. Triggers the OnDataChanged event when updated.
        /// </summary>
        public Image? Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnDataChanged();
            }
        }

        private string _notes = string.Empty;
        /// <summary>
        /// Gets or sets additional notes for the content. Triggers the OnDataChanged event when updated.
        /// </summary>
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnDataChanged();
            }
        }

        private string _comments = string.Empty;
        /// <summary>
        /// Gets or sets comments on the content. Triggers the OnDataChanged event when updated.
        /// </summary>
        public string Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                OnDataChanged();
            }
        }

        private ContentState _state = ContentState.IN_PROGRESS;
        /// <summary>
        /// Gets or sets the current state of the content. Triggers the OnDataChanged event when updated.
        /// </summary>
        public ContentState State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnDataChanged();
            }
        }

        /// <summary>
        /// Gets or sets the last saved timestamp for the content.
        /// </summary>
        public DateTime LastSaved { get; set; }

        private bool _localChanges = false;
        /// <summary>
        /// Gets or sets a value indicating whether there are unsaved changes locally. Triggers the SaveStateChanged event when updated.
        /// </summary>
        public bool LocalChanges
        {
            get { return _localChanges; }
            set
            {
                _localChanges = value;
                OnLocalChangesChanged();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentData"/> class with default values.
        /// </summary>
        public ContentData()
        {
            LastSaved = DateTime.Now;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentData"/> class with specified values.
        /// </summary>
        /// <param name="id">The unique identifier for the content.</param>
        /// <param name="assigned">The employee assigned to the content.</param>
        /// <param name="text">The textual content.</param>
        /// <param name="title">The title of the content.</param>
        /// <param name="image">The associated image.</param>
        /// <param name="notes">Additional notes for the content.</param>
        /// <param name="comments">Comments on the content.</param>
        /// <param name="state">The current state of the content.</param>
        /// <param name="lastSaved">The last saved timestamp.</param>
        public ContentData(int id, EmployeeData assigned, string text, string title, Image? image, string notes, string comments, ContentState state, DateTime lastSaved)
        {
            ContentID = id;
            _assigned = assigned;
            _text = text;
            _title = title;
            _image = image;
            _notes = notes;
            _comments = comments;
            _state = state;
            LastSaved = lastSaved;

            DataChanged += SetLocalChangesTrue;
        }

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the save state changes (e.g., local changes are made).
        /// </summary>
        public event EventHandler<EventArgs>? SaveStateChanged;

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the LocalChanges property to true when data changes.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void SetLocalChangesTrue(object? sender, EventArgs e)
        {
            LocalChanges = true;
        }

        /// <summary>
        /// Triggers the SaveStateChanged event when LocalChanges is updated.
        /// </summary>
        protected virtual void OnLocalChangesChanged()
        {
            SaveStateChanged?.Invoke(this, new EventArgs());
        }

        #endregion
    }
}
