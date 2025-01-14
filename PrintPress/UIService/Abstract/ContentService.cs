using PrintPress.Data;
using System;

namespace PrintPress.UIService.Abstract
{
    /// <summary>
    /// Represents a base class for managing content-specific services, providing functionality for handling content data and associated events.
    /// </summary>
    /// <typeparam name="T">The specific type of the content service inheriting from this base class.</typeparam>
    public abstract class ContentService<T> : ClientService<T> where T : ContentService<T>
    {
        #region Properties

        /// <summary>
        /// Gets the currently active content.
        /// </summary>
        public abstract ContentData? ActiveContent { get; }

        /// <summary>
        /// Gets the list of content data indexed by their unique IDs.
        /// </summary>
        public abstract Dictionary<int, StoryData> GetContentList { get; }

        private int _activeContentID = -1;

        /// <summary>
        /// Gets or sets the ID of the currently active content.
        /// </summary>
        public int ActiveID
        {
            get
            {
                return _activeContentID;
            }
            set
            {
                BeforeActiveChanged();
                _activeContentID = value;
                OnActiveChanged();
            }
        }

        private string _statusMessage = string.Empty;

        /// <summary>
        /// Gets or sets the current status message displayed in the tab.
        /// </summary>
        public string TabStatusMessage
        {
            get
            {
                return _statusMessage;
            }
            set
            {
                _statusMessage = value;
                OnTabStatusMessageChanged();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentService{T}"/> class.
        /// </summary>
        /// <param name="employee">The active employee associated with this service.</param>
        public ContentService(EmployeeData employee) : base(employee) { }

        #endregion

        #region Event Handling

        /// <summary>
        /// Event triggered when the content list changes.
        /// </summary>
        public event EventHandler<EventArgs>? ContentListChanged;

        /// <summary>
        /// Triggers the <see cref="ContentListChanged"/> event.
        /// </summary>
        protected virtual void OnContentListChanged()
        {
            ContentListChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Event triggered when the status message changes.
        /// </summary>
        public event EventHandler<EventArgs>? StatusMessageChanged;

        /// <summary>
        /// Triggers the <see cref="StatusMessageChanged"/> event.
        /// </summary>
        private void OnTabStatusMessageChanged()
        {
            StatusMessageChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Event triggered before the active content changes.
        /// </summary>
        public event EventHandler<EventArgs>? ActiveChanging;

        /// <summary>
        /// Triggers the <see cref="ActiveChanging"/> event.
        /// </summary>
        protected virtual void BeforeActiveChanged()
        {
            ActiveChanging?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Event triggered after the active content changes.
        /// </summary>
        public event EventHandler<EventArgs>? ActiveChanged;

        /// <summary>
        /// Triggers the <see cref="ActiveChanged"/> event.
        /// </summary>
        protected virtual void OnActiveChanged()
        {
            ActiveChanged?.Invoke(this, new EventArgs());
        }

        #endregion

        #region Helper Functions

        /// <summary>
        /// Sorts an array of content data by their last saved date and maps them to a dictionary by their unique IDs.
        /// </summary>
        /// <typeparam name="C">The specific type of content data.</typeparam>
        /// <param name="contents">The array of content data to sort.</param>
        /// <returns>A dictionary mapping content IDs to content data.</returns>
        protected Dictionary<int, C> SortContents<C>(C[] contents) where C : ContentData
        {
            List<C> sortedContents = SortContentByDate(contents.ToList());
            Dictionary<int, C> idStoryPair = new();
            foreach (C story in sortedContents)
            {
                idStoryPair.Add(story.ContentID, story);
            }
            return idStoryPair;
        }

        /// <summary>
        /// Sorts a list of content data by their last saved date.
        /// </summary>
        /// <typeparam name="C">The specific type of content data.</typeparam>
        /// <param name="contentList">The list of content data to sort.</param>
        /// <returns>The sorted list of content data.</returns>
        protected List<C> SortContentByDate<C>(List<C> contentList) where C : ContentData
        {
            contentList.Sort((x, y) => x.LastSaved.CompareTo(y.LastSaved));
            return contentList;
        }

        #endregion
    }
}
