using PrintPress.UIService.Abstract;

namespace PrintPress.UICommand.ContentCommand
{
    public class UndoCommand<T> : Command<T> where T : ClientService<T>
    {
        public UndoCommand(T clientService): base(clientService, false, false)
        {
            
        }

        protected override bool TryExecute()
        {
            Command<T>? command;

            while (Service.ExecuteHistory.TryPop(out command))
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