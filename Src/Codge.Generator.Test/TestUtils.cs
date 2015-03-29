using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Codge.Generator.Test
{
    public static class TestUtils
    {
        public static void AssertContent(string actualContent, string path, bool rebaseline)
        {
            string content = File.ReadAllText(path);

            if (content != actualContent)
            {
                if (rebaseline)
                {
                    File.WriteAllText(path, actualContent);
                }

                Assert.AreEqual(content, actualContent);
            }
        }
    }
}
