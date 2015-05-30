using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Xml;
using BasicModel.CS.Serialisation;
using Codge.TestSystem;
using Codge.TestSystem.FileBased;
using Types.XsdBasedModel;
using Serialisers.XsdBasedModel;

namespace BasicModel_CS_Test
{
    
    public class XsdBasedSerialisationTest
    {
        public static TestSystem TestSystem = new TestSystem(new DataStorage("../../TestStore/Serialisation"));

        [Test]
        public void SerialiseToXml()
        {
            var obj = new Types.XsdBasedModel.ordertype();
            obj.enumField = enumType.item2;
            var testCase = TestSystem.GetTestCase("XmlXsdBased");

            SerialisationContext context = new SerialisationContext();
            Registrar.RegisterSerialisers(context);

            string actualContent = Utils.Serialise(obj, "order", context);
            testCase.AssertContentXml(actualContent, "serialised.xml", true);
        }
    }
}

