using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.Generator
{
    public interface IOutputPathMapper
    {
        string MapPath(ItemInformation itemInfo, Context context);
    }

    public class OutpuPathMapper : IOutputPathMapper
    {
        public string MapPath(ItemInformation itemInfo, Context context)
        {
            string path = itemInfo.Item;
            if(string.IsNullOrEmpty(itemInfo.Category))
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

            if(!string.IsNullOrEmpty(category))
            {
                path = Path.Combine(category, path);
            }
            if(extension != null)
            {
                path += extension;
            }

            return path;
        }
    }
}
