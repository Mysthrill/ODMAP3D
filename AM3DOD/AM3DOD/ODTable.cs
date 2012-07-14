using System;
using System.Collections.Generic;

namespace AM3DOD
{
    public class OdTable
    {
        public string Name { get; private set; }

        public string Description { get; private set; }

        public List<OdFieldDefinition> FieldDefinitions { get; private set; }


        public OdTable(string name, string description, List<OdFieldDefinition> fieldDefinitions)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (fieldDefinitions == null)
                throw new ArgumentNullException("fieldDefinitions");

            Name = name;
            Description = description;
            FieldDefinitions = fieldDefinitions;
        }
    }
}
