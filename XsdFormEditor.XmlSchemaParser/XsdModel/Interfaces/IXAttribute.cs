using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel.Enums;

namespace SemeionModulesDesigner.XmlSchemaParser.XsdModel.Interfaces
{
    [Guid("A2436EFB-BE7A-4ECA-B32E-568058C87E3B")]
    public interface IXAttribute : INotifyPropertyChanged, IXmlSerializable
    {
        [DispId(1)]
        string Name { get; set; }

        [DispId(2)]
        XAttributeUse Use { get; set; }

        [DispId(3)]
        string GetStringXmlValue();
    }

    [Guid("F25519D1-7C9E-4F8D-9BA4-EE8E90B45E61")]
    public interface IXAttribute<T> : IXAttribute
    {
        [DispId(1)]
        T Value { get; set; }
    }
}