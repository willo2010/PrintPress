using PrintPress.UIService.Abstract;

namespace PrintPress.UICommand.ContentCommand
{
    public class RedoCommand<T> : Command<T> where T : ClientService<T>
    {
        public RedoCommand(T clientService): base(clientService, false, false)
        {
            
        }

        protected override bool TryExecute()
        {
            Command<T>? command;

            while (Service.UndoHistory.TryPop(out command))
            {
                if (command != null && command.Undoable)
                {
                    command.UndoExecute();
                    break;
                }
            }
            return false;
        }

        protected override bool TryUndoExecute()
        {
            return false;
        }
    }
}