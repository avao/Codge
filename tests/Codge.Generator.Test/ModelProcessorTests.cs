using Codge.DataModel.Descriptors;
using Codge.DataModel.Descriptors.Serialisation;
using Codge.DataModel.Framework;
using Codge.Generator.Presentations.Xml;
using NUnit.Framework;
using Qart.Core.DataStore;
using Qart.Core.Xml;
using Qart.Testing;
using Qart.Testing.Framework;
using System.Linq;

namespace Codge.Generator.Test
{
    class ModelProcessorTests
    {
        public static ITestStorage TestSystem = new TestStorage(new FileBasedDataStore("../../../TestStore/ModelProcessor"), dataStore => true, null, null);

        [TestCase("Optional")]
        public void ProcessModels(string testId)
        {
            var testCase = TestSystem.GetTestCase(testId);
            var modelDescriptors = testCase.GetItemIds("Models").Select(_ => (ModelDescriptor)testCase.UsingXmlReader(_, reader => ModelLoader.Load(reader)));
            ModelDescriptor modelDescriptor = ModelProcessor.MergeToLhs(modelDescriptors);

            testCase.AssertContent(XmlWriterUtils.ToXmlString(modelDescriptor.ToXml, true), "ModelDescriptor.xml", true);
        }
    }
}
