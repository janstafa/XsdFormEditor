using System;

namespace SemeionModulesDesigner.XmlSchemaParser.XsdModel.Enums
{
    /// <summary>
    /// Information about attribute usage.
    /// </summary>
    [Serializable]
    public enum XAttributeUse
    {
        None = 0,
        Optional = 1,
        Prohibited = 2,
        Required = 3,
    }
}