using PrintPress.Controller.Data;
using PrintPress.Data.Enum;
using PrintPress.UIService;
using PrintPress.UIService.Abstract;

namespace PrintPress.UIWindow.Interface
{
    internal interface IConcreteClientWindow<T> where T : ClientService<T>
    {
        public T Service { get; init; }
        public Department Type { get { return Service.Type; } }
        public string Name
        {
            get
            {
                if (DataTools.DepartmentString(Type, out string name))
                    return name;
                throw new NotImplementedException("Client service does not implement a department type.");
            }
        }
    }
}
