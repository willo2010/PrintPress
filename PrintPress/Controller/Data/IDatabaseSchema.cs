using System;
namespace PrintPress.Controller.Data
{
    public interface IDatabaseSchema
    {
        public TableSchema[] AllTables { get; }
    }
}
