using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.Gis.Map.ObjectData;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.Gis.Map.Project;
using Autodesk.AutoCAD.ApplicationServices;

namespace AM3DOD
{
    public interface IMapObjectData
    {
        /// <summary>
        /// Public Property. Get the Map Project.
        /// </summary>
        ProjectModel Project { get; }
        /// <summary>
        /// Public Property. Get the Document of the Map Project.
        /// </summary>
        Document Document { get; }

        /// <summary>
        /// Get the Object Data Table Records of a list of AcadObjects within a particular Object Data Table
        /// </summary>
        /// <param name="acadObjects">List of AcadObjects</param>
        /// <param name="oDTable">The Object Data Table where you want to find records on</param>
        /// <returns>List of ODRecords found on the Object Data Table</returns>
        List<Record> GetDataTableRecords(List<DBObject> acadObjects, Autodesk.Gis.Map.ObjectData.Table oDTable);
        /// <summary>
        /// Get the Object Data Table Records of a list of AcadObjects within all Object Data Tables of our Map
        /// </summary>
        /// <param name="acadObjects">List of AcadObjects</param>
        /// <returns>List of ODRecords</returns>
        List<Record> GetDataTableRecords(List<DBObject> acadObjects);
        /// <summary>
        /// Get the Object Data Table Records of a list of AcadObjects within all a particular Data Table of our Map
        /// </summary>
        /// <param name="acadObject">AcadObject we wish to get the records from</param>
        /// <param name="oDTable">The Object Data Table where you want to find records on</param>
        /// <returns>List of ODRecords tied to the AcadObject</returns>
        List<Record> GetDataTableRecords(DBObject acadObject, Autodesk.Gis.Map.ObjectData.Table oDTable);
        /// <summary>
        /// Get the Object Data Table Records of an AcadObject within all Object Data Tables of our Map
        /// </summary>
        /// <param name="acadObject">AcadObject we wish to get the records from</param>
        /// <returns>List of ODRecords tied to the AcadObject</returns>
        List<Record> GetDataTableRecords(DBObject acadObject);
        /// <summary>
        /// Gets a list of Object Data Table Names of the Map
        /// </summary>
        /// <returns>Returns a List of type string with the names of the Object Data Tables</returns>
        List<string> GetDataTableNames();
        /// <summary>
        /// Get the ODTables of the map
        /// </summary>
        /// <returns>A list of ODTables</returns>
        List<ODTable> GetDataTables();
        /// Create a Object Data Table within our Map
        /// </summary>
        /// <param name="table">Information about the new Table</param>
        /// <returns>A value indicating if the table got created succesfully.A -1 value equals an unsuccessfull creation.</returns>
        int CreateTable(ODTable table);
        /// <summary>
        /// Delete a Object Data Table within our Map
        /// </summary>
        /// <param name="table">Information about the Table to be deleted</param>
        /// <returns>A value indicating if the table got deleted succesfully. A -1 value equals an unsuccessfull removal of the Table</returns>
        int DeleteTable(ODTable table);
        /// <summary>
        /// Rename an ODTable in the Map
        /// </summary>
        /// <param name="table">ODTable to be renamed</param>
        /// <returns>-1 if renaming failed . 0 Table doesn't exixt. 1 on a successfull rename</returns>
        int RenameTable(ODTable table, ODTable newTable);
        /// <summary>
        /// Check wheter the table exists inside Map
        /// </summary>
        /// <param name="table">The ODTable we want to check for its existance</param>
        /// <returns>True if the table exists</returns>
        bool TableExists(ODTable table);
        /// <summary>
        /// Create records within an ODTable in our Map
        /// </summary>
        /// <param name="ODRecords">A list of ODRecords to be added to the ODTable</param>
        /// <param name="ODTable">The ODTable to create records on</param>
        void CreateRecords(List<ODRecord> ODRecords, ODTable table);
        /// <summary>
        /// Delete records within an ODTable in our Map
        /// </summary>
        /// <param name="ODRecords">A list of ODRecords to be removed from the ODTable</param>
        /// <param name="oDTable">The ODTable to delete records on</param>
        void DeleteRecords(List<ODRecord> ODRecords, ODTable table);
        /// <summary>
        /// Update records within an ODTable in our Map
        /// </summary>
        /// <param name="ODRecords">A list of ODRecords to be inserted in the ODTable</param>
        /// <param name="oDTable">The ODTable to update records on</param>
        void UpdateRecords(List<ODRecord> ODRecords, ODTable table);
    }
}
