using Codge.BasicModel.CS.Serialisation;
using NUnit.Framework;
using Qart.Core.DataStore;
using Qart.Testing;
using Qart.Testing.Framework;
using Serialisers.XsdBasedModel;
using System;
using Types.XsdBasedModel;

namespace BasicModel_CS_Test
{

    public class XsdBasedSerialisationTest
    {
        public static ITestStorage TestSystem = new TestStorage(new FileBasedDataStore("../../../TestStore/Serialisation"), (dataStore=> true), null, null);

        [Test]
        public void SerialiseToXml()
        {
            var obj = new Types.XsdBasedModel.ordertype();
            obj.enumField = enumType.item_With_Spaces_835378278;
            obj.enumFieldCollection = new[] { enumType.item_With_Spaces_835378278 };
            var testCase = TestSystem.GetTestCase("XmlXsdBased");

            SerialisationContext context = new SerialisationContext();
            Registrar.RegisterSerialisers(context);

            string actualContent = Utils.Serialise(obj, "order", context, true);
            testCase.AssertContent(actualContent, "serialised.xml", true);
        }


        [Test]
        public void SerialiseToXml_AllXsdTypes()
        {
            var obj = new Types.XsdBasedModel.allXsdTypes();
            obj.anyUri = "Uri";
            obj.base64Binary = "base64Bin";
            obj.boolean = true;
            obj.byte_ = (byte)1;
            obj.date = "aDate";
            obj.dateTime = "dateTime";
            obj.decimal_ = new Decimal(1.7);
            obj.double_ = (double)1.9;
            obj.duration = "duration";
            obj.Entities = "entities";
            obj.Entity = "entity";
            obj.float_ = (double)1.2;
            obj.gDay = "gDay";
            obj.gMonth = "gMonth";
            obj.gMonthDay = "gMonthDay";
            obj.gYear = "gYear";
            obj.gYearMonth = "gYearMonth";
            obj.hexBinary = "hexBin";
            obj.id = "id";
            obj.idRef = "idRef";
            obj.idRefs = "idRefs";
            obj.int_ = (int)3;
            obj.integer = (int)4;
            obj.language = "language";
            //obj.long_ = 567L; //TODO introduce long?
            obj.Name = "name";
            obj.ncName = "nName";
            obj.negativeInteger = -1;
            obj.nmToken = "nmToken";
            obj.nmTokens = "nmtokens";
            obj.nonNegativeInteger = 0;
            obj.nonPositiveInteger = 0;
            obj.normalizedString = "normalisedString";
            obj.positiveInteger = 7;
            obj.QName = "qName";
            obj.short_ = (short)3;
            obj.string_ = "string";
            obj.time = "time";
            obj.token = "token";
            obj.unsignedByte = (byte)9;
            obj.unsignedIint = (int)10;
            //obj.unsignedLong = 878L;
            obj.unsignedShort = (short)9;


            var testCase = TestSystem.GetTestCase("XmlXsdBased");

            SerialisationContext context = new SerialisationContext();
            Registrar.RegisterSerialisers(context);

            string actualContent = Utils.Serialise(obj, "allXsdTypes", context, true);
            testCase.AssertContent(actualContent, "allXsdSerialised.xml", true);
        }
    }
}

