using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.Generator.StringTemplateTasks
{
    class TaskProcessor
    {
        static public PathAndContent Execute(TaskInput taskInput, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            if (taskInput.OutputPath.GetAttributes()!=null)
            {
                foreach (var kvp in taskInput.OutputPath.GetAttributes().ToList())
                {
                    taskInput.OutputPath.Remove(kvp.Key);
                    taskInput.Content.Remove(kvp.Key);
                }
            }

            foreach(var kvp in parameters)
            {
                taskInput.OutputPath.Add(kvp.Key, kvp.Value);
                taskInput.Content.Add(kvp.Key, kvp.Value);
            }

            return new PathAndContent(taskInput.OutputPath.Render(), taskInput.Content.Render());
        }


        static public bool IsApplicable(TaskInput taskInput, IEnumerable<KeyValuePair<string, object>> parameters)
        {
            if (taskInput.IsApplicable.GetAttributes() != null)
            {
                foreach (var kvp in taskInput.IsApplicable.GetAttributes().ToList())
                {
                    taskInput.IsApplicable.Remove(kvp.Key);
                }
            }

            foreach (var kvp in parameters)
            {
                taskInput.IsApplicable.Add(kvp.Key, kvp.Value);
            }

            string result = taskInput.IsApplicable.Render();
            return string.Equals(result, "true", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
