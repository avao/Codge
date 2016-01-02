using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Qart.Core.Validation;
using Qart.Core.Io;
using Codge.Generator.Common;


namespace Codge.Models.Common
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

        public void Execute(Context context)
        {
            if (!Action.IsApplicable())
                return;
            var pathAndContent = Action.Execute(context);
            Require.NotNullOrEmpty(pathAndContent.ItemInfo.Item);

            //TODO should mapper return absolute path?
            string relativePath = context.PathMapper.MapPath(pathAndContent.ItemInfo, context);
            string path = context.GetAbsolutePath(relativePath);

            if (File.Exists(path) && File.ReadAllText(path) == pathAndContent.Content)
            {//same content 
                context.Tracker.OnFileSkipped(relativePath);
            }
            else
            {//did not exist or has changed
                FileUtils.WriteAllText(path, pathAndContent.Content);
                context.Tracker.OnFileUpdated(relativePath);
            }
        }
    }
}
