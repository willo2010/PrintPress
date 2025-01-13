using PrintPress.Controller;
using PrintPress.UIService;

namespace PrintPress.UICommand.ContentCommand.Journalism
{
    public class SaveActiveStoryCommand : Command<JournalismService>
    {
        public SaveActiveStoryCommand(JournalismService service)  : base(service) { }

        protected override bool TryExecute()
        {
            if (Service.ActiveContent == null) return false;
            CommercialDataController.Instance.UpdateStory(Service.ActiveContent);
            Service.ActiveContent.LocalChanges = false;
            Service.LoadStoryList();
            return true;
        }

    }
}
