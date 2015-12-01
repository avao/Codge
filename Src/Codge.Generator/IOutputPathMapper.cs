
namespace Codge.Generator
{
    public interface IOutputPathMapper
    {
        string MapPath(ItemInformation itemInfo, Context context);
    }
}
