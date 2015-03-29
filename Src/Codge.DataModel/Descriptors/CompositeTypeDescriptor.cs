using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.DataModel.Descriptors
{
    public class FieldDescriptor
    {
        public string Name { get; private set; }
        public string TypeName { get; private set; }
        public bool IsCollection { get; private set; }

        public FieldDescriptor(string name, string typeName, bool isCollection)
        { 
            Name = name;
            TypeName =  typeName;
            IsCollection = isCollection;
        }
    }

    public class CompositeTypeDescriptor : TypeDescriptor
    {
        IList<FieldDescriptor> _fields;
        public IEnumerable<FieldDescriptor> Fields { get { return _fields; } }

        internal CompositeTypeDescriptor(string name, NamespaceDescriptor nameSpace)
            : base(name, nameSpace)
        {
            _fields = new List<FieldDescriptor>();
        }

        public void AddField(string name, string fullyQualifiedTypeName)
        {
            _fields.Add(new FieldDescriptor(name, fullyQualifiedTypeName, false));
        }

        public void AddCollectionField(string name, string fullyQualifiedTypeName)
        {
            _fields.Add(new FieldDescriptor(name, fullyQualifiedTypeName, true));
        }

    }
}
