using NUnit.Framework;
using SemeionModulesDesigner.XmlSchemaParser;

namespace SemeionModulesDesigner.XsdTransformator.Tests
{
    [TestFixture, Category("SystemTest")]
    public class XmlParserTest
    {
        [Test]
        public void CanParseXFormFromXmlFileDocumentumArchivageSettingsXml() 
        {
            //asign
            var xmlParser = new XmlParser();
            var xsdParser = new XsdParser();
            var xForm = xsdParser.ParseXsdFile("DocumentumArchivageSettings.xsd");

            //action
            var xFormFromXml = xmlParser.GetFilledXForm("DocumentumArchivageSettings.xml", xForm);

            //assert
            Assert.NotNull(xFormFromXml);
        }
    }
}