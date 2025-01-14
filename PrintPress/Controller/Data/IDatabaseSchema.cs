using System;

namespace PrintPress.Controller.Data
{
    /// <summary>
    /// Represents the interface for database schemas in the PrintPress application.
    /// All implementing classes must provide access to an array of table schemas.
    /// </summary>
    public interface IDatabaseSchema
    {
        #region Properties

        /// <summary>
        /// Gets an array of all table schemas defined in the database schema.
        /// </summary>
        public TableSchema[] AllTables { get; }

        #endregion
    }
}
