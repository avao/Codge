using Codge.DataModel.Descriptors;
using Codge.DataModel.Descriptors.Serialisation;
using Codge.Generator.Presentations.Xsd;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Qart.Core.DataStore;
using Qart.Core.Xml;
using Qart.Core.Xsd;
using Qart.Testing;
using Qart.Testing.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace Codge.Generator.Test
{
    public class XsdLoader
    {
        private static ITestStorage TestSystem = new TestStorage(new FileBasedDataStore("../../../TestStore/XsdLoader"), dataStore => true, null, null, new LoggerFactory());

        private static IEnumerable<string> GetTests()
        {
            return TestSystem.GetTestCaseIds().Where(id => id != ".");
        }


        [TestCaseSource(nameof(GetTests))]
        public void LoadXsd(string testId)
        {
            var testCase = TestSystem.GetTestCase(testId);

            XmlSchema schema = testCase.UsingReadStream("Model.xsd", stream => SchemaLoader.Load(stream));
            ModelDescriptor modelDescriptor = ModelLoader.Load(new[] { schema }, "AModel");

            testCase.AssertContent(XmlWriterUtils.ToXmlString(modelDescriptor.ToXml, true), "ModelDescriptor.xml", true);
        }
    }
}
