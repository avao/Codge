using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.Generator
{
    public interface ITask<T>
    {
        bool IsApplicable();
        void Execute(Context context);
    }
}
