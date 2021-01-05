using System.Collections.Generic;

namespace Codge.DataModel.Descriptors
{
    public class ItemDescriptor
    {
        public string Name { get; }
        public int? Value { get; }

        public ItemDescriptor(string name, int value)
            : this(name, (int?)value)
        { }

        public ItemDescriptor(string name)
            : this(name, null)
        { }

        private ItemDescriptor(string name, int? value)
        {
            Name = name;
            Value = value;
        }
    }

    public class EnumerationTypeDescriptor : TypeDescriptor
    {
        private List<ItemDescriptor> _items;
        public IReadOnlyCollection<ItemDescriptor> Items => _items;

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
