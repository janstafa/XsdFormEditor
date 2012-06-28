using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace SemeionModulesDesigner.XmlSchemaParser.XsdModel.Interfaces
{
    [Guid("6E8C9AD4-B698-4E3F-B42D-6CF10BCFF796")]
    public interface IXContainer : IXmlSerializable
    {
        /// <summary>
        /// Reference to the parent container.
        /// </summary>
        [DispId(1)]
        XContainer ParentContainer { get; set; }

        /// <summary>
        /// Name of the container.
        /// </summary>
        [DispId(2)]
        string Name { get; set; }

        /// <summary>
        /// Container attributes.
        /// </summary>
        [DispId(3)]
        List<IXAttribute> Attributes { get; set; }

        /// <summary>
        /// Child containers.
        /// </summary>
        [DispId(4)]
        List<XContainer> Containers { get; set; }

        /// <summary>
        /// Container elements.
        /// </summary>
        [DispId(5)]
        List<XElement> Elements { get; set; }

        /// <summary>
        /// MinOccurs
        /// </summary>
        [DispId(6)]
        decimal MinOccurs { get; set; }

        /// <summary>
        /// MaxOccurs
        /// </summary>
        [DispId(7)]
        decimal MaxOccurs { get; set; }

        /// <summary>
        /// Container value.
        /// </summary>
        [DispId(8)]
        string Value { get; set; }

        /// <summary>
        /// Container id.
        /// </summary>
        [DispId(9)]
        int Id { get; set; }
    }
}