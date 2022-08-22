using Inventor;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DevAddIns
{
    internal class NewExportAs_Button : Button_Object
    {
        #region "Constructors"
        //Use constructors of the base class
        public NewExportAs_Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {

        }
        public NewExportAs_Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
        {

        }
        #endregion


        #region "EventHandling"
        override protected void ButtonDefinition_OnExecute(NameValueMap context)
        {
            if (InventorApplication.ActiveDocument == null) return;
            try
            {
                NewExportForm exportForm = new NewExportForm();
                exportForm.ShowDialog();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n" + e.Source + "\n" + e.StackTrace);
            }
        }
        #endregion
    }
}

