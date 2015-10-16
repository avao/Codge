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

namespace Codge.Generator.Test
{
    public class XsdLoader
    {
        public static TestSystem TestSystem = new TestSystem(new DataStore("../../TestStore/XsdLoader"));

        [Test]
        public void LoadModelXsd()
        {
            var testCase = TestSystem.GetTestCase("LoadModelXsd");

            ModelDescriptor modelDescriptor;
            var schema = SchemaLoader.Load(@"..\..\..\Codge.Generator\Presentations\Xml\Model.xsd");
            modelDescriptor = ModelLoader.Load(schema, "MyModel");

            var compiler = new ModelCompiler();
            var typeSystem = new TypeSystem();
            var model = compiler.Compile(typeSystem, modelDescriptor);

            testCase.AssertContentXml(TypeSystemXmlSerialiser.ToString(typeSystem), "TypeSystem.xml", true);
        }

        [TestCase("LoadXsd")]
        [TestCase("SequenceInChoice")]
        public void Process(string testId)
        {
            var testCase = TestSystem.GetTestCase(testId);

            ModelDescriptor modelDescriptor;
            using (var stream = testCase.GetReadStream("Model.xsd"))
            {
                var schema = SchemaLoader.Load(stream);
                modelDescriptor = ModelLoader.Load(schema, "AModel");
            }

            testCase.AssertContentXml(modelDescriptor.ToXml(), "ModelDescriptor.xml", true);
        }
    }
}
