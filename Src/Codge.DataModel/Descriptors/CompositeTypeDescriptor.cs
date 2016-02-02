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

        public CompositeTypeDescriptor(string name, NamespaceDescriptor nameSpace)
            : base(name, nameSpace)
        {
            _fields = new List<FieldDescriptor>();
        }

        //TODO should isCollection be part of attached data?
        public FieldDescriptor AddField(string name, string fullyQualifiedTypeName, bool isCollection, IDictionary<string, object> attachedData, int position)
        {
            var field = new FieldDescriptor(name, fullyQualifiedTypeName, isCollection, attachedData);
            _fields.Insert(position, field);
            return field;
        }
    }

    public static class CompositeTypeDescriptorExtentions
    {
        public static FieldDescriptor AddField(this CompositeTypeDescriptor descriptor, string name, string fullyQualifiedTypeName, bool isCollection, IDictionary<string, object> attachedData)
        {
            return descriptor.AddField(name, fullyQualifiedTypeName, isCollection, attachedData, descriptor.Fields.Count());
        }
        public static FieldDescriptor AddField(this CompositeTypeDescriptor descriptor, string name, string fullyQualifiedTypeName, bool isCollection)
        {
            return descriptor.AddField(name, fullyQualifiedTypeName, isCollection, new Dictionary<string, object>());
        }
        public static FieldDescriptor AddField(this CompositeTypeDescriptor descriptor, string name, string fullyQualifiedTypeName, bool isCollection, int position)
        {
            return descriptor.AddField(name, fullyQualifiedTypeName, isCollection, new Dictionary<string, object>(), position);
        }
        public static FieldDescriptor AddField(this CompositeTypeDescriptor descriptor, string name, string fullyQualifiedTypeName)
        {
            return descriptor.AddField(name, fullyQualifiedTypeName, false);
        }
        public static FieldDescriptor GetField(this CompositeTypeDescriptor descriptor, string name)
        {
            return descriptor.Fields.SingleOrDefault(_ => _.Name == name);
        }
    }
}
