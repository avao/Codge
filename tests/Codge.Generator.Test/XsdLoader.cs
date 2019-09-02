using Codge.DataModel.Descriptors;
using Codge.DataModel.Descriptors.Serialisation;
using Codge.Generator.Presentations.Xsd;
using NUnit.Framework;
using Qart.Core.DataStore;
using Qart.Core.Xml;
using Qart.Core.Xsd;
using Qart.Testing;
using Qart.Testing.Framework;
using System.Xml.Schema;

namespace Codge.Generator.Test
{
    public class XsdLoader
    {
        public static ITestStorage TestSystem = new TestStorage(new FileBasedDataStore("../../../TestStore/XsdLoader"), dataStore => true, null, null);

        [TestCase("LoadXsd")]
        [TestCase("SequenceInChoice")]
        [TestCase("BuiltInTypes")]
        [TestCase("ComplexTypeWithContent")]
        [TestCase("RecursiveType")]
        [TestCase("Enumeration")]
        [TestCase("ElementWithEmbededType")]
        [TestCase("Group")]
        [TestCase("ModelXsd")]
        [TestCase("SimpleType")]
        [TestCase("UnboundChoice")]
        [TestCase("AttributeRef")]
        [TestCase("XsdAny")]
        [TestCase("OptionalSequenceInSequence")]
        public void LoadXsd(string testId)
        {
            var testCase = TestSystem.GetTestCase(testId);

            XmlSchema schema = testCase.UsingReadStream("Model.xsd", stream => SchemaLoader.Load(stream));
            ModelDescriptor modelDescriptor = ModelLoader.Load(schema, "AModel");

            testCase.AssertContent(XmlWriterUtils.ToXmlString(modelDescriptor.ToXml, true), "ModelDescriptor.xml", true);
        }
    }
}
