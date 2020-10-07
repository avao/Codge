namespace Codge.Generator.Common
{
    public class PathAndContent
    {
        public ItemInformation ItemInfo { get; }
        public string Content { get; }

        public PathAndContent(ItemInformation itemInfo, string content)
        {
            ItemInfo = itemInfo;
            Content = content;
        }
    }

    public class ItemInformation
    {
        public string Item { get; }
        public string Category { get; }

        public ItemInformation(string item, string category)
        {
            Item = item;
            Category = category;
        }
    }
}
