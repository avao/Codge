using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.DataModel;

namespace Codge.Generator.Common
{
    public interface ITaskFactory
    {
        IEnumerable<ITask<Model>> CreateTasksForModel(Model model);
        IEnumerable<ITask<Namespace>> CreateTasksForNamespace(Namespace model);
        IEnumerable<ITask<TypeBase>> CreateTasksForType(TypeBase type);
    }
}
