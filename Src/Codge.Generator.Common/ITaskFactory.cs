using Codge.DataModel;
using System.Collections.Generic;

namespace Codge.Generator.Common
{
    public interface ITaskFactory
    {
        IEnumerable<ITask<Model>> CreateTasksForModel(Model model);
        IEnumerable<ITask<Namespace>> CreateTasksForNamespace(Namespace model);
        IEnumerable<ITask<TypeBase>> CreateTasksForType(TypeBase type);
    }
}
