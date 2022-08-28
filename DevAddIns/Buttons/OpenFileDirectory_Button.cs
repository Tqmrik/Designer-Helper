using Inventor;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DevAddIns
{
    internal class OpenFileDirectory_Button : Button_Object
    {
        #region "Constructors"
        //Use constructors of the base class
        public OpenFileDirectory_Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {

        }
        public OpenFileDirectory_Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
        {

        }
        #endregion


        #region "EventHandling"
        override protected void ButtonDefinition_OnExecute(NameValueMap context)
        {
            Document editDocument = InventorApplication.ActiveDocument;
            System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{editDocument.FullFileName}\"");
            //Code for event handling
        }
        #endregion
    }
}

