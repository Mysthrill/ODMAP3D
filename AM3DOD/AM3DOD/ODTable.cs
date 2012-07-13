using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AM3DOD
{
    public class ODTable
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
                name = value;
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
                description = value;
            }
        }
        private List<ODFieldDefinition> fieldDefinitions;
        public List<ODFieldDefinition> FieldDefinitions
        {
            get
            {
                return fieldDefinitions;
            }
            private set
            {
                fieldDefinitions = value;
            }
        }


        public ODTable(string name, string description, List<ODFieldDefinition> fieldDefinitions)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name of the Object Data Table can't be null or empty");
            if (fieldDefinitions == null)
                throw new ArgumentNullException("Fielddefinition should be set to an instance of an object");

            this.name = name;
            this.description = description;
            this.fieldDefinitions = fieldDefinitions;
        }
    }
}
