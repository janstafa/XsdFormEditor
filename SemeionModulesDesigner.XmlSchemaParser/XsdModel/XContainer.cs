using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Runtime.InteropServices;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel.Interfaces;

namespace SemeionModulesDesigner.XmlSchemaParser.XsdModel
{
    [Serializable]
    [Guid("AA0EA4EF-A399-4DBF-8FF9-06C00E4630BD")]
    public class XContainer : IXContainer
    {
        /// <summary>
        /// Reference to the parent container.
        /// </summary>
        public XContainer ParentContainer { get; set; }

        /// <summary>
        /// Name of the container.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Container attributes.
        /// </summary>
        public List<IXAttribute> Attributes { get; set; }

        /// <summary>
        /// Child containers.
        /// </summary>
        public List<XContainer> Containers { get; set; }

        /// <summary>
        /// Container elements.
        /// </summary>
        public List<XElement> Elements { get; set; }

        /// <summary>
        /// MinOccurs
        /// </summary>
        public decimal MinOccurs { get; set; }

        /// <summary>
        /// MaxOccurs
        /// </summary>
        public decimal MaxOccurs { get; set; }

        /// <summary>
        /// Container value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Container id.
        /// </summary>
        public int Id { get; set; }

        public XContainer()
        {
            Containers = new List<XContainer>();
            Attributes = new List<IXAttribute>();
            Elements = new List<XElement>();
        }

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

            foreach (var xAttribute in Attributes)
            {
                xAttribute.WriteXml(writer);
            }

            if (Value != null)
            {
                writer.WriteValue(Value);
            }

            foreach (var xElement in Elements)
            {
                xElement.WriteXml(writer);
            }

            foreach (var xContainer in Containers)
            {
                xContainer.WriteXml(writer);
            }

            writer.WriteEndElement();

        }
    }
}