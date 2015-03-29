using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;


namespace Codge.Generator.Tasks
{
    public class OutputTask<T> : ITask<T>
    {
        public ILog Logger { get; private set; }

        IOutputAction<T> Action;
        public OutputTask(IOutputAction<T> action, ILog logger)
        {
            Action = action;
            Logger = logger;
        }

        public bool IsApplicable()
        {
            return Action.IsApplicable();
        }

        public void Execute(Context context)
        {
            var pathAndContent = Action.Execute(context);

            if (string.IsNullOrEmpty(pathAndContent.Path))
            {
                throw new Exception("aa");//TODO
            }

            string path = context.GetAbsolutePath(pathAndContent.Path);

            if (File.Exists(path) && File.ReadAllText(path) == pathAndContent.Content)
            {//same content 
                //TODO hashing?
                context.Tracker.OnFileSkipped(pathAndContent.Path);
            }
            else
            {//did not exist or has changed
                string dirPath = Path.GetDirectoryName(path);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                context.Tracker.OnFileUpdated(pathAndContent.Path);
                File.WriteAllText(path, pathAndContent.Content);
            }
        }
    }
}
