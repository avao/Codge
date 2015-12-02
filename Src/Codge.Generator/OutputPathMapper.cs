using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.Generator
{

    public class OutpuPathMapper : IOutputPathMapper
    {
        private ISet<string> _processedFiles = new HashSet<string>();

        public string MapPath(ItemInformation itemInfo, Context context)
        {
            string path = itemInfo.Item;
            if (string.IsNullOrEmpty(itemInfo.Category))
            {
                return path;
            }
            path = path.Replace('.', Path.DirectorySeparatorChar);

            string extension = null;
            string category = itemInfo.Category;
            int pos = itemInfo.Category.LastIndexOf('.');
            if (pos != -1)
            {
                extension = itemInfo.Category.Substring(pos);
                category = itemInfo.Category.Substring(0, pos);
            }

            if (!string.IsNullOrEmpty(category))
            {
                path = Path.Combine(category, path);
            }
            if (extension != null)
            {
                path += extension;
            }

            var pathLower = path.ToLower();
            if (_processedFiles.Contains(pathLower))
            {//the same path was already used so need to change it
                path = Path.Combine(Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path) + path.GetHashCode() + Path.GetExtension(path));
                _processedFiles.Add(path.ToLower());
            }
            else
            {
                _processedFiles.Add(pathLower);
            }

            return path;
        }
    }

}
