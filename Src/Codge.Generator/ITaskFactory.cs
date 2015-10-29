using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.DataModel;

namespace Codge.Generator
{
    public interface ITaskFactory
    {
        IEnumerable<ITask<Model>> CreateTasksForModel(Model model, IModelBehaviour modelBehaviour);
        IEnumerable<ITask<Namespace>> CreateTasksForNamespace(Namespace model, IModelBehaviour modelBehaviour);
        IEnumerable<ITask<TypeBase>> CreateTasksForType(TypeBase type, IModelBehaviour modelBehaviour);
    }
}
