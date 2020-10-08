using System.Collections.Generic;
using System.Linq;

namespace Codge.DataModel.Descriptors
{
    public class FieldDescriptor
    {
        public string Name { get; }
        public string TypeName { get; }
        public bool IsCollection { get; }
        public IDictionary<string, object> AttachedData { get; }

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
        public IEnumerable<FieldDescriptor> Fields => _fields;

        public string BaseTypeName { get; }
        public string BaseTypeNamespace { get; }

        public CompositeTypeDescriptor(string name, NamespaceDescriptor nameSpace)
            : this(name, nameSpace, null, null)
        { }

        public CompositeTypeDescriptor(string name, NamespaceDescriptor nameSpace, string baseTypeName, string baseTypeNamespace)
            : base(name, nameSpace)
        {
            _fields = new List<FieldDescriptor>();
            BaseTypeName = baseTypeName;
            BaseTypeNamespace = baseTypeNamespace;
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
