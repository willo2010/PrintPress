using PrintPress.Data;
using PrintPress.UIService;
using PrintPress.UIService.Abstract;

namespace PrintPress.UICommand.ContentCommand
{
    public class UpdateSourcesCommand : Command<JournalismService>
    {
        private string _newText = string.Empty;
        private string _oldText = string.Empty;
        private StoryData? _content;
        public UpdateSourcesCommand(JournalismService contentService, string newText) : base(contentService, true) 
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
            _oldText = _content.Sources;
            _content.Sources = _newText;
            return true;
        }
        protected override bool TryUndoExecute()
        {
            if (_content == null)
            {
                return false;
            }
            _content.Sources = _oldText;
            return true;
        }
    }
}