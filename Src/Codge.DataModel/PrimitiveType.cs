using System.Collections.Generic;
using System.Linq;

namespace Codge.DataModel
{
    //string, int, int64, 
    public class PrimitiveType : TypeBase
    {
        internal PrimitiveType(int id, string name, Namespace nameSpace)
            : base(id, name, nameSpace)
        {
        }

        public override IEnumerable<TypeBase> Dependencies => Enumerable.Empty<TypeBase>();
    }
}
