namespace Codge.Generator.Common
{
    public interface IOutputAction<T>
    {
        PathAndContent Execute(Context context);
        bool IsApplicable();
    }
}
