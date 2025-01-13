using PrintPress.UIService.Abstract;
using static System.Windows.Forms.ListView;

namespace PrintPress.UICommand.ContentCommand
{
    public class UpdateActiveContentCommand<T> : Command<T> where T : ContentService<T>
    {
        SelectedListViewItemCollection _items;
        public UpdateActiveContentCommand(T contentService, SelectedListViewItemCollection items) : base(contentService)
        {
            _items = items;
        }
        protected override bool TryExecute()
        {
            if (_items != null && _items.Count > 0)
            {
                object? contentIdObj = _items[0].Tag;
                if (contentIdObj == null)
                {
                    return false;
                }
                if (contentIdObj is not int)
                {
                    return false;
                }
                Service.ActiveID = (int)contentIdObj;
            }
            return true;
        }
    }
}