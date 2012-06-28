using System.IO;
using System.Windows.Forms;
using NUnit.Framework;
using SemeionModulesDesigner.UI;

namespace SemeionModulesDesigner.Tests.UI
{
    [TestFixture, Category("SystemTest")]
    public class FormManagerTests
    {
        [Test]
        public void CanGenerateFormFromXsdFile()
        {
            //asign
            var formManager = new FormManager(new Panel());

            //action
            formManager.GenerateFormFromXsdFile("DocumentumArchivageSettings.xsd");

            //assert
        }


        [Test]
        public void CanUpdateFormFromXml()
        {
            //asign
            var formManager = new FormManager(new Panel());

            ////action
            formManager.GenerateFormFromXsdFile("DocumentumArchivageSettings.xsd");
            formManager.UpdateFormFromXml("DocumentumArchivageSettings.xml");

            //assert
        }

        [Test]
        public void CanUseSaveFormToXmlFile()
        {
            //asign
            var formManager = new FormManager(new Panel());
            var stream = new MemoryStream();

            //action
            formManager.GenerateFormFromXsdFile("DocumentumArchivageSettings.xsd");
            formManager.UpdateFormFromXml("DocumentumArchivageSettings.xml");
            formManager.SaveFormToXmlFile(stream);

            //assert
        }


        [Test]
        public void CanUseIsFormValid()
        {
            //asign
            var formManager = new FormManager(new Panel());

            //action
            formManager.IsFormValid();

            //assert
        }
    }
}