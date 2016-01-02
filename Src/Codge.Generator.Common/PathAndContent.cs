using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.Generator.Common
{
    public class PathAndContent
    {
        public ItemInformation ItemInfo { get; private set; }
        public string Content { get; private set; }

        public PathAndContent(ItemInformation itemInfo, string content)
        {
            ItemInfo = itemInfo;
            Content = content;
        }
    }

    public class ItemInformation
    {
        public string Item{get; private set;}
        public string Category{get; private set;}

        public ItemInformation(string item, string category)
        {
            Item = item;
            Category = category;
        }
    }
}
