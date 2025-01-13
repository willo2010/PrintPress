using PrintPress.UI;
using PrintPress.UIService;
using PrintPress.UIService.Abstract;
using System.Diagnostics;

namespace PrintPress.UICommand.ContentCommand
{
    public class ExitCommand<T> : Command<T> where T : ClientService<T>
    {
        ClientWindow window;
        public ExitCommand(T clientService, ClientWindow clientWindow): base(clientService)
        {
            window = clientWindow;
        }

        protected override bool TryExecute()
        {
            try
            {
                window.Close();
                Service.Close();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
        protected override bool TryUndoExecute()
        {
            return false;
        }
    }
}