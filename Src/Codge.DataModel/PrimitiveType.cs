using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.DataModel
{
    //string, int, int64, 
    public class PrimitiveType : TypeBase
    {
        internal PrimitiveType(int id, string name, Namespace nameSpace)
            : base(id, name, nameSpace)
        {
        }

        public override IEnumerable<TypeBase> Dependencies
        {
            get
            {
                return Enumerable.Empty<TypeBase>();
            }
        }
    }

}
