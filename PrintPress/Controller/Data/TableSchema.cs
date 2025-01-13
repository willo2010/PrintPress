using System.Data;

namespace PrintPress.Controller.Data
{
    public struct TableSchema
    {
        public string Name { get; init; }
        public string[][] Columns { get; init; }
        public TableSchema(string name, string[][] columns)
        {
            Name = name;
            Columns = columns;
        }
    }
}
