using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using Inventor;
using Microsoft.VisualBasic.Compatibility.VB6;

namespace DevAddIns
{
	//Class to create buttons from
    internal abstract class Button
	{
		#region "Data"
		//App
		private static Inventor.Application m_inventorApplication;

		//Definition of the button
		public ButtonDefinition m_buttonDefinition;

		//A delegate to point to a method that will handle the event
		private ButtonDefinitionSink_OnExecuteEventHandler ButtonDefinition_OnExecuteEventDelegate;
        #endregion

        #region "Properties"
        
		//Props for some reason
		//Quality of life??
        public static Inventor.Application InventorApplication
		{
			set
			{
				m_inventorApplication = value;
			}
			get
			{
				return m_inventorApplication;
			}
		}

		//To use in AddButton method
		public Inventor.ButtonDefinition ButtonDefinition
		{
			get
			{
				return m_buttonDefinition;
			}
		}
        #endregion


        #region "Methods"
        //All the arguments are from inventor's method to add a button
        public Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
		{
			try
            {
				//get IPictureDisp for icons
				stdole.IPictureDisp standardIconIPictureDisp;
				standardIconIPictureDisp = (stdole.IPictureDisp)Support.ImageToIPicture(standardIcon);

				stdole.IPictureDisp largeIconIPictureDisp;
				largeIconIPictureDisp = (stdole.IPictureDisp)Support.ImageToIPicture(largeIcon);

				m_buttonDefinition = m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition(displayName, internalName, commandType, clientId, description, tooltip, standardIconIPictureDisp, largeIconIPictureDisp, buttonDisplayType);

				m_buttonDefinition.Enabled = true;

				ButtonDefinition_OnExecuteEventDelegate = new ButtonDefinitionSink_OnExecuteEventHandler(ButtonDefinition_OnExecute);
				m_buttonDefinition.OnExecute += ButtonDefinition_OnExecuteEventDelegate;
			}
			catch(Exception e)
            {
				MessageBox.Show(e.Message);
            }
			
		}

		//Override without an icon
		public Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
		{
			try
			{
				//create button definition
				m_buttonDefinition = m_inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition(displayName, internalName, commandType, clientId, description, tooltip, Type.Missing, Type.Missing, buttonDisplayType);

				//enable the button
				m_buttonDefinition.Enabled = true;

				//connect the button event sink
				ButtonDefinition_OnExecuteEventDelegate = new ButtonDefinitionSink_OnExecuteEventHandler(ButtonDefinition_OnExecute);
				m_buttonDefinition.OnExecute += ButtonDefinition_OnExecuteEventDelegate;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}

		abstract protected void ButtonDefinition_OnExecute(NameValueMap context);
        #endregion
    }
}
