using System;
using System.Windows.Forms;
using SemeionModulesDesigner.Properties;
using SemeionModulesDesigner.UI;

namespace SemeionModulesDesigner
{
    public partial class MainForm : Form
    {
        private FormManager _formManager;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            _formManager = new FormManager(mainPanel);
        }

        /// <summary>
        /// Strip menu item for loading XSD file.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event data.</param>
        private void LoadXSDToolStripMenuItemClick(object sender, EventArgs e)
        {
            openFileDialog.DefaultExt = "xsd";
            openFileDialog.CheckFileExists = true;
            openFileDialog.Filter = Resources.MainForm_LoadXSDToolStripMenuItemClick_XSD_Files____xsd____xsd_All_Files__________;
            var result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    var file = openFileDialog.FileName;

                    _formManager.GenerateFormFromXsdFile(file);

                    loadXMLToolStripMenuItem.Enabled = true;
                    saveXMLAsToolStripMenuItem.Enabled = true;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, Resources.MainForm_LoadXSDToolStripMenuItemClick_Error_during_processing_XSD_schema_);
                }
            }
        }

        /// <summary>
        /// Stip menu item event click handler to save Xml file with data.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event data.</param>
        private void SaveXmlAsToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (!_formManager.IsFormValid())
            {
                MessageBox.Show(Resources.MainForm_SaveXmlAsToolStripMenuItemClick_Form_contain_error_,
                                Resources.MainForm_SaveXmlAsToolStripMenuItemClick_Error, MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            saveFileDialog.Title = Resources.MainForm_SaveXmlAsToolStripMenuItemClick_Save_file_as___;
            saveFileDialog.Filter = Resources.MainForm_SaveXmlAsToolStripMenuItemClick_XML_files____xml____xml_All_files__________;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var fileStream = saveFileDialog.OpenFile())
                {
                    try
                    {
                        _formManager.SaveFormToXmlFile(fileStream);
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message, Resources.MainForm_SaveXmlAsToolStripMenuItemClick_Error_during_saving_XML_file_with_data_to_the_file_);
                    }
                }
            }
        }

        /// <summary>
        /// Strip menu item event click handler to load Xml file with data.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event data.</param>
        private void LoadXmlToolStripMenuItemClick(object sender, EventArgs e)
        {
            openFileDialog.DefaultExt = "xml";
            openFileDialog.CheckFileExists = true;
            openFileDialog.Filter = Resources.MainForm_LoadXmlToolStripMenuItemClick_XML_Files____xml____xml_All_Files__________;
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                try
                {
                    _formManager.UpdateFormFromXml(openFileDialog.FileName);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, Resources.MainForm_LoadXmlToolStripMenuItemClick_Error_during_loading_XML_file_with_form_data_);
                }
            }
        }

        /// <summary>
        /// Strip menu item click handler for app close.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event data.</param>
        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}
