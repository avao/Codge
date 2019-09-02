using System.Xml;

namespace Codge.BasicModel.CS.Serialisation
{
    public interface IXmlSerialiser
    {
        void Serialize(XmlWriter writer, object o, SerialisationContext context);
    }
}
