using PrintPress.Controller;
using PrintPress.Controller.Data;

namespace PrintPressUnitTesting.DataController.Tools
{
    public class MockDataController : DataController<MockDataController>
    {
        private MockDatabaseScema _schema;
        protected override MockDatabaseScema Tables { get { return _schema; } }

        public override void Initialise()
        {
            _schema = new MockDatabaseScema();
            Initialise("MockCommercialData");
        }

        public bool ExecuteNonQuery<C>(SqlCommandData<C> command)
        {
            return base.ExecuteNonQuery(command);
        }
    }
}