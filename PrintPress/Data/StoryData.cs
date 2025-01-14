using PrintPress.Data.Enum;

namespace PrintPress.Data
{
    /// <summary>
    /// Represents a story in the PrintPress application, inheriting from ContentData.
    /// Contains specific properties and behaviors for managing story data, including sources.
    /// </summary>
    public class StoryData : ContentData
    {
        #region Private Fields

        private string _source = string.Empty;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the source information for the story.
        /// When set, triggers the SourceChanged event.
        /// </summary>
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

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StoryData"/> class.
        /// </summary>
        /// <param name="id">The unique identifier for the story.</param>
        /// <param name="assigned">The employee assigned to the story.</param>
        /// <param name="text">The main text of the story.</param>
        /// <param name="title">The title of the story.</param>
        /// <param name="image">An optional image associated with the story.</param>
        /// <param name="notes">Notes related to the story.</param>
        /// <param name="comments">Comments related to the story.</param>
        /// <param name="state">The current state of the story (e.g., in progress, published).</param>
        /// <param name="lastSaved">The last saved timestamp for the story.</param>
        /// <param name="source">The source information for the story.</param>
        public StoryData(
            int id,
            EmployeeData assigned,
            string text,
            string title,
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

        #endregion

        #region Events

        /// <summary>
        /// Event triggered when the source information is updated.
        /// </summary>
        public event EventHandler<EventArgs>? SourceChanged;

        #endregion

        #region Protected Methods

        /// <summary>
        /// Invokes the SourceChanged event.
        /// </summary>
        protected virtual void OnSourceChanged()
        {
            SourceChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Handles the SourceChanged event and invokes any subscribed handlers.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event data.</param>
        protected virtual void OnSourceChanged(object? sender, EventArgs e)
        {
            SourceChanged?.Invoke(sender, e);
        }

        #endregion
    }
}
