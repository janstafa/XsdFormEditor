using System;
using System.Drawing;
using System.Windows.Forms;
using SemeionModulesDesigner.Helpers;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel.Interfaces;

namespace SemeionModulesDesigner.UI
{
    /// <summary>
    /// Contains methods to create WinForms Controls.
    /// </summary>
    internal class ControlFactory
    {
        /// <summary>
        /// Creates Label control from given XContainer.
        /// </summary>
        /// <param name="container">Given XContainer.</param>
        /// <returns>Label control according to given XContainer.</returns>
        internal Label GetLabel(XContainer container)
        {
            var label = new Label();
            label.AutoSize = true;
            label.Name = container.Name + "Label";
            label.Size = new Size(35, 13);
            label.TabIndex = 0;
            label.Text = container.Name;
            return label;
        }

        /// <summary>
        /// Creates Label control from given XElement.
        /// </summary>
        /// <param name="xElement">Given XElement.</param>
        /// <returns>Label control according to given XElement.</returns>
        internal Label GetLabel(XElement xElement)
        {
            var label = new Label();
            label.AutoSize = true;
            label.Name = xElement.Name + "Label";
            label.Size = new Size(35, 13);
            label.TabIndex = 0;
            label.Text = xElement.Name;
            return label;
        }

        /// <summary>
        /// Creates Label control from given IXAttribute.
        /// </summary>
        /// <param name="attribute">Given IXAttribute.</param>
        /// <returns>Label control according to given IXAttribute.</returns>
        internal Label GetLabel(IXAttribute attribute)
        {
            var label = new Label();
            label.AutoSize = true;
            label.Name = attribute.Name + "Label";
            label.Size = new Size(35, 13);
            label.TabIndex = 0;
            label.Text = attribute.Name;
            return label;
        }

        /// <summary>
        /// Creates Control control from given XElement.
        /// </summary>
        /// <param name="element">Given XElement.</param>
        /// <returns>Label control according to given XElement.</returns>
        internal Control GetControl(XElement element)
        {
            var textBox = new TextBox();
            textBox.DataBindings.Add("Text", element, "Value");

            textBox.Name = element.Name;
            textBox.Tag = element;

            return textBox;
        }

        /// <summary>
        /// Creates Control control from given IXAttribute.
        /// </summary>
        /// <param name="attribute">Given IXAttribute.</param>
        /// <returns>Label control according to given IXAttribute.</returns>
        internal Control GetControl(IXAttribute attribute)
        {
            var control = new Control();

            if (attribute is XAttribute<string>)
            {
                var stringAttribute = (XAttribute<string>)attribute;
                var textBox = new TextBox();
                control = textBox;
                control.DataBindings.Add("Text", stringAttribute, "Value");
            }
            else if (attribute is XAttribute<int>)
            {
                var intAttribute = (XAttribute<int>)attribute;
                var numericUpDown = new NumericUpDown();
                numericUpDown.DataBindings.Add("Value", intAttribute, "Value");
                control = numericUpDown;
            }
            else if (attribute is XAttribute<bool>)
            {
                var boolAttribute = (XAttribute<bool>)attribute;
                var checkBox = new CheckBox();
                control = checkBox;
                checkBox.DataBindings.Add("Checked", boolAttribute, "Value");
            }
            else if (attribute is XAttribute<DateTime>)
            {
                var dateTimeAttribute = (XAttribute<DateTime>)attribute;
                var dateTimePicker = new DateTimePicker();

                if (!dateTimeAttribute.Value.HasMeaning())
                {
                    dateTimeAttribute.Value = DateTime.Now;
                }

                dateTimePicker.DataBindings.Add("Value", dateTimeAttribute, "Value");
                control = dateTimePicker;
            }
            else if (attribute is XEnumerationAttribute<string>)
            {
                var enumerationAttribute = (XEnumerationAttribute<string>)attribute;
                var enumeration = enumerationAttribute.Enumeration;

                var comboBox = new ComboBox();

                comboBox.BeginUpdate();
                comboBox.Items.Clear();
                foreach (var item in enumeration)
                {
                    comboBox.Items.Add(item);
                }
                comboBox.EndUpdate();

                comboBox.SelectedItem = enumerationAttribute.Value;
                comboBox.DataBindings.Add("Text", enumerationAttribute, "Value");
                control = comboBox;
            }

            control.Name = attribute.Name;
            control.Tag = attribute;


            return control;
        }
    }
}
