using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.DataModel;
using Codge.Generator.Common;

namespace Codge.Generator.StringTemplateTasks
{
    public class ModelTask : IOutputAction<Model>
    {
        public TaskInput TaskInput { get; private set; }

        public Model Model { get; private set; }

        public ModelTask(TaskInput taskInput, Model model)
        {
            TaskInput = taskInput;
            Model = model;
        }

        public PathAndContent Execute(Context context)
        {
            return TaskProcessor.Execute(TaskInput, new[] { new KeyValuePair<string, object>("model", Model) });
        }

        public bool IsApplicable()
        {
            return true;
        }
    }

}
