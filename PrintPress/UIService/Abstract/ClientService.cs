using PrintPress.Controller.Data;
using PrintPress.Data;
using PrintPress.Data.Enum;
using PrintPress.UICommand;

namespace PrintPress.UIService.Abstract
{
    /// <summary>
    /// Represents an abstract base class for client services, managing the operations, commands, and user context within a specific department.
    /// </summary>
    /// <typeparam name="T">The specific type of the client service inheriting from this base class.</typeparam>
    public abstract class ClientService<T> where T : ClientService<T>
    {
        #region Properties

        /// <summary>
        /// Gets the active employee associated with this client service.
        /// </summary>
        public EmployeeData ActiveEmployee { get; init; }

        /// <summary>
        /// Gets the name of the department associated with this client service.
        /// </summary>
        /// <exception cref="Exception">Thrown if the department does not have a valid name assigned.</exception>
        public string Name
        {
            get
            {
                if (DataTools.DepartmentString(Type, out var name))
                {
                    return name;
                }
                throw new Exception("Department does not have a valid name assigned");
            }
        }

        /// <summary>
        /// Gets the type of department associated with this client service.
        /// </summary>
        public abstract Department Type { get; }

        /// <summary>
        /// Gets the stack of executed commands for this client service.
        /// </summary>
        public Stack<Command<T>> ExecuteHistory { get; init; }

        /// <summary>
        /// Gets the stack of undone commands for this client service.
        /// </summary>
        public Stack<Command<T>> UndoHistory { get; init; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService{T}"/> class.
        /// </summary>
        /// <param name="employee">The active employee associated with this client service.</param>
        public ClientService(EmployeeData employee)
        {
            ActiveEmployee = employee;
            ExecuteHistory = new Stack<Command<T>>();
            UndoHistory = new Stack<Command<T>>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Clears all command histories for this client service.
        /// </summary>
        public void Close()
        {
            ExecuteHistory.Clear();
            UndoHistory.Clear();
        }

        #endregion
    }
}
