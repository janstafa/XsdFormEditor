using System;
using System.Xml;
using System.Runtime.InteropServices;
using SemeionModulesDesigner.XmlSchemaParser.Helpers;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel.Interfaces;

namespace SemeionModulesDesigner.XmlSchemaParser
{
    /// <summary>
    /// Xml file parser.
    /// </summary>
    [Guid("AD9502E5-DC76-42E3-AD44-6E245DBB9A88")]
    public class XmlParser : IXmlParser
    {
        private XContainer _newRoot = new XContainer();

        /// <summary>
        /// Fills given XForm structure with data form given Xml file.
        /// </summary>
        /// <param name="xmlFileName">Path to Xml file contains data.</param>
        /// <param name="xsdForm">XForm to be filled with data from Xml file.</param>
        /// <returns>Filled XForm.</returns>
        public XForm GetFilledXForm(string xmlFileName, XForm xsdForm)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFileName);

            var filled = Fill(xsdForm.Root, xmlDoc);

            if (!filled)
            {
                return null;
            }

            return new XForm { Root = _newRoot };
        }

        /// <summary>
        /// Fill given XContainer attributes with data form Xml file.
        /// </summary>
        /// <param name="container">XContainer to be filled up.</param>
        /// <param name="xmlElement">XmlElement with data.</param>
        private void ResolveAttributes(XContainer container, XmlElement xmlElement)
        {
            if (xmlElement != null)
            {
                foreach (IXAttribute attribute in container.Attributes)
                {
                    string value = xmlElement.GetAttribute(attribute.Name);

                    if (!string.IsNullOrEmpty(value))
                    {
                        if (attribute is XAttribute<string>)
                        {
                            ((XAttribute<string>)attribute).Value = value;
                        }
                        else if (attribute is XAttribute<int>)
                        {
                            ((XAttribute<int>)attribute).Value = int.Parse(value);
                        }
                        else if (attribute is XAttribute<bool>)
                        {
                            ((XAttribute<bool>)attribute).Value = bool.Parse(value);
                        }
                        else if (attribute is XAttribute<DateTime>)
                        {
                            ((XAttribute<DateTime>)attribute).Value = DateTime.Parse(value);
                        }
                        else if (attribute is XEnumerationAttribute<string>)
                        {
                            ((XEnumerationAttribute<string>)attribute).Value = value;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Fill given XContainer attributes with data form Xml file.
        /// </summary>
        /// <param name="container">XContainer to be filled up.</param>
        /// <param name="element">XmlElement with data.</param>
        private void ResolveElements(XContainer container, XmlElement element)
        {
            foreach (var containerElement in container.Elements)
            {
                foreach (var childElement in element.ChildNodes)
                {
                    if (!(childElement is XmlComment))
                    {
                        var name = ((XmlElement)childElement).Name;
                        if (containerElement.Name == name)
                        {
                            containerElement.Value = ((XmlElement)childElement).InnerText;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Fill up given XContainer with data from Xml file.
        /// </summary>
        /// <param name="sourceContainer">Given XContainer to be filled up.</param>
        /// <param name="xmlDoc">Xml document contains data.</param>
        /// <returns>True if filling was successfull, false if not.</returns>
        private bool Fill(XContainer sourceContainer, XmlDocument xmlDoc)
        {
            var xmlNodeList = xmlDoc.GetElementsByTagName(sourceContainer.Name);

            if (xmlNodeList.Count > 0)
            {
                _newRoot = sourceContainer.Clone();
                _newRoot.Containers.Clear();
                ResolveAttributes(_newRoot, ((XmlElement)xmlNodeList[0]));
                ResolveElements(_newRoot, ((XmlElement)xmlNodeList[0]));
                _newRoot.Value = xmlNodeList[0].Value;
                _newRoot.Name = xmlNodeList[0].Name;

                foreach (var element in xmlNodeList[0].ChildNodes)
                {
                    if (!(element is XmlComment))
                    {
                        if (element is XmlText)
                        {
                            _newRoot.Value = ((XmlText)element).Value;
                        }

                        foreach (var xContainer in sourceContainer.Containers)
                        {
                            var name = ((XmlElement)element).Name;
                            if (xContainer.Name == name)
                            {
                                FillChildNodes(_newRoot, xContainer, (XmlElement)element);
                            }
                        }
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Fill up child nodes of XContainer.
        /// </summary>
        /// <param name="destinationContainer">XContainer used to copy structure to source container.</param>
        /// <param name="sourceContainer">Source container contained data.</param>
        /// <param name="xmlElement">Xml Element contains data.</param>
        private void FillChildNodes(XContainer destinationContainer, XContainer sourceContainer, XmlElement xmlElement)
        {
            var newContainer = sourceContainer.Clone();
            newContainer.Containers.Clear();
            ResolveAttributes(newContainer, xmlElement);
            ResolveElements(newContainer, xmlElement);

            newContainer.Name = xmlElement.Name;

            if (xmlElement.ChildNodes.Count == 1)
            {
                if (xmlElement.ChildNodes[0] is XmlText)
                {
                    newContainer.Value = xmlElement.InnerText;
                }
            }

            newContainer.ParentContainer = destinationContainer;
            newContainer.Id = newContainer.ParentContainer.Containers.Count + 1;
            newContainer.ParentContainer.Containers.Add(newContainer);

            foreach (var element in xmlElement.ChildNodes)
            {
                if (!(element is XmlComment))
                {
                    foreach (var xContainer in sourceContainer.Containers)
                    {
                        var name = ((XmlElement)element).Name;
                        if (xContainer.Name == name)
                        {
                            FillChildNodes(newContainer, xContainer, (XmlElement)element);
                        }
                    }
                }
            }
        }
    }
}
