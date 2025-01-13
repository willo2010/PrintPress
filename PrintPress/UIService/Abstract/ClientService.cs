using PrintPress.Controller.Data;
using PrintPress.Data;
using PrintPress.Data.Enum;
using PrintPress.UICommand;

namespace PrintPress.UIService.Abstract
{
    public abstract class ClientService<T> where T : ClientService<T>
    { 
        public EmployeeData ActiveEmployee { get; init; }
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
        public abstract Department Type { get; }
        public Stack<Command<T>> ExecuteHistory { get; init; }
        public Stack<Command<T>> UndoHistory { get; init; }

        public ClientService(EmployeeData employee)
        {
            ActiveEmployee = employee;
            ExecuteHistory = new Stack<Command<T>>();
            UndoHistory = new Stack<Command<T>>();
        }

        public void Close()
        {
            ExecuteHistory.Clear();
            UndoHistory.Clear();
        }
    }
}