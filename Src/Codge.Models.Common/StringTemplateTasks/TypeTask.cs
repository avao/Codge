using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.DataModel;
using Codge.Generator.Common;

namespace Codge.Generator.StringTemplateTasks
{
    public class TypeTask : IOutputAction<TypeBase>
    {
        public TaskInput TaskInput { get; private set; }
        public TypeBase Descriptor { get; private set; }

        public TypeTask(TaskInput taskInput, TypeBase descriptor)
        {
            TaskInput = taskInput;
            Descriptor = descriptor;
        }

        public PathAndContent Execute(Context context)
        {
            return TaskProcessor.Execute(TaskInput, new[] {
                new KeyValuePair<string, object>("model", Descriptor.Namespace.TypeSystem),
                new KeyValuePair<string, object>("namespace", Descriptor.Namespace),
                new KeyValuePair<string, object>("type", Descriptor)});
        }


        public bool IsApplicable()
        {
            return TaskProcessor.IsApplicable(TaskInput, new[] {
                new KeyValuePair<string, object>("model", Descriptor.Namespace.TypeSystem),
                new KeyValuePair<string, object>("namespace", Descriptor.Namespace),
                new KeyValuePair<string, object>("type", Descriptor)});
        }
    }

}
