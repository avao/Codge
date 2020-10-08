using Codge.BasicModel.CS.Serialisation;
using NUnit.Framework;
using Qart.Testing;
using Qart.Testing.Framework;
using Serialisers.rootNs;
using Types.rootNs.nestedNs;

namespace BasicModel_CS_Test
{

    public class SerialisationTest
    {
        public static ITestStorage TestSystem = TestStorageFactory.CreateTestStorage("../../../TestStore/Serialisation");

        [Test]
        public void SerialiseToXml()
        {
            var obj = new Types.rootNs.myType2(2, true, new[] { 4, 5 }, new[] { new typeInNestedNs("avalue1"), new typeInNestedNs("avalue2") }, "<blah/>");

            var testCase = TestSystem.GetTestCase("Xml");


            SerialisationContext context = new SerialisationContext();
            Registrar.RegisterSerialisers(context);

            string actualContent = Utils.Serialise(obj, "root", context, true);
            testCase.AssertContent(actualContent, "serialised.xml", true);
        }
    }
}

