using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.DatabaseServices;

namespace AM3DOD
{
    public class ODRecord
    {
        /// <summary>
        /// 
        /// </summary>
        private DBObject dBObject;
        public DBObject DBObject
        {
            get
            {
                return dBObject;
            }
            private set
            {
                dBObject = value;
            }
        }
        private List<Autodesk.Gis.Map.Utilities.MapValue> values;
        public List<Autodesk.Gis.Map.Utilities.MapValue> Values
        {
            get
            {
                return values;
            }
            private set
            {
                values = value;
            }
        }

        private ODTable odTable;
        public ODTable ODTable
        {
            get
            {
                return odTable;
            }
            private set
            {
                odTable = value;
            }
        }

        public ODRecord(DBObject dbObject)
        {
            if (dbObject == null)
                throw new ArgumentNullException();

            this.dBObject = dbObject;
            this.values = new List<Autodesk.Gis.Map.Utilities.MapValue>();
        }
    }
}
