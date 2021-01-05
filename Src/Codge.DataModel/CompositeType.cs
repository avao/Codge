using System.Collections.Generic;
using System.Linq;
using static Codge.DataModel.CompositeType;

namespace Codge.DataModel
{
    public interface IExtendableType
    {
        CompositeType BaseType { get; }
    }


    public class CompositeType : TypeBase, IExtendableType
    {
        public class Field
        {
            public string Name { get; }
            public TypeBase Type { get; }

            public int Id { get; }

            public int MinOccur { get; }
            public int MaxOccur { get; }

            public IDictionary<string, object> AttachedData { get; }

            public CompositeType ContainingType { get; }

            internal Field(string name, int id, TypeBase type, IDictionary<string, object> attachedData, CompositeType containingType)
                : this(name, id, type, 1, 1, attachedData, containingType)
            {
            }

            internal Field(string name, int id, TypeBase type, int minOccur, int maxOccur, IDictionary<string, object> attachedData, CompositeType containingType)
            {
                Type = type;
                Name = name;
                Id = id;
                MinOccur = minOccur;
                MaxOccur = maxOccur;
                AttachedData = attachedData;
                ContainingType = containingType;
            }

            public bool IsCollection { get { return MaxOccur > 1; } }
        }

        private IList<Field> _fields;
        public IEnumerable<Field> Fields => _fields;

        public CompositeType BaseType { get; private set; }


        internal CompositeType(string name, Namespace nameSpace)
            : base(name, nameSpace)
        {
            _fields = new List<Field>();
        }

        public void AddBase(CompositeType baseType)
        {
            BaseType = baseType;
        }

        public void AddField(string name, TypeBase type, IDictionary<string, object> attachedData)
        {
            _fields.Add(new Field(name, _fields.Count, type, attachedData, this));
        }

        public void AddCollectionField(string name, TypeBase type, IDictionary<string, object> attachedData)
        {
            _fields.Add(new Field(name, _fields.Count, type, 1, int.MaxValue, attachedData, this));
        }

        public override IEnumerable<TypeBase> Dependencies
        {
            get
            {
                var dependencies = Fields.SelectMany(field => field.Type.Dependencies.Concat(new[] { field.Type }));
                if (BaseType != null)
                    dependencies = dependencies.Concat(new[] { BaseType });
                return dependencies.ToList();
            }
        }
    }

    public static class CompositeTypeExtensions
    {
        public static IEnumerable<Field> GetAllFields(this CompositeType compositeType)
        {
            return compositeType.BaseType != null
                ? compositeType.BaseType.GetAllFields().Concat(compositeType.Fields)
                : compositeType.Fields;
        }
    }
}
