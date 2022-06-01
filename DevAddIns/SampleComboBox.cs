using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Inventor;
using Microsoft.VisualBasic.Compatibility.VB6;

namespace DevAddIns
{
    internal class SampleComboBox : ComboBox
    {

		#region "Constructors"
		//All the arguments are from inventor's method to add a comboBox
		public SampleComboBox(string displayName, string internalName, CommandTypesEnum commandType, int dropDownWidthPx, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType) : base(displayName,internalName, commandType, dropDownWidthPx, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
		{
		}

		//Override without an icon
		public SampleComboBox(string displayName, string internalName, CommandTypesEnum commandType, int dropDownWidthPx, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType) : base(displayName, internalName, commandType, dropDownWidthPx, clientId, description, tooltip, buttonDisplayType)
		{
		}
		#endregion

		#region "EventHAndlers"
		override protected void ComboBoxDefinition_OnSelect(NameValueMap context)
        {
			//Your code goes here
        }
		#endregion
	}
}
