using System.IO;
using System.Runtime.InteropServices;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel;

namespace SemeionModulesDesigner.XmlSchemaParser
{
    /// <summary>
    /// Xml writer interface used to write data to xml file.
    /// </summary>
    [Guid("3EEBEED6-F0E6-4562-9A16-D3B97E587944")]
    public interface IXmlWriter
    {
        /// <summary>
        /// Write data from form to output stream.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        /// <param name="xForm">XForm with data.</param>
        [DispId(1)]
        void WriteXFormToXmlFile(Stream stream, XForm xForm);
    }
}