using System.Runtime.InteropServices;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel;

namespace SemeionModulesDesigner.XmlSchemaParser
{
    /// <summary>
    /// Used for Xsd file parsing.
    /// </summary>
    [Guid("03BE1C71-DA40-448C-AD0B-6FAF2BCB6708")]
    public interface IXsdParser
    {
        /// <summary>
        /// Get XForm from given Xsd file.
        /// </summary>
        /// <param name="fileName">Path to Xsd file.</param>
        /// <returns></returns>
        [DispId(1)]
        XForm ParseXsdFile(string fileName);
    }
}