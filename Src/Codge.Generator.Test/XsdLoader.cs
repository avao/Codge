using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.DataModel;
using Codge.Generator.Presentations.Xsd;
using NUnit.Framework;
using Codge.DataModel.Descriptors;
using Qart.Testing;
using Qart.Testing.FileBased;
using Codge.DataModel.Framework;
using Qart.Core.Xsd;
using Codge.DataModel.Descriptors.Serialisation;
using System.Xml.Schema;

namespace Codge.Generator.Test
{
    public class XsdLoader
    {
        public static TestSystem TestSystem = new TestSystem(new DataStore("../../TestStore/XsdLoader"));

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
        public void LoadXsd(string testId)
        {
            var testCase = TestSystem.GetTestCase(testId);

            XmlSchema schema = testCase.UsingReadStream("Model.xsd", stream => SchemaLoader.Load(stream));
            ModelDescriptor modelDescriptor = ModelLoader.Load(schema, "AModel");

            testCase.AssertContentXml(modelDescriptor.ToXml(), "ModelDescriptor.xml", true);
        }
    }
}
