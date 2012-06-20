using System;
using System.Windows.Forms;
using NUnit.Framework;
using SemeionModulesDesigner.UI;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel;

namespace SemeionModulesDesigner.Tests.UI
{
    [TestFixture, Category("UnitTest"), Timeout(500)]
    public class ControlFactoryTest
    {
        [Test]
        public void GetLabelForXElement()
        {
            //asign
            var xElement = new XElement();
            xElement.Name = "xElement";
            var controlFactory = new ControlFactory();

            //action
            var control = controlFactory.GetLabel(xElement);

            //assert
            Assert.NotNull(control);
            Assert.AreEqual(control.Name, xElement.Name + "Label");
        }

        [Test]
        public void GetLabelForXAttribute()
        {
            //asign
            var xElement = new XAttribute<string>("test");
            xElement.Name = "XAttribute";
            var controlFactory = new ControlFactory();

            //action
            var control = controlFactory.GetLabel(xElement);

            //assert
            Assert.NotNull(control);
            Assert.AreEqual(control.Name, xElement.Name + "Label");
        }

        [Test]
        public void GetLabelForXContainer()
        {
            //asign
            var xElement = new XContainer();
            xElement.Name = "XContainer";
            var controlFactory = new ControlFactory();

            //action
            var control = controlFactory.GetLabel(xElement);

            //assert
            Assert.NotNull(control);
            Assert.AreEqual(control.Name, xElement.Name + "Label");
        }

        [Test]
        public void GetControlForXElement()
        {
            //asign
            var xElement = new XElement();
            xElement.Name = "xElement";
            var controlFactory = new ControlFactory();

            //action
            var control = controlFactory.GetControl(xElement);

            //assert
            Assert.NotNull(control);
            Assert.AreEqual(control.Name, xElement.Name);
            Assert.True(control.Tag is XElement);
            Assert.True(control is TextBox);
        }

        [Test]
        public void GetControlForXAttributeTypeString()
        {
            //asign
            var xAttribute = new XAttribute<string>("default");
            xAttribute.Name = "xAttribute";
            var controlFactory = new ControlFactory();

            //action
            var control = controlFactory.GetControl(xAttribute);

            //assert
            Assert.NotNull(control);
            Assert.AreEqual(control.Name, xAttribute.Name);
            Assert.True(control.Tag is XAttribute<string>);
            Assert.True(control is TextBox);
        }


        [Test]
        public void GetControlForXAttributeTypeInteger()
        {
            //asign
            var xAttribute = new XAttribute<int>(1);
            xAttribute.Name = "xAttribute";
            var controlFactory = new ControlFactory();

            //action
            var control = controlFactory.GetControl(xAttribute);

            //assert
            Assert.NotNull(control);
            Assert.AreEqual(control.Name, xAttribute.Name);
            Assert.True(control.Tag is XAttribute<int>);
            Assert.True(control is NumericUpDown);
        }

        [Test]
        public void GetControlForXAttributeTypeDateTime()
        {
            //asign
            var xAttribute = new XAttribute<DateTime>(DateTime.Now);
            xAttribute.Name = "xAttribute";
            var controlFactory = new ControlFactory();

            //action
            var control = controlFactory.GetControl(xAttribute);

            //assert
            Assert.NotNull(control);
            Assert.AreEqual(control.Name, xAttribute.Name);
            Assert.True(control.Tag is XAttribute<DateTime>);
            Assert.True(control is DateTimePicker);
        }

        [Test]
        public void GetControlForXAttributeTypeBoolean()
        {
            //asign
            var xAttribute = new XAttribute<bool>(false);
            xAttribute.Name = "xAttribute";
            var controlFactory = new ControlFactory();

            //action
            var control = controlFactory.GetControl(xAttribute);

            //assert
            Assert.NotNull(control);
            Assert.AreEqual(control.Name, xAttribute.Name);
            Assert.True(control.Tag is XAttribute<bool>);
            Assert.True(control is CheckBox);
        }


        [Test]
        public void GetControlForXEnumerationAttributeTypeString()
        {
            //asign
            var xAttribute = new XEnumerationAttribute<string>("test");
            xAttribute.Enumeration.Add("test1");
            xAttribute.Enumeration.Add("test");
            xAttribute.Name = "xAttribute";
            xAttribute.Value = "test";
            var controlFactory = new ControlFactory();

            //action
            var control = controlFactory.GetControl(xAttribute);

            //assert
            Assert.NotNull(control);
            Assert.AreEqual(control.Name, xAttribute.Name);
            Assert.True(control.Tag is XEnumerationAttribute<string>);
            Assert.True(control is ComboBox);
            Assert.AreEqual(((ComboBox)control).SelectedItem, "test");
        }
    }
}