using Codge.DataModel.Descriptors;
using Codge.DataModel.Descriptors.Serialisation;
using NUnit.Framework;
using Qart.Core.DataStore;
using Qart.Core.Xml;
using Qart.Testing;
using Qart.Testing.Framework;

namespace Codge.Generator.Test
{
    public class DescriptorsSerialisation
    {
        public static ITestStorage TestSystem = new TestStorage(new FileBasedDataStore("../../../TestStore/ModelSerialisation"), dataStore => true, null, null);

        [Test]
        public void TestReadTypeSystem()
        {
            var testCase = TestSystem.GetTestCase("RoundTrip");

            ModelDescriptor model = null;
            testCase.UsingXmlReader("Model.xml", reader =>
            {
                reader.MoveToContent();
                model = DescriptorXmlReader.Read(reader);
            });

            testCase.AssertContent(XmlWriterUtils.ToXmlString(model.ToXml, true), "Serialised.xml", true);
        }
    }
}
