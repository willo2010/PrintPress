using PrintPress.Controller;

namespace PrintPressUnitTesting.DataController.Tools
{
    internal class MockCommercialDataController : CommercialDataController
    {
        public override void Initialise()
        {
            Initialise("MockCommercialData");
        }
    }
}