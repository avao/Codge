using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.DataModel
{

    public class EnumerationType : TypeBase
    {

        private IList<Item> _items;
        public IEnumerable<Item> Items { get { return _items; } }

        public class Item
        {
            public string Name { get; private set; }
            public int Value { get; private set; }

            public Item(string name, int value)
            {
                Name = name;
                Value = value;
            }
        }


        internal EnumerationType(int id, string name, Namespace nameSpace)
            : base(id, name, nameSpace)
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

        public override IEnumerable<TypeBase> Dependencies
        {
            get
            {
                return Enumerable.Empty<TypeBase>();
            }
        }

    }
}
