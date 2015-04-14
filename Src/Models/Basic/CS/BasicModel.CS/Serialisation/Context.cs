using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicModel.CS.Serialisation
{
    public class SerialisationContext
    {
        private IDictionary<string, IXmlSerialiser> _serialisers;

        public SerialisationContext()
        {
            _serialisers = new Dictionary<string, IXmlSerialiser>();
        }

        public void RegisterSerialiser<T>(IXmlSerialiser serialiser)
        {
            RegisterSerialiser(typeof(T).FullName, serialiser);
        }

        public void RegisterSerialiser(string typeName, IXmlSerialiser serialiser)
        {
            _serialisers.Add(typeName, serialiser);
        }

        public IXmlSerialiser GetSerialiser(string typeName)
        {
            return _serialisers[typeName];
        }
    }
}
