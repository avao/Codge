using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BasicModel.CS.Serialisation
{
    public static class Utils
    {
        public static void Serialise(XmlWriter writer, object o, SerialisationContext context)
        {
            var serialiser = context.GetSerialiser(o.GetType().FullName);
            serialiser.Serialize(writer, o, context);
        }
    }
}
