using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.DataModel;
using Codge.Generator.Common;

namespace Codge.Generator.StringTemplateTasks
{
    public class NamespaceTask : IOutputAction<Namespace>
    {
        public TaskInput TaskInput { get; private set; }
        public Namespace NamespaceDescriptor { get; private set; }

        public NamespaceTask(TaskInput taskInput, Namespace namespaceDescriptor)
        {
            TaskInput = taskInput;
            NamespaceDescriptor = namespaceDescriptor;
        }

        public PathAndContent Execute(Context context)
        {
            return TaskProcessor.Execute(TaskInput, new[] {
                new KeyValuePair<string, object>("model", NamespaceDescriptor.TypeSystem),
                new KeyValuePair<string, object>("namespace", NamespaceDescriptor)});
        }


        public bool IsApplicable()
        {
            return true;
        }
    }

}
