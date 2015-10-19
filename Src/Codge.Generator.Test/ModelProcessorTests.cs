using Codge.DataModel.Descriptors;
using Codge.DataModel.Framework;
using Codge.Generator.Presentations.Xml;
using Qart.Testing;
using Qart.Testing.FileBased;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Codge.DataModel.Descriptors.Serialisation;


namespace Codge.Generator.Test
{
    class ModelProcessorTests
    {
        public static TestSystem TestSystem = new TestSystem(new DataStore("../../TestStore/ModelProcessor"));

        [TestCase("Optional")]
        public void Process(string testId)
        {
            var testCase = TestSystem.GetTestCase(testId);
            var modelDescriptors = testCase.GetItemIds("Models").Select(_ => (ModelDescriptor)testCase.UsingXmlReader(_, reader => ModelLoader.Load(reader)));
            ModelDescriptor modelDescriptor = ModelProcessor.MergeToLhs(modelDescriptors);
            
            testCase.AssertContentXml(modelDescriptor.ToXml(), "ModelDescriptor.xml", true);
        }
    }
}
