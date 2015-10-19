using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Codge.BasicModel.CS.Serialisation
{
    public interface IXmlSerialiser
    {
        void Serialize(XmlWriter writer, object o, SerialisationContext context);
    }
}
