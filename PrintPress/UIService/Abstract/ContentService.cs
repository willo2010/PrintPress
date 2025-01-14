using PrintPress.Data;
using System;

namespace PrintPress.UIService.Abstract
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ContentService<T> : ClientService<T> where T : ContentService<T>
    {
        #region Properties

        public abstract ContentData? ActiveContent { get; }
        public abstract Dictionary<int, StoryData> GetContentList { get; }

        private int _activeContentID = -1;

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

        public ContentService(EmployeeData employee) : base(employee) { }

        #endregion
        #region Event handling

        public event EventHandler<EventArgs>? ContentListChanged;
        protected virtual void OnContentListChanged()
        {
            ContentListChanged?.Invoke(this, new EventArgs());
        }
        public event EventHandler<EventArgs>? StatusMessageChanged;
        private void OnTabStatusMessageChanged()
        {
            StatusMessageChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler<EventArgs>? ActiveChanging;
        protected virtual void BeforeActiveChanged()
        {
            ActiveChanging?.Invoke(this, new EventArgs());
        }
        public event EventHandler<EventArgs>? ActiveChanged;
        protected virtual void OnActiveChanged()
        {
            ActiveChanged?.Invoke(this, new EventArgs());
        }

        #endregion
        #region Helper functions

        protected Dictionary<int,C> SortContents<C>(C[] contents) where C : ContentData
        {
            List<C> sortedContents = SortContentByDate(contents.ToList());
            Dictionary<int, C> idStoryPair = new();
            foreach (C story in sortedContents)
            {
                idStoryPair.Add(story.ContentID, story);
            }
            return idStoryPair;
        }

        protected List<C> SortContentByDate<C>(List<C> contentList) where C : ContentData
        {
            contentList.Sort((x, y) => x.LastSaved.CompareTo(y.LastSaved));
            return contentList;
        }

        #endregion
    }
}