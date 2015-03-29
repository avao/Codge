using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codge.DataModel.Descriptors
{
    public class PrimitiveTypeDescriptor : TypeDescriptor
    {
        internal PrimitiveTypeDescriptor(string name, NamespaceDescriptor nameSpace)
            : base(name, nameSpace)
        {
        }
    }
}
