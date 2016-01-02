using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.Generator.Common
{
    public interface IOutputAction<T>
    {
        PathAndContent Execute(Context context);
        bool IsApplicable();
    }
}
