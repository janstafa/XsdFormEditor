using System.Runtime.InteropServices;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel.Interfaces;

namespace SemeionModulesDesigner.XmlSchemaParser.XsdModel
{
    [Guid("AE5B0D3D-19D3-4BA3-889B-B34F8342CB04")]
    public class XForm : IXForm
    {
        /// <summary>
        /// Root container.
        /// </summary>
        public XContainer Root { get; set; }
    }
}