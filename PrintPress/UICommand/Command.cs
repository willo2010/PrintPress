using PrintPress.UIService;
using PrintPress.UIService.Abstract;

namespace PrintPress.UICommand
{
    public abstract class Command<T> where T : ClientService<T>
    {
        // Abstract property for command name
        public string Name { get { return GetType().Name; } }

        public bool Undoable { get; init; }
        protected bool AddToHistory { get; init; }
        protected T Service { get; init; }

        public Command(T clientService, bool undoable = false, bool addToHistory = true)
        {
            Undoable = undoable;
            Service = clientService;
            AddToHistory = addToHistory;
        }
        public void Execute()
        {
            if (TryExecute() && AddToHistory)
            {
                Service.ExecuteHistory.Push(this);
            }
        }
        protected abstract bool TryExecute();
        public void UndoExecute()
        {
            if (!Undoable)
            {
                return;
            }
            if (TryUndoExecute())
            {
                Service.UndoHistory.Push(this);
            }
        }
        protected virtual bool TryUndoExecute() 
        { 
            return false;
        }
    }
}