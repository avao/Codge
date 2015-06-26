using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Xml;
using Codge.DataModel.Descriptors.Serialisation;
using Codge.DataModel.Descriptors;
using Codge.TestSystem.FileBased;

namespace Codge.Generator.Test
{
    public class DescriptorsSerialisation
    {
        public static TestSystem.TestSystem TestSystem = new TestSystem.TestSystem(new DataStorage("../../TestStore/ModelSerialisation"));

        [Test]
        public void TestReadTypeSystem()
        {
            var testCase = TestSystem.GetTestCase("RoundTrip");

            ModelDescriptor model;
            using (var stream = testCase.GetStream("Model.xml"))
            using (var reader = XmlReader.Create(stream))
            {
                reader.MoveToContent();
                model = DescriptorXmlReader.Read(reader);
            }

            var output = new StringBuilder();
            using (var writer = XmlWriter.Create(output))
            {
                DescriptorXmlWriter.Write(writer, model);
            }

            testCase.AssertContentXml(output.ToString(), "Serialised.xml", true);

        }
    }
}
