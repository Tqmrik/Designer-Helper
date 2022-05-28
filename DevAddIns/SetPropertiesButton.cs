using System;
using System.Windows.Forms;
using System.Drawing;
using Inventor;

namespace DevAddIns
{
    internal class SetPropertiesButton : Button
    {
		#region "Constructors"
		//Use constructors of the base class
		public SetPropertiesButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Icon standardIcon, Icon largeIcon, ButtonDisplayEnum buttonDisplayType)
			: base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
		{

		}
		public SetPropertiesButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
			: base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
		{

		}
        #endregion


        #region "Event handling of the button"
        override protected void ButtonDefinition_OnExecute(NameValueMap context)
        {
			MessageBox.Show("Hello But changed");
        }
        #endregion
    }
}
