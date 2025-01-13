using PrintPress.Data.Enum;

namespace PrintPress.Data
{
    public abstract class ContentData : Data
    {
        public int ContentID { get; init; } = -1;

        protected EmployeeData _assigned = new EmployeeData();
        public EmployeeData Assigned
        {
            get { return _assigned; }
            set
            {
                _assigned = value;
                OnDataChanged();
            }
        }

        protected string _text = string.Empty;
        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnDataChanged();
            }
        }

        protected string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnDataChanged();
            }
        }

        protected Image? _image = null;
        public Image? Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnDataChanged();
            }
        }

        protected string _notes = string.Empty;
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnDataChanged();
            }
        }

        protected string _comments = string.Empty;
        public string Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                OnDataChanged();
            }
        }

        protected ContentState _state = ContentState.IN_PROGRESS;
        public ContentState State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnDataChanged();
            }
        }
        public DateTime LastSaved { get; set; }
        public ContentData()
        {
            LastSaved = DateTime.Now;
        }
        private bool _localChanges = false;
        public bool LocalChanges
        {
            get
            {
                return _localChanges;
            }
            set
            {
                _localChanges = value;
                OnLocalChangesChanged();
            }
        }

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

        private void SetLocalChangesTrue(object? sender, EventArgs e)
        {
            LocalChanges = true;
        }

        public event EventHandler<EventArgs>? SaveStateChanged;
        protected virtual void OnLocalChangesChanged()
        {
            SaveStateChanged?.Invoke(this, new EventArgs());
        }
    }
}
