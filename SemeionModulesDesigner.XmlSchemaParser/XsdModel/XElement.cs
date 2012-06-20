using System;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Schema;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel.Interfaces;

namespace SemeionModulesDesigner.XmlSchemaParser.XsdModel
{
    /// <summary>
    /// Encapsulate data for an element.
    /// </summary>
    [Serializable]
    [Guid("F0F8ED22-D59D-4C2E-AB4A-85C77BA7250F")]
    public class XElement : IXElement
    {
        /// <summary>
        /// Element name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Element value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema"/> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> method.
        /// </returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> stream from which the object is deserialized. </param>
        public void ReadXml(XmlReader reader)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized. </param>
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            writer.WriteStartElement(Name);
            writer.WriteString(Value);
            writer.WriteEndElement();
        }
    }
}