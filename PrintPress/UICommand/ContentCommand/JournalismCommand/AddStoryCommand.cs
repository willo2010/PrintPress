using PrintPress.Controller;
using PrintPress.UIService;

namespace PrintPress.UICommand.ContentCommand.Journalism
{
    public class AddStoryCommand : Command<JournalismService>
    {

        public AddStoryCommand(JournalismService service) : base(service, false) { }

        protected override bool TryExecute()
        {
            CommercialDataController.Instance.AllocateStory(Service.ActiveEmployee.Id, out int autoId);
            Service.LoadStoryList();
            Service.ActiveID = autoId;
            return true;
        }
        protected override bool TryUndoExecute()
        {
            return false;
        }
    }
}
