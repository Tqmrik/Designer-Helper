using Inventor;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DevAddIns
{
    internal class TestFunctionButton : Button
    {
        #region "Constructors"
        //Use constructors of the base class
        public TestFunctionButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {

        }
        public TestFunctionButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
        {

        }
        #endregion


        #region "EventHandling"
        override protected void ButtonDefinition_OnExecute(NameValueMap context)
        {
            Document activeDocument = InventorApplication.ActiveDocument;

            if(activeDocument.isAssemblyDocument())
            {
                DocumentsEnumerator asb = (DocumentsEnumerator)activeDocument.File.AvailableDocuments;
                
                for(int i = 0; i< asb.Count; i++)
                {
                    var fgf = asb[i];
                }

            }


        }
        #endregion
    }
}

