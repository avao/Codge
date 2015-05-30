﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.DataModel;
using Codge.Generator.Presentations.Xsd;
using Codge.TestSystem.FileBased;
using NUnit.Framework;
using Codge.DataModel.Descriptors;

namespace Codge.Generator.Test
{
    public class XsdLoader
    {
        public static TestSystem.TestSystem TestSystem = new TestSystem.TestSystem(new DataStorage("../../TestStore/XsdLoader"));

        [Test]
        public void LoadModelXsd()
        {
            var testCase = TestSystem.GetTestCase("LoadModelXsd");

            ModelDescriptor modelDescriptor;
            var typeSystem = new TypeSystem();
            var schema = SchemaLoader.Load(@"..\..\..\Codge.Generator\Presentations\Xml\Model.xsd");
            modelDescriptor = ModelLoader.Load(typeSystem, schema, "MyModel");
            var compiler = new ModelCompiler();
            var model = compiler.Compile(typeSystem, modelDescriptor);
            testCase.AssertContentXml(TypeSystemXmlSerialiser.ToString(typeSystem), "TypeSystem.xml", true);
        }

        [Test]
        public void LoadXsd()
        {
            var testCase = TestSystem.GetTestCase("LoadXsd");

            var typeSystem = new TypeSystem();
            ModelDescriptor modelDescriptor;
            using (var stream = testCase.GetStream("Test.xsd"))
            {
                var schema = SchemaLoader.Load(stream);
                modelDescriptor = ModelLoader.Load(typeSystem, schema, "AModel");
            }

            var compiler = new ModelCompiler();
            var model = compiler.Compile(typeSystem, modelDescriptor);

            testCase.AssertContentXml(TypeSystemXmlSerialiser.ToString(typeSystem), "XsdTypes.xml", true);
        }
    }
}
