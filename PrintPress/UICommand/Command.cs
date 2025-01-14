using PrintPress.UIService;
using PrintPress.UIService.Abstract;

namespace PrintPress.UICommand
{
    /// <summary>
    /// Abstract base class representing a generic command in the PrintPress application.
    /// Provides execution and undo functionality with support for maintaining command history.
    /// </summary>
    /// <typeparam name="T">Type of the client service associated with the command.</typeparam>
    public abstract class Command<T> where T : ClientService<T>
    {
        #region Properties

        /// <summary>
        /// Gets the name of the command, which defaults to the class name.
        /// </summary>
        public string Name { get { return GetType().Name; } }

        /// <summary>
        /// Indicates whether the command supports undo functionality.
        /// </summary>
        public bool Undoable { get; init; }

        /// <summary>
        /// Indicates whether the command should be added to the execution history.
        /// </summary>
        protected bool AddToHistory { get; init; }

        /// <summary>
        /// The client service associated with this command.
        /// </summary>
        protected T Service { get; init; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Command{T}"/> class.
        /// </summary>
        /// <param name="clientService">The client service associated with this command.</param>
        /// <param name="undoable">Specifies whether the command supports undo functionality.</param>
        /// <param name="addToHistory">Specifies whether the command should be added to the execution history.</param>
        public Command(T clientService, bool undoable = false, bool addToHistory = true)
        {
            Undoable = undoable;
            Service = clientService;
            AddToHistory = addToHistory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Executes the command. If successful and history addition is enabled, the command is added to the service's execution history.
        /// </summary>
        public void Execute()
        {
            if (TryExecute() && AddToHistory)
            {
                Service.ExecuteHistory.Push(this);
            }
        }

        /// <summary>
        /// Undoes the command execution if undo functionality is enabled.
        /// </summary>
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

        #endregion

        #region Protected Methods

        /// <summary>
        /// Attempts to execute the command. Must be implemented in derived classes.
        /// </summary>
        /// <returns>True if the execution was successful; otherwise, false.</returns>
        protected abstract bool TryExecute();

        /// <summary>
        /// Attempts to undo the command execution. Can be overridden in derived classes.
        /// </summary>
        /// <returns>True if the undo operation was successful; otherwise, false.</returns>
        protected virtual bool TryUndoExecute()
        {
            return false;
        }

        #endregion
    }
}
