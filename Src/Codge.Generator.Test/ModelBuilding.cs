using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Codge.DataModel;
using Codge.DataModel.Descriptors;
using NUnit.Framework;
using Qart.Testing;
using Qart.Testing.FileBased;
using Codge.DataModel.Framework;

namespace Codge.Generator.Test
{
    class ModelBuilding
    {
        public static TestSystem TestSystem = new TestSystem(new DataStore("../../TestStore/ModelBuilding"));

        private static void CompileAndAssert(ModelDescriptor model, string testCaseName)
        {
            var typeSystem = new TypeSystem();

            var compiler = new ModelCompiler();

            var compiledModel = compiler.Compile(typeSystem, model);

            var testCase = TestSystem.GetTestCase(testCaseName);
            testCase.AssertContentXml(TypeSystemXmlSerialiser.ToString(typeSystem), "TypeSystem.xml", true);
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

