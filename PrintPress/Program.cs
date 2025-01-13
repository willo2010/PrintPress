using PrintPress.Controller;

namespace PrintPress
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CommercialDataController.Instance.Initialise();
            ClassifiedDataController.Instance.Initialise();

            ApplicationConfiguration.Initialize();
            Application.Run(new Launcher());
        }
    }
}