using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.DataModel
{
    public interface IExtendableType
    {
        IEnumerable<TypeBase> BaseClasses { get; }
    }


    public class CompositeType : TypeBase, IExtendableType
    {
        public class Field
        {
            public string Name { get; private set; }
            public TypeBase Type { get; private set; }

            public int Id { get; private set; }

            public int MinOccur { get; private set; }
            public int MaxOccur { get; private set; }

            internal Field(string name, int id, TypeBase type)
                :this(name, id, type, 1, 1)
            {
            }

            internal Field(string name, int id, TypeBase type, int minOccur, int maxOccur)
            {
                Type = type;
                Name = name;
                Id = id;
                MinOccur = minOccur;
                MaxOccur = maxOccur;
            }

            public bool IsCollection { get { return MaxOccur > 1; } }
        }

        private IList<Field> _fields;
        public IEnumerable<Field> Fields { get { return _fields; } }

        internal CompositeType(int id, string name, Namespace nameSpace)
            : base(id, name, nameSpace)
        {
            _fields = new List<Field>();
        }

        public void AddField(string name, TypeBase type)
        {
            _fields.Add(new Field(name, _fields.Count, type));
        }

        public void AddCollectionField(string name, TypeBase type)
        {
            _fields.Add(new Field(name, _fields.Count, type, 1, int.MaxValue));
        }

        public override IEnumerable<TypeBase> Dependencies
        {
            get
            {
                return Fields.SelectMany(field => field.Type.Dependencies.Concat(new TypeBase[] { field.Type })).ToList();
            }
        }

        public IEnumerable<TypeBase> BaseClasses
        {
            get { throw new NotImplementedException(); }
        }
    }


}
