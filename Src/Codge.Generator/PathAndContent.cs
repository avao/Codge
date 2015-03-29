using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.Generator
{
    public class PathAndContent
    {
        public string Path { get; private set; }
        public string Content { get; private set; }

        public PathAndContent(string path, string content)
        {
            Path = path;
            Content = content;
        }

    }
}
