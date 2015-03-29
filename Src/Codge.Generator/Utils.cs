using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.DataModel;

namespace Codge.Generator
{
    public static class Utils
    {
        public static string GetOutputPath(TypeBase type, string extension)
        {
            string path = string.Join(Path.DirectorySeparatorChar.ToString(), type.Namespace.GetParentNamespaces().Select(n => n.Name));
            return Path.Combine(path, type.Name + "." + extension);
        }

        public static string GetOutputPath(TypeBase type, string prefix, string extension)
        {
            return Path.Combine(prefix, GetOutputPath(type, extension));
        }
    
    }
}
