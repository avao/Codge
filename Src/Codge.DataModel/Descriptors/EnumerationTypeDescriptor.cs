using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.DataModel.Descriptors
{

    public class ItemDescriptor
    {
        public string Name { get; private set; }
        public int? Value { get; private set; }

        public ItemDescriptor(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public ItemDescriptor(string name)
        {
            Name = name;
            Value = null;
        }
    }

    public class EnumerationTypeDescriptor : TypeDescriptor
    {
        IList<ItemDescriptor> _items;
        public IEnumerable<ItemDescriptor> Items { get { return _items; } }

        internal EnumerationTypeDescriptor(string name, NamespaceDescriptor nameSpace)
            : base(name, nameSpace)
        {
            _items = new List<ItemDescriptor>();
        }

        public void AddItem(string name)
        {
            _items.Add(new ItemDescriptor(name));
        }

        public void AddItem(string name, int value)
        {
            _items.Add(new ItemDescriptor(name, value));
        }

    }
}
