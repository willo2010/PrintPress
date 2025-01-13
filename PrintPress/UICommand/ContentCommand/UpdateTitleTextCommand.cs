using PrintPress.Data;
using PrintPress.UIService.Abstract;

namespace PrintPress.UICommand.ContentCommand
{
    public class UpdateTitleTextCommand<T> : Command<T> where T : ContentService<T>
    {
        private string _newText = string.Empty;
        private string _oldText = string.Empty;
        private ContentData? _content;
        public UpdateTitleTextCommand(T contentService, string newText) : base(contentService, true)
        {
            _newText = newText;
        }
        protected override bool TryExecute()
        {
            _content = Service.ActiveContent;
            if (_content == null)
            {
                return false;
            }

            _oldText = _content.Title;
            _content.Title = _newText;
            return true;
        }
        protected override bool TryUndoExecute()
        {
            if (_content == null)
            {
                return false;
            }
            _content.Title = _oldText;
            return true;
        }
    }
}