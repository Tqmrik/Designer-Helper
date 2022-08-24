using Inventor;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DevAddIns
{
    internal class TestFunction_Button : Button_Object
    {
        #region "Constructors"
        //Use constructors of the base class
        public TestFunction_Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {

        }
        public TestFunction_Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
        {

        }
        #endregion


        #region "EventHandling"
        override protected void ButtonDefinition_OnExecute(NameValueMap context)
        {
            //Document activeDocument = InventorApplication.ActiveDocument;

            //if(activeDocument.isAssemblyDocument())
            //{
            //    DocumentsEnumerator asb = (DocumentsEnumerator)activeDocument.File.AvailableDocuments;

            //    for(int i = 0; i< asb.Count; i++)
            //    {
            //        var fgf = asb[i];
            //    }

            //}


            //TranslatorOptions translator = new TranslatorOptions();
            //translator.DisplayTranslatorOptions();
            Document editDocument = InventorApplication.ActiveDocument;
            string fileName = System.IO.Path.GetDirectoryName(editDocument.FullFileName) + "\\" + System.IO.Path.GetFileNameWithoutExtension(editDocument.FullDocumentName);
            string extension = System.IO.Path.GetExtension(editDocument.FullFileName);
            for (int i = 1; ; i++)
            {
                string copyName = $"{fileName}_{i}{extension}";
                if (!System.IO.File.Exists(copyName))
                {
                    editDocument.SaveAs(copyName, true);
                    return;
                }
            }
        }
        #endregion
    }
}

