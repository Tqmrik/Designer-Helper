using Inventor;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DevAddIns
{
    internal class EditPropertiesButton : Button
    {
        #region "Constructors"
        //Use constructors of the base class
        public EditPropertiesButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {

        }
        public EditPropertiesButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
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
                //EditPropertiesForm editProperties = new EditPropertiesForm(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData));
                //editProperties.ShowDialog();

                IPropertiesForm editProperties = new IPropertiesForm();
                editProperties.ShowDialog();

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message + "\nAddIn: Sedenum Pack\nMethod:EditPropertiesButton");
            }
        }
        #endregion
    }
}

