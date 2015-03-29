using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.TestSystem.FileBased
{
    public class DataStorage : IDataStorage
    {
        public string BasePath { get; private set; }

        public DataStorage(string basePath)
        {
            BasePath = basePath;
        }

        public string GetContent(string testCaseId, string itemId)
        {
            string absolutePath = GetAbsolutePath(testCaseId, itemId);
            if (File.Exists(absolutePath))
            {
                return File.ReadAllText(absolutePath);
            }
            return string.Empty;
        }

        public void PutContent(string testCaseId, string itemId, string content)
        {
            string absolutePath = GetAbsolutePath(testCaseId, itemId);
            string dir = Path.GetDirectoryName(absolutePath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.WriteAllText(absolutePath, content);
        }


        private string GetAbsolutePath(string testCaseId, string itemId)
        {
            return Path.Combine(BasePath, testCaseId, itemId);
        }

    }
}
