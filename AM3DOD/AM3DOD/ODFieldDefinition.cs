using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AM3DOD
{
    public class ODFieldDefinition
    {
        private string name;
        public string Name 
        { 
            get
            {
                return name;
            }
            private set
            {
                name  = value;
            }
        }
        private string description;
        public string Description 
        {  
            get
            {
                return description;
            }
            private set
            {
                description  = value;
            } 
        }
        private Autodesk.Gis.Map.Constants.DataType dataType;
        public Autodesk.Gis.Map.Constants.DataType DataType 
        {
            get
            {
                return dataType;
            }
            private set
            {
                dataType  = value;
            }  
        }

        public ODFieldDefinition(string name, string description, Autodesk.Gis.Map.Constants.DataType dataType)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name of FieldDefinition can't be null or empty");

            this.name = name;
            this.description = description;
            this.dataType = dataType;
        }
    }
}
