using System;
using Autodesk.Gis.Map.Constants;

namespace AM3DOD
{
    public class OdFieldDefinition
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public DataType DataType { get; private set; }

        public OdFieldDefinition(string name, string description, DataType dataType)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");

            Name = name;
            Description = description;
            DataType = dataType;
        }
    }
}
