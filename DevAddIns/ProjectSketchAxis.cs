﻿using System;
using System.Windows.Forms;
using System.Drawing;
using Inventor;


namespace DevAddIns
{
    class ProjectSketchAxis : Button
    {
		#region "Constructors"
		public ProjectSketchAxis(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
			: base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
		{

		}
		public ProjectSketchAxis(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
			: base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
		{

		}
		#endregion

		#region "EventHandling"
		override protected void ButtonDefinition_OnExecute(NameValueMap context)
		{
			
		}
		#endregion
	}
}