using System.Collections.Generic;
using System.Linq;

namespace Codge.DataModel
{
    //string, int, int64, 
    public class PrimitiveType : TypeBase
    {
        internal PrimitiveType(string name, Namespace nameSpace)
            : base(name, nameSpace)
        {
        }

        public override IEnumerable<TypeBase> Dependencies => Enumerable.Empty<TypeBase>();
    }
}
