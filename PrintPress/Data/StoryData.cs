using PrintPress.Data.Enum;

namespace PrintPress.Data
{
    public class StoryData : ContentData
    {
        private string _source = string.Empty;
        public string Sources
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
                OnSourceChanged();
            }
        }

        public StoryData(int id,
            EmployeeData assigned,
            string text, string title,
            Image? image,
            string notes,
            string comments,
            ContentState state,
            DateTime lastSaved,
            string source) :
            base(id, assigned, text, title, image, notes, comments, state, lastSaved)
        {
            _source = source;
            SourceChanged += OnDataChanged;
        }

        public event EventHandler<EventArgs>? SourceChanged;
        protected virtual void OnSourceChanged()
        {
            SourceChanged?.Invoke(this, new EventArgs());
        }
        protected virtual void OnSourceChanged(object? sender, EventArgs e)
        {
            SourceChanged?.Invoke(sender, e);
        }
    }
}