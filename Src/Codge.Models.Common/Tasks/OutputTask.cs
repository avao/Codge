using Codge.Generator.Common;
using Microsoft.Extensions.Logging;
using Qart.Core.Io;
using Qart.Core.Validation;
using System.IO;

namespace Codge.Models.Common
{
    public class OutputTask<T> : ITask<T>
    {
        public ILogger Logger { get; private set; }

        IOutputAction<T> Action;
        public OutputTask(IOutputAction<T> action, ILogger logger)
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
