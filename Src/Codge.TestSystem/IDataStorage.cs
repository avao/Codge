using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.TestSystem
{
    public interface IDataStorage
    {
        string GetContent(string testCaseId, string itemId);

        void PutContent(string testCaseId, string itemId, string content);
    }
}
