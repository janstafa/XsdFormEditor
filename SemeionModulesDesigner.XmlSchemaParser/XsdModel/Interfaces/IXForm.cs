using System.Runtime.InteropServices;

namespace SemeionModulesDesigner.XmlSchemaParser.XsdModel.Interfaces
{
    [Guid("40CB15EE-0273-473D-8408-513D3BBFB622")]
    public interface IXForm
    {
        /// <summary>
        /// Root container.
        /// </summary>
        [DispId(1)]
        XContainer Root { get; set; }
    }
}