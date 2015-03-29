using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.DataModel
{
    public class BuiltInType : TypeBase
    {
        internal BuiltInType(int id, string name, Namespace ns)
            : base(id, name, ns)
        {
        }

        public override IEnumerable<TypeBase> Dependencies { get { return Enumerable.Empty<TypeBase>(); } }
    }

    
}
