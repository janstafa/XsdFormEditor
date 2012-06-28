using System.IO;
using System.Windows.Forms;
using SemeionModulesDesigner.XmlSchemaParser;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel;

namespace SemeionModulesDesigner.UI
{
    /// <summary>
    /// Managing form UI
    /// </summary>
    internal class FormManager
    {
        /// <summary>
        /// UI Panel to render controls.
        /// </summary>
        private readonly Panel _mainPanel;

        private readonly ControlManager _controlManager;

        /// <summary>
        /// XForm used for form description.
        /// </summary>
        private XForm _xForm;

        /// <summary>
        /// XForm used to store form data.
        /// </summary>
        private XForm _xFormData;

        internal FormManager(Panel mainPanel)
        {
            _mainPanel = mainPanel;
            _controlManager = new ControlManager();

        }


        /// <summary>
        /// Creates XForm from given Xsd file
        /// </summary>
        /// <param name="file">Path to Xsd file.</param>
        internal void GenerateFormFromXsdFile(string file)
        {
            _mainPanel.Controls.Clear();
            _xForm = null;

            _controlManager.Clear();//prepare for new rendering

            var xsdParser = new XsdParser();

            _xForm = xsdParser.ParseXsdFile(file);
            _xFormData = xsdParser.ParseXsdFile(file);
            var generateGuiGetGroupBox = _controlManager.GetGroupBoxGui(_xForm.Root, _xFormData.Root);

            _mainPanel.Controls.Add(generateGuiGetGroupBox);
        }

        /// <summary>
        /// Updates values in XForm from given Xml file.
        /// </summary>
        /// <param name="fileName">Path to Xml file contains data.</param>
        internal void UpdateFormFromXml(string fileName)
        {
            var xmlParser = new XmlParser();
            var filledXForm = xmlParser.GetFilledXForm(fileName, _xForm);

            if (filledXForm != null)
            {
                _xFormData = filledXForm;
            }

            _controlManager.UpdateVisibleContainers(_xFormData.Root);
            _controlManager.UpdateBindingForVisibleContainer(_xFormData.Root);
        }

        /// <summary>
        /// Save XForm to Xml file.
        /// </summary>
        /// <param name="stream">Output stream.</param>
        internal void SaveFormToXmlFile(Stream stream)
        {
            _controlManager.Save();

            var xmlWriter = new XmlWriter();
            xmlWriter.WriteXFormToXmlFile(stream, _xFormData);
        }

        /// <summary>
        /// Check if form is valid usually before saving Xml data.
        /// </summary>
        /// <returns>True if form is valid, false if not.</returns>
        internal bool IsFormValid()
        {
            return _controlManager.AreControlsValid();
        }
    }
}
