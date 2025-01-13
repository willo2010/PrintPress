using PrintPress.Data;
using PrintPress.Data.Enum;
using PrintPress.UIService.Abstract;

namespace PrintPress.UICommand.ContentCommand
{
    public class UpdateContentStatusCommand<T> : Command<T> where T : ContentService<T>
    {
        ContentState _state;
        public UpdateContentStatusCommand(T contentService, int index) : base(contentService)
        {
            _state = (ContentState)index;
        }
        protected override bool TryExecute()
        {
            ContentData? contentData = Service.ActiveContent;
            if (contentData == null)
            {
                return false;
            }
            contentData.State = _state;
            return true;
        }
    }
}