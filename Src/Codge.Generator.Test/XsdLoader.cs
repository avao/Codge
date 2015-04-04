using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codge.DataModel;
using Codge.Generator.Presentations.Xsd;
using Codge.TestSystem.FileBased;
using NUnit.Framework;

namespace Codge.Generator.Test
{
    public class XsdLoader
    {
        public static TestSystem.TestSystem TestSystem = new TestSystem.TestSystem(new DataStorage("../../TestStore/XsdLoader"));

        [Test]
        public void LoadModelXsd()
        {
            var testCase = TestSystem.GetTestCase("LoadModelXsd");

            var typeSystem = new TypeSystem();
            ModelLoader.Load(typeSystem, @"..\..\..\Codge.Generator\Presentations\Xml\Model.xsd", "MyModel");//TODO proper path
                        
            testCase.AssertContentXml(TypeSystemXmlSerialiser.ToString(typeSystem), "TypeSystem.xml", true);
        }

        [Test]
        public void LoadXsd()
        {
            var testCase = TestSystem.GetTestCase("LoadXsd");
            
            var typeSystem = new TypeSystem();
            using (var stream = testCase.GetStream("Test.xsd"))
            {
                ModelLoader.Load(typeSystem, stream, "AModel");
            }
            
            testCase.AssertContentXml(TypeSystemXmlSerialiser.ToString(typeSystem), "XsdTypes.xml", true);
        }
    }
}
