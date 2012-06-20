using NUnit.Framework;
using SemeionModulesDesigner.XmlSchemaParser;

namespace SemeionModulesDesigner.XsdTransformator.Tests
{
    [TestFixture, Category("UnitTest"), Timeout(500)]
    public class XsdParserTest
    {
        [Test]
        public void CanParseXFormFromXmlFileDocumentumArchivageSettingsXsd()
        {
            //asign
            var xsdParser = new XsdParser();

            //action
            var xForm = xsdParser.ParseXsdFile("DocumentumArchivageSettings.xsd");

            //assert
            Assert.NotNull(xForm);
        }

        [Test]
        public void CanParseXFormFromXmlFileMoveCopySettingsXsd()
        {
            //asign
            var xsdParser = new XsdParser();

            //action
            var xForm = xsdParser.ParseXsdFile("MoveCopySettings.xsd");

            //assert
            Assert.NotNull(xForm);
        }
    }
}