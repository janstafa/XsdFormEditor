using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using SemeionModulesDesigner.Helpers;
using SemeionModulesDesigner.XmlSchemaParser.Helpers;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel.Enums;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel.Interfaces;
using XsdFormEditor.WinForms.Properties;

namespace SemeionModulesDesigner.UI
{
    /// <summary>
    /// Managing UI rendering.
    /// </summary>
    internal class ControlManager
    {
        /// <summary>
        /// Collection of visible XContainers currently used in GUI.
        /// </summary>
        readonly IDictionary<string, XContainer> _visibleContainers = new Dictionary<string, XContainer>();

        /// <summary>
        /// Collection of all rendered GroupBoxes, used for managing navigation.
        /// </summary>
        readonly IDictionary<string, GroupBox> _allGroupBoxes = new Dictionary<string, GroupBox>();

        /// <summary>
        /// Collection of all rendered Controls, used for validation.
        /// </summary>
        private readonly IList<Control> _controls = new List<Control>();

        /// <summary>
        /// Main rendered GroupBox, used during rendering the form.
        /// </summary>
        private GroupBox _rootGroupBox;

        /// <summary>
        /// Last rendered GroupBox, used during rendering the form.
        /// </summary>
        private GroupBox _lastGroupBox;

        private readonly FormValidation _formValidation;
        private readonly ControlFactory _controlFactory;

        /// <summary>
        /// Sub class to keep track on navigation names.
        /// </summary>
        private class NavigationName
        {
            public const string IdTextBox = "idTextBox";
            public const string ParentIdTextBox = "parentIdTextBox";
            public const string CreateButton = "createButton";
            public const string DeleteButton = "deleteButton";
            public const string PreviousButton = "previousButton";
            public const string NextButton = "nextButton";
        }

        internal ControlManager()
        {
            _formValidation = new FormValidation(new ErrorProvider());
            _controlFactory = new ControlFactory();
        }

        /// <summary>
        /// Save current state of form to memory before save to a file.
        /// </summary>
        internal void Save()
        {
            _controls[0].Focus(); //controls are binded, we just need to change focus for the case of last edited control value to be binded properly.
        }

        /// <summary>
        /// Check if all controls in form are valid.
        /// </summary>
        /// <returns></returns>
        internal bool AreControlsValid()
        {
            return _formValidation.AreControlsValid(_controls);
        }

        /// <summary>
        /// Updates all state of all visible containers after change.
        /// </summary>
        /// <param name="container">Updated container.</param>
        internal void UpdateVisibleContainers(XContainer container)
        {
            if (container.Name == null) return;

            if (_visibleContainers.ContainsKey(container.Name))
            {
                _visibleContainers[container.Name] = container;
                UpdatePrevNextButton(container);//updates navigation
            }

            foreach (var containerChild in container.Containers)
            {
                UpdateVisibleContainers(containerChild);//recursive
            }
        }

        /// <summary>
        /// Updates binding for current visible container.
        /// </summary>
        /// <param name="container">Current container.</param>
        internal void UpdateBindingForVisibleContainer(XContainer container)
        {
            if (container.Name == null) return;

            if (_allGroupBoxes.ContainsKey(container.Name))
            {
                UpdateBinding(container, _allGroupBoxes[container.Name]);

                if (container.Containers.Count > 0)
                {
                    foreach (var containerChild in container.Containers)
                    {
                        UpdateBindingForVisibleContainer(containerChild);//recursive
                    }
                }
            }
        }


        /// <summary>
        /// Render Gui according to given XContainers tree.
        /// </summary>
        /// <param name="rootContainer">XContainers tree used for form rendering.</param>
        /// <param name="rootContainerData">XContainers tree used for data from form.</param>
        /// <returns>Rendered GroupBox structure.</returns>
        internal GroupBox GetGroupBoxGui(XContainer rootContainer, XContainer rootContainerData)
        {
            GenerateGui(rootContainer, rootContainerData);
            return _rootGroupBox;
        }

        /// <summary>
        /// Prepare to render new form.
        /// </summary>
        public void Clear()
        {
            _rootGroupBox = null;
            _lastGroupBox = null;
            _visibleContainers.Clear();
            _allGroupBoxes.Clear();
        }

        /// <summary>
        /// Render Gui according to given XContainers tree.
        /// </summary>
        /// <param name="rootContainer">XContainers tree used for form rendering.</param>
        /// <param name="rootContainerData">XContainers tree used for data from form.</param>
        private void GenerateGui(XContainer rootContainer, XContainer rootContainerData)
        {
            int x = 20;
            int y = 10;

            if (_lastGroupBox != null)
            {
                y = _lastGroupBox.Size.Height + 20;
            }

            var groupBoxFromContainer = GetGroupBoxFromContainer(rootContainer, rootContainerData);
            groupBoxFromContainer.Location = new Point(x, y);

            if (_rootGroupBox == null)
            {
                _rootGroupBox = groupBoxFromContainer;
            }
            else
            {
                _lastGroupBox.Controls.Add(groupBoxFromContainer);
            }

            _allGroupBoxes.Add(groupBoxFromContainer.Name, groupBoxFromContainer);

            foreach (var container in rootContainer.Containers)
            {
                _lastGroupBox = groupBoxFromContainer;

                var containerData = rootContainerData.Containers.FindAll(o => o.Name == container.Name)[0];

                GetGroupBoxGui(container, containerData);
            }
        }

        /// <summary>
        /// Create a GroupBox form given XContainers.
        /// </summary>
        /// <param name="rootContainer">XContainers tree used for form rendering.</param>
        /// <param name="rootContainerData">XContainers tree used for data from form.</param>
        /// <returns>New GroupBox.</returns>
        private GroupBox GetGroupBoxFromContainer(XContainer rootContainer, XContainer rootContainerData)
        {
            var groupBox = new GroupBox();
            groupBox.Name = rootContainerData.Name;
            groupBox.Text = rootContainerData.Name;

            int tabIndex = 0;
            int x = 10;
            int y = 20;

            if (rootContainer.Value != null)
            {
                var label = _controlFactory.GetLabel(rootContainerData);
                label.Location = new Point(x, y + 4);
                groupBox.Controls.Add(label);

                var control = new TextBox();
                control.Location = new Point(x + 220, y);
                control.Name = rootContainerData.Name;
                control.TabIndex = tabIndex;
                control.DataBindings.Add("Text", rootContainerData, "Value");
                _controls.Add(control);
                groupBox.Controls.Add(control);
                y += 25;
            }

            foreach (var xAttribute in rootContainerData.Attributes)
            {
                var label = _controlFactory.GetLabel(xAttribute);
                label.Location = new Point(x, y + 4);
                groupBox.Controls.Add(label);

                var control = _controlFactory.GetControl(xAttribute);
                control.Location = new Point(x + 220, y);
                control.TabIndex = tabIndex;
                _controls.Add(control);
                groupBox.Controls.Add(control);
                y += 25;

                if (((IXAttribute)control.Tag).Use == XAttributeUse.Required)
                {
                    label.Text += Resources.ControlManager_GetGroupBoxFromContainer___;
                    control.Validating += _formValidation.ControlValidating;
                }

                tabIndex++;
            }

            foreach (var xElement in rootContainerData.Elements)
            {
                var label = _controlFactory.GetLabel(xElement);
                label.Location = new Point(x, y + 4);
                groupBox.Controls.Add(label);

                var control = _controlFactory.GetControl(xElement);
                control.Location = new Point(x + 220, y);
                control.TabIndex = tabIndex;
                _controls.Add(control);
                groupBox.Controls.Add(control);
                y += 25;

                tabIndex++;
            }

            var labelId = new Label();
            labelId.AutoSize = true;
            labelId.Location = new Point(x, y + 4);
            labelId.Name = "idLabel";
            labelId.Size = new Size(35, 13);
            labelId.TabIndex = 0;
            labelId.Text = rootContainerData.Name + " Id";
            groupBox.Controls.Add(labelId);

            var textBoxId = new TextBox();
            textBoxId.Location = new Point(x + 220, y);
            textBoxId.TabIndex = tabIndex;
            textBoxId.Name = NavigationName.IdTextBox;
            textBoxId.Enabled = false;
            textBoxId.Text = rootContainerData.Id.ToString(CultureInfo.InvariantCulture);
            textBoxId.DataBindings.Add("Text", rootContainerData, "Id");
            groupBox.Controls.Add(textBoxId);
            y += 25;

            groupBox.AutoSize = true;
            groupBox.Size = new Size(x, y);

            if (rootContainer.MaxOccurs > 1)
            {
                _visibleContainers.Add(rootContainerData.Name, rootContainerData);

                Button button = new Button();
                button.Text = "*";
                button.Size = new Size(30, 20);
                button.Location = new Point(110, 0);
                button.Name = NavigationName.CreateButton;
                button.Tag = rootContainer;
                button.Click += CreateButtonClick;
                groupBox.Controls.Add(button);

                Button button2 = new Button();
                button2.Text = "x";
                button2.Size = new Size(30, 20);
                button2.Location = new Point(140, 0);
                button2.Name = rootContainer.Name + "Delete";
                button2.Tag = rootContainer;
                button2.Name = NavigationName.DeleteButton;
                button2.Click += DeleteButtonClick;
                button2.Enabled = false;
                groupBox.Controls.Add(button2);

                Button button3 = new Button();
                button3.Text = "<";
                button3.Size = new Size(30, 20);
                button3.Location = new Point(170, 0);
                button3.Name = rootContainer.Name + "Prev";
                button3.Tag = rootContainer;
                button3.Name = NavigationName.PreviousButton;
                button3.Click += PreviousButtonClick;
                groupBox.Controls.Add(button3);


                Button button4 = new Button();
                button4.Text = ">";
                button4.Size = new Size(30, 20);
                button4.Location = new Point(200, 0);
                button4.Name = rootContainer.Name + "Next";
                button4.Tag = rootContainer;
                button4.Name = NavigationName.NextButton;
                button4.Click += NextButtonClick;
                groupBox.Controls.Add(button4);


                if (rootContainerData.ParentContainer != null)
                {
                    var labelParentId = new Label();

                    labelParentId.AutoSize = true;
                    labelParentId.Location = new Point(x, y + 4);
                    labelParentId.Name = "parentIdLabel";
                    labelParentId.Size = new Size(35, 13);
                    labelParentId.TabIndex = 0;
                    labelParentId.Text = rootContainerData.ParentContainer.Name + " Id";
                    groupBox.Controls.Add(labelParentId);


                    var textBoxParentId = new TextBox();
                    textBoxParentId.Location = new Point(x + 220, y);
                    textBoxParentId.TabIndex = tabIndex;
                    textBoxParentId.Name = NavigationName.ParentIdTextBox;
                    textBoxParentId.Enabled = false;
                    textBoxParentId.Text = rootContainerData.ParentContainer.Id.ToString(CultureInfo.InvariantCulture);
                    textBoxParentId.DataBindings.Add("Text", rootContainerData.ParentContainer, "Id");
                    groupBox.Controls.Add(textBoxParentId);
                    y += 25;
                }


            }

            groupBox.AutoSize = true;
            groupBox.Size = new Size(x, y);

            return groupBox;
        }

        /// <summary>
        /// Event click handler for creating new XContainer.
        /// </summary>
        /// <param name="sender">Button.</param>
        /// <param name="e">Event data.</param>
        private void CreateButtonClick(object sender, EventArgs e)
        {
            var xContainer = (XContainer)((Button)sender).Tag;
            var container = xContainer.Clone();
            var visibleContainer = _visibleContainers[xContainer.Name];

            var withMaxId = visibleContainer.ParentContainer.Containers.OrderByDescending(item => item.Id).First();
            container.Id = withMaxId.Id + 1;
            container.ParentContainer = visibleContainer.ParentContainer;


            var controls = (((Button)sender).Parent).Controls.Find(NavigationName.IdTextBox, false);
            controls[0].DataBindings.Clear();
            controls[0].DataBindings.Add("Text", container, "Id");


            var controlsParent = (((Button)sender).Parent).Controls.Find(NavigationName.ParentIdTextBox, false);
            controlsParent[0].DataBindings.Clear();
            controlsParent[0].DataBindings.Add("Text", container.ParentContainer, "Id");

            container.ParentContainer.Containers.Add(container);
            _visibleContainers[xContainer.Name] = container;

            UpdateVisibleContainers(container);

            UpdateBindingForVisibleContainer(container);
        }


        /// <summary>
        /// Updates visibility of previous or next button.
        /// </summary>
        /// <param name="container">Current visible container.</param>
        private void UpdatePrevNextButton(XContainer container)
        {
            if (_visibleContainers.ContainsKey(container.Name) && _allGroupBoxes.ContainsKey(container.Name))
            {
                var visibleContainer = _visibleContainers[container.Name];
                var visibleGroupBox = _allGroupBoxes[container.Name];

                var deleteButton = visibleGroupBox.Controls.Find(NavigationName.DeleteButton, false)[0];
                deleteButton.Enabled = visibleContainer.ParentContainer.Containers.Count != 1;
            }
        }


        /// <summary>
        /// Event click handler for deleting new XContainer.
        /// </summary>
        /// <param name="sender">Button.</param>
        /// <param name="e">Event data.</param>
        private void DeleteButtonClick(object sender, EventArgs e)
        {
            var xContainer = (XContainer)((Button)sender).Tag;
            var visibleContainer = _visibleContainers[xContainer.Name];

            visibleContainer.ParentContainer.Containers.RemoveAll(x => x.Id == visibleContainer.Id & x.Name == visibleContainer.Name);

            var container = visibleContainer.ParentContainer.Containers.FindAll(x => x.Name == visibleContainer.Name).First();

            var controls = ((Button)sender).Parent.Controls.Find(NavigationName.IdTextBox, false);
            controls[0].DataBindings.Clear();
            controls[0].DataBindings.Add("Text", container, "Id");


            var controlsParent = ((Button)sender).Parent.Controls.Find(NavigationName.ParentIdTextBox, false);
            controlsParent[0].DataBindings.Clear();
            controlsParent[0].DataBindings.Add("Text", container.ParentContainer, "Id");

            _visibleContainers[xContainer.Name] = container;

            UpdateVisibleContainers(container);

            UpdateBindingForVisibleContainer(container);
        }


        /// <summary>
        /// Event click handler for showing previous visible XContainer.
        /// </summary>
        /// <param name="sender">Button.</param>
        /// <param name="e">Event data.</param>
        private void PreviousButtonClick(object sender, EventArgs e)
        {
            var xContainer = (XContainer)((Button)sender).Tag;
            var visibleContainer = _visibleContainers[xContainer.Name];

            var index = visibleContainer.ParentContainer.Containers.FindIndex(x => x.Id == visibleContainer.Id & x.Name == visibleContainer.Name);

            if (index == 0)
            {
                index = visibleContainer.ParentContainer.Containers.Count;
            }

            var container = visibleContainer.ParentContainer.Containers[index - 1];

            var controls = (((Button)sender).Parent).Controls.Find(NavigationName.IdTextBox, false);
            controls[0].DataBindings.Clear();
            controls[0].DataBindings.Add("Text", container, "Id");


            var controlsParent = (((Button)sender).Parent).Controls.Find(NavigationName.ParentIdTextBox, false);
            controlsParent[0].DataBindings.Clear();
            controlsParent[0].DataBindings.Add("Text", container.ParentContainer, "Id");

            UpdateVisibleContainers(container);

            UpdateBindingForVisibleContainer(container);

            _visibleContainers[xContainer.Name] = container;
        }



        /// <summary>
        /// Event click handler for showing next XContainer.
        /// </summary>
        /// <param name="sender">Button.</param>
        /// <param name="e">Event data.</param>
        private void NextButtonClick(object sender, EventArgs e)
        {
            var xContainer = (XContainer)((Button)sender).Tag;
            var visibleContainer = _visibleContainers[xContainer.Name];

            var index = visibleContainer.ParentContainer.Containers.FindIndex(x => x.Id == visibleContainer.Id & x.Name == visibleContainer.Name);

            if (index == visibleContainer.ParentContainer.Containers.Count - 1)
            {
                index = -1;
            }

            var container = visibleContainer.ParentContainer.Containers[index + 1];

            var controls = (((Button)sender).Parent).Controls.Find(NavigationName.IdTextBox, false);
            controls[0].DataBindings.Clear();
            controls[0].DataBindings.Add("Text", container, "Id");

            var controlsParent = (((Button)sender).Parent).Controls.Find(NavigationName.ParentIdTextBox, false);
            controlsParent[0].DataBindings.Clear();
            controlsParent[0].DataBindings.Add("Text", container.ParentContainer, "Id");

            UpdateVisibleContainers(container);

            UpdateBindingForVisibleContainer(container);

            _visibleContainers[xContainer.Name] = container;
        }

        /// <summary>
        /// Update binding between given XContainer and Control.
        /// </summary>
        /// <param name="container">XContainer to be binded with Control.</param>
        /// <param name="parentControl">Controlo to be binded with XContainer.</param>
        private void UpdateBinding(XContainer container, Control parentControl)
        {
            var controlsForContainer = parentControl.Controls.Find(container.Name, false);
            if (controlsForContainer.Any())
            {
                var controlForContainer = controlsForContainer[0];
                controlForContainer.DataBindings.Clear();
                controlForContainer.DataBindings.Add("Text", container, "Value");
            }

            foreach (XElement element in container.Elements)
            {
                var controlsForElement = parentControl.Controls.Find(element.Name, false);
                if (controlsForElement.Any())
                {
                    var controlForElement = controlsForElement[0];
                    controlForElement.DataBindings.Clear();
                    controlForElement.DataBindings.Add("Text", container, "Value");
                }
            }

            foreach (IXAttribute attribute in container.Attributes)
            {
                var controlForAttribute = parentControl.Controls.Find(attribute.Name, false)[0];
                controlForAttribute.DataBindings.Clear();

                if (attribute is XAttribute<string>)
                {
                    var stringAttribute =  (XAttribute<string>)attribute;
                    controlForAttribute.DataBindings.Add("Text", stringAttribute, "Value");
                }
                else if (attribute is XAttribute<int>)
                {
                    var intAttribute =  (XAttribute<int>)attribute;
                    controlForAttribute.DataBindings.Add("Value", intAttribute, "Value");
                }
                else if (attribute is XAttribute<bool>)
                {
                    var boolAttribute =  (XAttribute<bool>)attribute;
                    controlForAttribute.DataBindings.Add("Checked", boolAttribute, "Value");
                }
                else if (attribute is XAttribute<DateTime>)
                {
                    var dateTimeAttribute =  (XAttribute<DateTime>)attribute;

                    if (!dateTimeAttribute.Value.HasMeaning())
                    {
                        dateTimeAttribute.Value = DateTime.Now;
                    }

                    controlForAttribute.DataBindings.Add("Value", dateTimeAttribute, "Value");
                }
                else if (attribute is XEnumerationAttribute<string>)
                {
                    var enumerationAttribute =  (XEnumerationAttribute<string>)attribute;
                    var enumeration = enumerationAttribute.Enumeration;

                    var comboBox = (ComboBox)controlForAttribute;

                    comboBox.BeginUpdate();
                    comboBox.Items.Clear();
                    foreach (var item in enumeration)
                    {
                        comboBox.Items.Add(item);
                    }
                    comboBox.EndUpdate();

                    comboBox.SelectedItem = enumerationAttribute.Value;
                    controlForAttribute.DataBindings.Add("Text", enumerationAttribute, "Value");
                }
            }

            var visibleGroupBox = _allGroupBoxes[container.Name];
            var controlsParent = visibleGroupBox.Controls.Find("parIdTextBoxent", false);

            if (controlsParent.Count() != 0)
            {
                controlsParent[0].DataBindings.Clear();
                controlsParent[0].DataBindings.Add("Text", container.ParentContainer, "Id");

            }

            var controls = visibleGroupBox.Controls.Find("idTextbox", false);
            controls[0].DataBindings.Clear();
            controls[0].DataBindings.Add("Text", container, "Id");
        }

    }
}