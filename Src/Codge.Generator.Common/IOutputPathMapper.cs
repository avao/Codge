namespace Codge.Generator.Common
{
    public interface IOutputPathMapper
    {
        string MapPath(ItemInformation itemInfo, Context context);
    }
}
