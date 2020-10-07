using System.Collections.Generic;
using System.Linq;

namespace Codge.DataModel
{
    public class BuiltInType : TypeBase
    {
        internal BuiltInType(int id, string name, Namespace ns)
            : base(id, name, ns)
        {
        }

        public override IEnumerable<TypeBase> Dependencies => Enumerable.Empty<TypeBase>();
    }
}
