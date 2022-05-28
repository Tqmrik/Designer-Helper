using System;
using System.Windows.Forms;
using System.Drawing;
using Inventor;

namespace DevAddIns
{
	internal class SampleButton : Button
	{
		#region "Constructors"
		//Use constructors of the base class
		public SampleButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Icon standardIcon, Icon largeIcon, ButtonDisplayEnum buttonDisplayType)
			: base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
		{

		}
		public SampleButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
			: base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
		{

		}
		#endregion


		#region "EventHandling"
		override protected void ButtonDefinition_OnExecute(NameValueMap context)
		{
			//Code for event handling
		}
		#endregion
	}
}

