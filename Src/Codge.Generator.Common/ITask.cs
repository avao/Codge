namespace Codge.Generator.Common
{
    public interface ITask<T>
    {
        void Execute(Context context);
    }
}
