using System.Collections.Generic;
using System.Linq;

namespace Codge.DataModel
{
    public class EnumerationType : TypeBase
    {
        private IList<Item> _items;
        public IEnumerable<Item> Items => _items;

        public class Item
        {
            public string Name { get; }
            public int Value { get; }

            public Item(string name, int value)
            {
                Name = name;
                Value = value;
            }
        }


        internal EnumerationType(string name, Namespace nameSpace)
            : base(name, nameSpace)
        {
            _items = new List<Item>();
        }

        public Item AddItem(string name, int value)
        {
            var item = new Item(name, value);
            _items.Add(item);
            return item;
        }

        public Item AddItem(string name)
        {
            int value = _items.Count > 0
                ? _items[_items.Count - 1].Value + 1
                : 0;

            return AddItem(name, value);
        }

        public override IEnumerable<TypeBase> Dependencies => Enumerable.Empty<TypeBase>();
    }
}
