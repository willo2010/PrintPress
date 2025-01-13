using PrintPress.Data;
using PrintPress.UIService.Abstract;

namespace PrintPress.UICommand.ContentCommand
{
    public class UpdateNotesTextCommand<T> : Command<T> where T : ContentService<T>
    {
        private string _newText = string.Empty;
        private string _oldText = string.Empty;
        private ContentData? _content;
        public UpdateNotesTextCommand(T contentService, string newText) : base(contentService, true)
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
            _oldText = _content.Notes;
            _content.Notes = _newText;
            return true;
        }
        protected override bool TryUndoExecute()
        {
            Service.ActiveContent.Notes = _oldText;
            return true;
        }
    }
}