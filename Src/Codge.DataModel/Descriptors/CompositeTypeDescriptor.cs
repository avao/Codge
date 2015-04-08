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
        public IDictionary<string, object> AttachedData { get; private set; }

        public FieldDescriptor(string name, string typeName, bool isCollection)
            : this(name, typeName, isCollection, new Dictionary<string, object>())
        { }

        public FieldDescriptor(string name, string typeName, bool isCollection, IDictionary<string, object> attachedData)
        {
            Name = name;
            TypeName = typeName;
            IsCollection = isCollection;
            AttachedData = attachedData;
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

        public void AddField(string name, string fullyQualifiedTypeName, IDictionary<string, object> attachedData)
        {
            _fields.Add(new FieldDescriptor(name, fullyQualifiedTypeName, false, attachedData));
        }

        public void AddCollectionField(string name, string fullyQualifiedTypeName, IDictionary<string, object> attachedData)
        {
            _fields.Add(new FieldDescriptor(name, fullyQualifiedTypeName, true, attachedData));
        }
    }

    public static class CompositeTypeDescriptorExtentions
    {
        public static void AddField(this CompositeTypeDescriptor descriptor, string name, string fullyQualifiedTypeName)
        {
            descriptor.AddField(name, fullyQualifiedTypeName, new Dictionary<string, object>());
        }

        public static void AddCollectionField(this CompositeTypeDescriptor descriptor, string name, string fullyQualifiedTypeName)
        {
            descriptor.AddCollectionField(name, fullyQualifiedTypeName, new Dictionary<string, object>());
        }
    }
}
