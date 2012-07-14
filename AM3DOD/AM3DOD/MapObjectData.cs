using System;
using System.Collections.Generic;
using Autodesk.Gis.Map.Project;
using Autodesk.Gis.Map.ObjectData;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.Gis.Map.Utilities;
using System.Collections;

namespace AM3DOD
{
    /// <summary>
    /// Class that will give you access to the map object data. 
    /// Create, Read, Update, Delete Tables. 
    /// Create, Read, Update, Delete Records.
    /// This Class Immplements the IMapObjectData Interface
    /// </summary>
    public class MapObjectData : IMapObjectData
    {
        #region Variables & Properties
        /// <summary>
        /// Private variable storing a Gis Map Document.
        /// </summary>
        private readonly Document _document;
        public Document Document
        {
            get
            {
                return _document;
            }
        }
        /// <summary>
        /// Private variable storing a Gis Map ProjectModel.
        /// </summary>
        private readonly ProjectModel _project;
        public ProjectModel Project
        {
            get
            {
                return _project;
            }
        }
        #endregion

        #region Constructor
        public MapObjectData(ProjectModel project)
        {
            _project = project;

            DocumentCollection acDocMgr = Application.DocumentManager;
            _document = acDocMgr.GetDocument(project.Database);
        }
        #endregion

        #region Table Specific Methods
        public List<string> GetDataTableNames()
        {
            List<string> list = new List<string>();

            //Run through all object data table names
            foreach (string s in _project.ODTables.GetTableNames())
            {
                //Add the tablename to the list
                list.Add(s);
            }

            return list;
        }

        public List<OdTable> GetDataTables()
        {
            //Declaration of list to store all mapped ODTables in
            List<OdTable> tables = new List<OdTable>();
            //Declaration of a list to store all TableNames in
            //Get the Data Table Names and store them in the list of Object Table Names
            List<string> names = GetDataTableNames();

            //Although you should never get a null value fromGetDataTableNames we first makes
            //sure the map actually has tables created before we continue mapping
            if (names != null)
            {
                //Foreach datatable found in the Maps list of Object Data Tables, map Table Info and Fielddefinition info
                for (int i = 0; i < names.Count; i++)
                {
                    //Declare and instantiate a new instance of a List containing FielDefinitions
                    List<OdFieldDefinition> fieldDefinitions = new List<OdFieldDefinition>();
                    //Foreach fielddefinition in the table we add a new instance of Fielddefinition containing all info
                    //of the fielddefinition
                    for (int j = 0; j < _project.ODTables[names[i]].FieldDefinitions.Count; j++)
                    {
                        fieldDefinitions.Add(new OdFieldDefinition(_project.ODTables[names[i]].FieldDefinitions[j].Name,
                                                                   _project.ODTables[names[i]].FieldDefinitions[j].Description,
                                                                   _project.ODTables[names[i]].FieldDefinitions[j].Type));
                    }
                    //Once all fielddefinitions are defined we add the table to our List of ODTables
                    tables.Add(new OdTable(_project.ODTables[names[i]].Name, _project.ODTables[names[i]].Description, fieldDefinitions));
                }
            }

            return tables;
        }

        public int CreateTable(OdTable table)
        {
            if (table == null)
                throw new ArgumentNullException();

            //Makes sure the table doesnt exist allready before you try to create it.
            if (!TableExists(table))
            {
                //Declaration. ObjectData.FieldDefinitions Object
                FieldDefinitions fd;
                //Apply a document lock before you make any changes to the Object Data Tables
                using (_document.LockDocument())
                {
                    //Run your code inside a transaction
                    using (Transaction tr = _project.Database.TransactionManager.StartTransaction())
                    {
                        //Instantiation of an ObjectData.FieldDefinitions object
                        fd = _project.MapUtility.NewODFieldDefinitions();

                        for (int i = 0; i < table.FieldDefinitions.Count; i++)
                        {
                            //Add one fielddefinition to the fieldefinitions object
                            fd.Add(table.FieldDefinitions[i].Name, table.FieldDefinitions[i].Description, table.FieldDefinitions[i].DataType, i);
                        }

                        Tables tables = _project.ODTables;
                        //Add the new table to the project OD Tables. I have no idea what the IsXDataRecord does!!
                        tables.Add(table.Name, fd, table.Description, true);
                        //Commit your changes you made to the map Tables
                        tr.Commit();

                        //Check wheter the addition of the table was successfull
                        if (TableExists(table))
                            return 1;
                        return -1;
                    }
                }
            }
            return 0;
        }

        public int DeleteTable(OdTable table)
        {
            if (table == null)
                throw new ArgumentNullException();

            //Check if the table exists
            if (TableExists(table))
            {
                //Lock your Map document before executing the deletion
                using (_document.LockDocument())
                {
                    //Run everything inside a transaction
                    using (Transaction tr = _project.Database.TransactionManager.StartTransaction())
                    {
                        //2. Remove the table if it exists
                        _project.ODTables.RemoveTable(table.Name);
                        //Commit your changes made to the Object Data Tables
                        tr.Commit();

                        //Check wheter deleting the table was successfull
                        if (!TableExists(table))
                        {
                            return 1;
                        }
                        return -1;
                    }
                }
            }
            return 0;
        }

        public int RenameTable(OdTable table, OdTable newTable)
        {
            if (table == null)
                throw new ArgumentNullException();

            //Check if the table exists
            if (TableExists(table))
            {
                //Lock your Map document before executing the deletion
                using (_document.LockDocument())
                {
                    //Run everything inside a transaction
                    using (Transaction tr = _project.Database.TransactionManager.StartTransaction())
                    {
                        //2. Rename the table if it exists
                        _project.ODTables.RenameTable(table.Name, newTable.Name);
                        //Commit your changes made to the Object Data Tables
                        tr.Commit();

                        //Check if renaming was succefull
                        if (TableExists(newTable))
                            return 1;
                        return -1;
                    }
                }
            }
            return 0;
        }

        public bool TableExists(OdTable table)
        {
            if (table == null)
                throw new ArgumentNullException();

            const bool exists = false;
            //Get all Object Table Names
            List<string> l = GetDataTableNames();

            //Foreach tableName in the list
            foreach (string s in l)
            {
                //Compare the ODTable name and the tableName from the list
                //return true if there is a match
                if (table.Name == s)
                    return true;
            }
            return exists;
        }
        #endregion

        #region Record Specific Methods
        public List<Record> GetDataTableRecords(List<DBObject> acadObjects, Autodesk.Gis.Map.ObjectData.Table oDTable)
        {
            throw new NotImplementedException();
        }

        public List<Record> GetDataTableRecords(List<DBObject> acadObjects)
        {
            throw new NotImplementedException();
        }

        public List<Record> GetDataTableRecords(DBObject acadObject, Autodesk.Gis.Map.ObjectData.Table oDTable)
        {
            throw new NotImplementedException();
        }

        public List<Record> GetDataTableRecords(DBObject acadObject)
        {
            throw new NotImplementedException();
        }

        public void CreateRecords(List<OdRecord> odRecords, OdTable odTable)
        {
            if (odRecords == null)
                throw new ArgumentNullException();
            if (odTable == null)
                throw new ArgumentNullException();

            //Create a reference to the corresponding ODTable of the Map project
            Autodesk.Gis.Map.ObjectData.Table table = _project.ODTables[odTable.Name];

            //Lock your document
            using (_document.LockDocument())
            {
                //Execute everything inside a transaction
                using (Transaction tr = _project.Database.TransactionManager.StartTransaction())
                {
                    //We need to reassign our BlockTable and BlockTableRecord so we can call our objects from the database.
                    BlockTable bt = tr.GetObject(_project.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                    if (bt != null)
                    {
                        BlockTableRecord modal = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;

                        foreach (OdRecord record in odRecords)
                        {
                            //Create an empty record using the static method ObjectData.Record.Create(). This does not define any fields for the record.
                            using (Record rec = Record.Create())
                            {
                                //Initialize a new record inside the project Object Data Table
                                table.InitRecord(rec);

                                //Foreach object inside the List of Values of the record object
                                //I will have to make sure the Values amount equals the columns defined within the table
                                //Before continueing and performing any actions
                                for (int i = 0; i < record.Values.Count; i++)
                                {
                                    //Set a reference to a cell inside the record
                                    MapValue val = rec[i];
                                    val.Assign(record.Values[i]);
                                }

                                //Foreach item in our modelspace we will add a new record
                                if (modal != null)
                                    foreach (ObjectId item in modal)
                                    {
                                        //If the Handle of an item in modelspace equals the handle of our AcadObject from the businesslayer
                                        if (item.Handle.Value == record.DbObject.Handle.Value)
                                        {
                                            //Get a new reference to the actual database object
                                            DBObject obj = tr.GetObject(item, OpenMode.ForRead);
                                            //Add the mapped record to the Map Project's specific Object DataTable
                                            table.AddRecord(rec, obj.ObjectId);
                                            //jump out loop if object is found in modal and dealt with
                                            break;
                                        }
                                    }
                            }
                        }
                    }
                    //If all went well, commit the changes made to the project Object DataTables
                    tr.Commit();
                }
            }
        }

        public void DeleteRecords(List<OdRecord> odRecords, OdTable oDTable)
        {
            if (odRecords == null)
                throw new ArgumentNullException();
            if (oDTable == null)
                throw new ArgumentNullException();

            //Check if the oDTable Exists
            if (TableExists(oDTable))
            {
                Autodesk.Gis.Map.ObjectData.Table table = _project.ODTables[oDTable.Name];

                //Use a document lock on your Map drawing
                using (_document.LockDocument())
                {
                    //Run everything inside a transaction
                    using (Transaction tr = _project.Database.TransactionManager.StartTransaction())
                    {
                        BlockTable bt = tr.GetObject(_project.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                        if (bt != null)
                        {
                            BlockTableRecord modal = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;

                            foreach (OdRecord record in odRecords)
                            {
                                if (modal != null)
                                    foreach (ObjectId item in modal)
                                    {
                                        if (item.Handle.Value == record.DbObject.Handle.Value)
                                        {
                                            using (Records recs = table.GetObjectTableRecords(0, item, Autodesk.Gis.Map.Constants.OpenMode.OpenForWrite, true))
                                            {
                                                //Makes sure you only delete the records in the list and not all records attached to the object
                                                //Will loop through all records and delete them
                                                IEnumerator ie = recs.GetEnumerator();
                                                ie.MoveNext();
                                                //Make sure the record in the records == the ODRecord in the list
                                                if (recs.CurrentObjectId.Handle.Value == record.DbObject.Handle.Value)
                                                {
                                                    //remove the record from the table.
                                                    recs.RemoveRecord();
                                                }
                                            }
                                            //jump out loop if object is found in modal and dealt with
                                            break;
                                        }
                                    }
                            }
                        }
                        //Commit the changes made to the table
                        tr.Commit();
                    }
                }
            }
        }

        public void UpdateRecords(List<OdRecord> odRecords, OdTable oDTable)
        {
            if (odRecords == null)
                throw new ArgumentNullException();
            if (oDTable == null)
                throw new ArgumentNullException();

            //Check if the oDTable Exists
            if (TableExists(oDTable))
            {
                Autodesk.Gis.Map.ObjectData.Table table = _project.ODTables[oDTable.Name];

                //Use a document lock on your Map drawing
                using (_document.LockDocument())
                {
                    //Run everything inside a transaction
                    using (Transaction tr = _project.Database.TransactionManager.StartTransaction())
                    {
                        BlockTable bt = tr.GetObject(_project.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                        if (bt != null)
                        {
                            BlockTableRecord modal = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForRead) as BlockTableRecord;

                            foreach (OdRecord record in odRecords)
                            {
                                if (modal != null)
                                    foreach (ObjectId item in modal)
                                    {
                                        if (item.Handle.Value == record.DbObject.Handle.Value)
                                        {
                                            using (Records recs = table.GetObjectTableRecords(0, item, Autodesk.Gis.Map.Constants.OpenMode.OpenForWrite, true))
                                            {
                                                foreach (Record rec in recs)
                                                {
                                                    if (rec.MapObjectId.ObjectHandle.Value == record.DbObject.Handle.Value)
                                                    {
                                                        //Foreach object inside the List of Values of the record object
                                                        //I will have to make sure the Values amount equals the columns defined within the table
                                                        //Before continueing and performing any actions
                                                        for (int i = 0; i < record.Values.Count; i++)
                                                        {
                                                            //Set a reference to a cell inside the record
                                                            MapValue val = rec[i];
                                                            //Assign a new value to the record
                                                            val.Assign(record.Values[i]);
                                                            recs.UpdateRecord(rec);
                                                        }
                                                    }
                                                }
                                            }
                                            //jump out loop if object is found in modal and dealt with
                                            break;
                                        }
                                    }
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
