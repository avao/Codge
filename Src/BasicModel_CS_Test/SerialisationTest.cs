using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Xml;
using BasicModel.CS.Serialisation;
using Serialisers;
using Types.rootNs;
using Types.rootNs.nestedNs;
using Serialisers.rootNs;
using Qart.Testing;
using Qart.Testing.FileBased;

namespace BasicModel_CS_Test
{

    public class SerialisationTest
    {
        public static TestSystem TestSystem = new TestSystem(new DataStorage("../../TestStore/Serialisation"));

        [Test]
        public void SerialiseToXml()
        {
            var obj = new Types.rootNs.myType2(2, true, new[] { 4, 5 }, new[] { new typeInNestedNs("avalue1"), new typeInNestedNs("avalue2") });

            var testCase = TestSystem.GetTestCase("Xml");


            SerialisationContext context = new SerialisationContext();
            Registrar.RegisterSerialisers(context);

            string actualContent = Utils.Serialise(obj, "root", context);
            testCase.AssertContentXml(actualContent, "serialised.xml", true);
        }
    }
}

