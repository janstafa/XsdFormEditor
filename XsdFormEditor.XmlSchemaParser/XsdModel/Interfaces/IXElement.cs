using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace SemeionModulesDesigner.XmlSchemaParser.XsdModel.Interfaces
{
    [Guid("30DF0297-9EB6-4254-A4E2-34A1A5231016")]
    public interface IXElement : IXmlSerializable
    {
        /// <summary>
        /// Element name.
        /// </summary>
        [DispId(1)]
        string Name { get; set; }

        /// <summary>
        /// Element value.
        /// </summary>
        [DispId(2)]
        string Value { get; set; }
    }
}