using System.IO;
using System.Xml;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel;

namespace SemeionModulesDesigner.XmlSchemaParser
{
    /// <summary>
    /// Xml writer used to write data to xml file.
    /// </summary>
    public class XmlWriter : IXmlWriter
    {
        /// <summary>
        /// Write data from form to output stream.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="xForm">XForm with data.</param>
        public void WriteXFormToXmlFile(Stream stream, XForm xForm)
        {
            //var settings = new XmlWriterSettings();
            //settings.Indent = true;
            //settings.NamespaceHandling = NamespaceHandling.OmitDuplicates;

            using (var writer = System.Xml.XmlWriter.Create(stream, settings))
            {
                writer.WriteStartElement(xForm.Root.Name);
                writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");

                foreach (var xAttribute in xForm.Root.Attributes)
                {
                    writer.WriteAttributeString(xAttribute.Name, xAttribute.GetStringXmlValue());
                }

                foreach (var xElement in xForm.Root.Elements)
                {
                    xElement.WriteXml(writer);
                }

                foreach (var xContainer in xForm.Root.Containers)
                {
                    xContainer.WriteXml(writer);
                }

                writer.WriteEndElement();
            }

            stream.Close();
        }
    }
}