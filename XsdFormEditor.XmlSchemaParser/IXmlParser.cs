using System.Runtime.InteropServices;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel;

namespace SemeionModulesDesigner.XmlSchemaParser
{
    /// <summary>
    /// Xml file parser interface.
    /// </summary>
    [Guid("6460FA95-6BBA-4C2F-B146-F79199C866FB")]
    public interface IXmlParser
    {
        /// <summary>
        /// Fills given XForm structure with data form given Xml file.
        /// </summary>
        /// <param name="xmlFileName">Path to Xml file contains data.</param>
        /// <param name="xsdForm">XForm to be filled with data from Xml file.</param>
        /// <returns>Filled XForm.</returns>
        [DispId(1)]
        XForm GetFilledXForm(string xmlFileName, XForm xsdForm);
    }
}