using Codge.DataModel;
using Codge.DataModel.Descriptors;
using Codge.DataModel.Framework;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Qart.Core.DataStore;
using Qart.Core.Xml;
using Qart.Testing;
using Qart.Testing.Framework;

namespace Codge.Generator.Test
{
    class ModelBuilding
    {
        public static ITestStorage TestSystem = new TestStorage(new FileBasedDataStore("../../../TestStore/ModelBuilding"), dataStore => true, null, null, new LoggerFactory());

        private static void CompileAndAssert(ModelDescriptor model, string testCaseName)
        {
            var typeSystem = new TypeSystem();

            var compiler = new ModelProcessor(new LoggerFactory());

            var compiledModel = compiler.Compile(typeSystem, model);

            var testCase = TestSystem.GetTestCase(testCaseName);
            testCase.AssertContent(XmlWriterUtils.ToXmlString(writer => TypeSystemXmlSerialiser.WriteTo(typeSystem, writer)), "TypeSystem.xml", true);
        }

        [Test]
        public void Enumeration()
        {
            var model = new ModelDescriptor("TestModel", "TestModel");

            var type = model.RootNamespace.CreateEnumerationType("TestEnumeration");
            type.AddItem("item0");
            type.AddItem("item1", 3);
            type.AddItem("item2");

            CompileAndAssert(model, "Enumeration");
        }

        [Test]
        public void DependentTypes()
        {
            var model = new ModelDescriptor("MyModel", "MyModel");

            var composite = model.RootNamespace.CreateCompositeType("MyCompositeType");
            composite.AddField("Field1", BuiltInTypes.Int);
            composite.AddField("Field2", "MyModel.NestedNamespace.AnotherComposite");
            composite.AddField("Field3", BuiltInTypes.Bool);

            var nestedNamespace = model.RootNamespace.GetOrCreateNamespace("NestedNamespace");
            var anotherComposite = nestedNamespace.CreateCompositeType("AnotherComposite");
            anotherComposite.AddField("AField", BuiltInTypes.Bool);

            CompileAndAssert(model, "DependentTypes");
        }

        [Test]
        public void CollectionField()
        {
            var model = new ModelDescriptor("MyModel", "MyModel");

            var enumeration = model.RootNamespace.CreateEnumerationType("TestEnumeration");

            var composite = model.RootNamespace.CreateCompositeType("MyCompositeType");
            composite.AddField("Field1", BuiltInTypes.Int);
            composite.AddField("CollectionOfBuiltInTypes", BuiltInTypes.Bool, true);
            composite.AddField("CollectionOfEnums", "TestEnumeration", true);

            CompileAndAssert(model, "CollectionField");
        }

    }
}

