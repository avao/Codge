using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Types.rootNs;
using System.Xml;
using BasicModel.CS.Serialisation;
using Serialisers;
using Codge.TestSystem;
using Codge.TestSystem.FileBased;

namespace BasicModel_CS_Test
{
    
    public class SerialisationTest
    {
        public static TestSystem TestSystem = new TestSystem(new DataStorage("../../TestStore/Serialisation"));

        [Test]
        public void SerialiseToXml()
        {
            var obj = new myType2(2, true, new int[]{4,5});
            var serialiser = new Serialisers.rootNs.myType2();

            var testCase = TestSystem.GetTestCase("Xml");


            SerialisationContext context = new SerialisationContext();
            Registrar.RegisterSerialisers(context);

            string actualContent = Utils.Serialise(obj, "root", context);
            testCase.AssertContentXml(actualContent, "serialised.xml", true);
        }
    }
}

