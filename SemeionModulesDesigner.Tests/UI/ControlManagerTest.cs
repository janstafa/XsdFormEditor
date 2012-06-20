using System;
using System.ComponentModel;
using System.Windows.Forms;
using NUnit.Framework;
using SemeionModulesDesigner.UI;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel;
using SemeionModulesDesigner.XmlSchemaParser.XsdModel.Enums;

namespace SemeionModulesDesigner.Tests.UI
{
    [TestFixture, NUnit.Framework.Category("UnitTest"), Timeout(500)]
    public class ControlManagerTest
    {
        [Test]
        public void CanUseSave()
        {
            //asign
            var controlManager = new ControlManager();
            var xContainer = new XContainer();
            xContainer.Value = "test";

            //action
            controlManager.GetGroupBoxGui(xContainer, xContainer);
            controlManager.Save();

            //assert
        }


        [Test]
        public void CanUseAreControlsValid()
        {
            //asign
            var controlManager = new ControlManager();
            var xForm = new XForm();

            xForm.Root = new XContainer { Name = "BaseContainer" };

            var xAttribute = new XAttribute<string>("StringAttribute") { Name = "StringAttribute", Value = "StringAttribute", Use = XAttributeUse.Required };
            var xContainer = new XContainer { Name = "ChildContainer", Value = "ChildContainerValue", ParentContainer = xForm.Root, MaxOccurs = 1234, MinOccurs = 0 };
            xContainer.Attributes.Add(xAttribute);
            var xElement = new XElement { Name = "Element", Value = "ElementValue" };
            xContainer.Elements.Add(xElement);

            xForm.Root.Attributes.Add(xAttribute);
            xForm.Root.Containers.Add(xContainer);
            xForm.Root.Elements.Add(xElement);
            xContainer.Value = "test";

            //action
            controlManager.GetGroupBoxGui(xForm.Root, xForm.Root);
            var areControlsValid = controlManager.AreControlsValid();

            //assert
            Assert.False(areControlsValid);
        }

        [Test]
        public void CanUseGenerateGui()
        {
            //asign
            var controlManager = new ControlManager();
            var xForm = new XForm();

            xForm.Root = new XContainer { Name = "BaseContainer" };

            var xAttribute = new XAttribute<string>(string.Empty) { Name = "StringAttribute", Value = "StringAttribute", Use = XAttributeUse.Required };
            var xContainer = new XContainer { Name = "ChildContainer", Value = "ChildContainerValue", ParentContainer = xForm.Root, MaxOccurs = 1234, MinOccurs = 0 };
            xContainer.Attributes.Add(xAttribute);
            var xElement = new XElement { Name = "Element", Value = "ElementValue" };
            xContainer.Elements.Add(xElement);

            xForm.Root.Attributes.Add(xAttribute);
            xForm.Root.Containers.Add(xContainer);
            xForm.Root.Elements.Add(xElement);

            //action
            var groupBoxGui = controlManager.GetGroupBoxGui(xForm.Root, xForm.Root);

            //assert
            Assert.NotNull(groupBoxGui);
        }


        [Test]
        public void CanUseUpdateVisibleContainers()
        {
            //asign
            var controlManager = new ControlManager();
            var xForm = new XForm();

            xForm.Root = new XContainer { Name = "BaseContainer" };

            var xAttribute = new XAttribute<string>(string.Empty) { Name = "StringAttribute", Value = "StringAttribute", Use = XAttributeUse.Required };
            var xContainer = new XContainer { Name = "ChildContainer", Value = "ChildContainerValue", ParentContainer = xForm.Root, MaxOccurs = 1234, MinOccurs = 0 };
            xContainer.Attributes.Add(xAttribute);
            var xElement = new XElement { Name = "Element", Value = "ElementValue" };
            xContainer.Elements.Add(xElement);

            xForm.Root.Attributes.Add(xAttribute);
            xForm.Root.Containers.Add(xContainer);
            xForm.Root.Elements.Add(xElement);

            //action
            controlManager.GetGroupBoxGui(xForm.Root, xForm.Root);
            controlManager.UpdateVisibleContainers(xForm.Root);

            //assert
        }


        [Test]
        public void CanUseUpdateBindingForChildren()
        {
            //asign
            var controlManager = new ControlManager();
            var xForm = new XForm();

            xForm.Root = new XContainer { Name = "BaseContainer" };

            var xAttribute1 = new XAttribute<string>(string.Empty) { Name = "StringAttribute1", Value = "StringAttribute", Use = XAttributeUse.Required };
            var xAttribute2 = new XAttribute<int>(1) { Name = "StringAttribute2", Use = XAttributeUse.Required };
            var xAttribute3 = new XAttribute<bool>(true) { Name = "StringAttribute3", Use = XAttributeUse.Required };
            var xAttribute4 = new XAttribute<DateTime>(DateTime.Now) { Name = "StringAttribute4", Use = XAttributeUse.Required };
            var xAttribute5 = new XEnumerationAttribute<string>(string.Empty) { Name = "StringAttribute5", Use = XAttributeUse.Required };
            var xContainer = new XContainer { Name = "ChildContainer", Value = "ChildContainerValue", ParentContainer = xForm.Root, MaxOccurs = 1234, MinOccurs = 0 };
            xContainer.Attributes.Add(xAttribute1);
            var xElement = new XElement { Name = "Element", Value = "ElementValue" };
            xContainer.Elements.Add(xElement);

            xForm.Root.Attributes.Add(xAttribute1);
            xForm.Root.Attributes.Add(xAttribute2);
            xForm.Root.Attributes.Add(xAttribute3);
            xForm.Root.Attributes.Add(xAttribute4);
            xForm.Root.Attributes.Add(xAttribute5);
            xForm.Root.Containers.Add(xContainer);
            xForm.Root.Elements.Add(xElement);

            //action
            controlManager.GetGroupBoxGui(xForm.Root, xForm.Root);
            controlManager.UpdateBindingForVisibleContainer(xForm.Root);

            //assert
        }
    }
}