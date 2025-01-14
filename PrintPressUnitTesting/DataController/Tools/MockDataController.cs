using PrintPress.Controller;
using PrintPress.Controller.Data;

namespace PrintPressUnitTesting.DataController.Tools
{
    public class MockDataController : DataController<MockDataController>
    {
        private MockDatabaseSchema _schema;
        protected override MockDatabaseSchema Tables { get { return _schema; } }

        public override void Initialise()
        {
            _schema = new MockDatabaseSchema();
            Initialise("MockCommercialData");
        }

        public bool ExecuteNonQuery<C>(SqlCommandData<C> command)
        {
            return base.ExecuteNonQuery(command);
        }
    }
}