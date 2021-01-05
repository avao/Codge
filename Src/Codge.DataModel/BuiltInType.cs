using System.Collections.Generic;
using System.Linq;

namespace Codge.DataModel
{
    public class BuiltInType : TypeBase
    {
        internal BuiltInType(string name, Namespace ns)
            : base(name, ns)
        {
        }

        public override IEnumerable<TypeBase> Dependencies => Enumerable.Empty<TypeBase>();
    }
}
