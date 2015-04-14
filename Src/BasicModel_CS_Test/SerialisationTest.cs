using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Types.rootNs;
using System.Xml;
using BasicModel.CS.Serialisation;

namespace BasicModel_CS_Test
{
    
    public class SerialisationTest
    {
        [Test]
        public void SerialiseToXml()
        {
            var obj = new myType2(2, true, new int[]{4});
            var serialiser = new Serialisers.rootNs.myType2();

            SerialisationContext context = new SerialisationContext();
            context.RegisterSerialiser<myType2>(new Serialisers.rootNs.myType2());

            var sb = new StringBuilder();
            using(var writer = XmlWriter.Create(sb))
            {
                serialiser.Serialize(writer, obj, context);
            }
        }
    }
}

