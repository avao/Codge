using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Xml;
using Codge.DataModel.Descriptors.Serialisation;

namespace Codge.Generator.Test
{
    public class DescriptorsSerialisation
    {
        [Test]
        public void TestReadTypeSystem()
        {
            using (var reader = XmlReader.Create(@"../../../Generated/TestModel.xml"))
            {
                reader.MoveToContent();
                var model = DescriptorXmlSerialiser.Read(reader, "Name", "Namespace");
            }
        }
    }
}
