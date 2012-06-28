using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.Runtime.InteropServices;
using System.Xml.Schema;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel.Enums;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel.Interfaces;

namespace SemeionModulesDesigner.XmlSchemaParser.XsdModel
{
    [Serializable]
    [Guid("2DDA17F1-E58D-475E-8140-E20F7F5D163C")]
    public class XEnumerationAttribute<T> : IXAttribute<T>
    {
        /// <summary>
        /// Enumerations.
        /// </summary>
        public IList<T> Enumeration { get; set; }

        public XEnumerationAttribute(T defaultValue)
        {
            DefaultValue = defaultValue;
            Enumeration = new List<T>();
        }

        /// <summary>
        /// Property changed event.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        public void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Attribute value.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Attribute usage.
        /// </summary>
        public XAttributeUse Use { get; set; }

        public string GetStringXmlValue()
        {
            if (Value == null)
            {
                return string.Empty;
            }

            return Value.ToString();
        }

        /// <summary>
        /// Default value of an attribute.
        /// </summary>
        public T DefaultValue { get; private set; }

        private T _value;

        /// <summary>
        /// Attribute value.
        /// </summary>
        public T Value
        {
            get { return _value; }
            set
            {
                _value = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("Value"));
            }
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
            writer.WriteAttributeString(Name, GetStringXmlValue());
        }
    }
}