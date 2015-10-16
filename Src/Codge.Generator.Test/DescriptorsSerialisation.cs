using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Xml;
using Codge.DataModel.Descriptors.Serialisation;
using Codge.DataModel.Descriptors;
using Qart.Testing;
using Qart.Testing.FileBased;

namespace Codge.Generator.Test
{
    public class DescriptorsSerialisation
    {
        public static TestSystem TestSystem = new TestSystem(new DataStore("../../TestStore/ModelSerialisation"));

        [Test]
        public void TestReadTypeSystem()
        {
            var testCase = TestSystem.GetTestCase("RoundTrip");

            ModelDescriptor model=null;
            testCase.UsingXmlReader("Model.xml", reader =>
            {
                reader.MoveToContent();
                model = DescriptorXmlReader.Read(reader);
            });

            testCase.AssertContentXml(  model.ToXml(), "Serialised.xml", true);
        }
    }
}
