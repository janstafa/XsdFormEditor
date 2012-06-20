using System.IO;
using NUnit.Framework;
using SemeionModulesDesigner.XmlSchemaParser;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel;

namespace SemeionModulesDesigner.XsdTransformator.Tests
{
    [TestFixture, Category("UnitTest"), Timeout(5000000)]
    public class XmlWriterTest
    {
        [Test]
        public void CanParseXFormFromXmlFileDocumentumArchivageSettingsXsd()
        {
            //asign
            var xmlWriter = new XmlWriter();
            var xForm = new XForm();

            xForm.Root = new XContainer { Name = "BaseContainer" };

            var xAttribute = new XAttribute<string>(string.Empty) { Name = "StringAttribute", Value = "StringAttribute" };
            var xContainer = new XContainer { Name = "ChildContainer", Value = "ChildContainerValue", ParentContainer = xForm.Root };
            xContainer.Attributes.Add(xAttribute);
            var xElement = new XElement { Name = "Element", Value = "ElementValue" };
            xContainer.Elements.Add(xElement);

            xForm.Root.Attributes.Add(xAttribute);
            xForm.Root.Containers.Add(xContainer);
            xForm.Root.Elements.Add(xElement);

            var fileStream = new FileStream(@"testXml.txt", FileMode.OpenOrCreate);

            //action
            xmlWriter.WriteXFormToXmlFile(fileStream, xForm);

            //assert
            var xml = File.ReadAllText(@"testXml.txt");

            var resultXml = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n" +
"<BaseContainer xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" StringAttribute=\"StringAttribute\">" +
"\r\n  <Element>ElementValue</Element>" +
"\r\n  <ChildContainer StringAttribute=\"StringAttribute\">ChildContainerValue" +
"<Element>ElementValue</Element>" +
"</ChildContainer>" +
"\r\n</BaseContainer>";

            Assert.AreEqual(xml, resultXml);
        }




    }
}