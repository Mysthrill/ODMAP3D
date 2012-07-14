using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.Gis.Map.Utilities;

namespace AM3DOD
{
    public class OdRecord
    {
        public DBObject DbObject { get; private set; }

        public List<MapValue> Values { get; private set; }

        //public OdTable OdTable { get; private set; }

        public OdRecord(DBObject dbObject)
        {
            if (dbObject == null)
                throw new ArgumentNullException();

            DbObject = dbObject;
            Values = new List<MapValue>();
        }
    }
}
