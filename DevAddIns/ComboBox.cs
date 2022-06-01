using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Inventor;
using Microsoft.VisualBasic.Compatibility.VB6;

namespace DevAddIns
{
	//Class to create buttons from
	internal abstract class ComboBox
	{
		#region "Data"
		//App
		private static Inventor.Application m_inventorApplication;

		//Definition of the button
		public ComboBoxDefinition m_comboBoxDefinition;

		//A delegate to point to a method that will handle the event
		private ComboBoxDefinitionSink_OnSelectEventHandler ComboBoxDefinition_OnSelectEventDelegate;
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
		public Inventor.ComboBoxDefinition ComboBoxDefinition
		{
			get
			{
				return m_comboBoxDefinition;
			}
		}

		#endregion


		#region "Methods"
		//All the arguments are from inventor's method to add a comboBox
		public ComboBox(string displayName, string internalName, CommandTypesEnum commandType, int dropDownWidthPx, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
		{
			try
			{
				//get IPictureDisp for icons
				stdole.IPictureDisp standardIconIPictureDisp;
				standardIconIPictureDisp = (stdole.IPictureDisp)Support.ImageToIPicture(standardIcon);

				stdole.IPictureDisp largeIconIPictureDisp;
				largeIconIPictureDisp = (stdole.IPictureDisp)Support.ImageToIPicture(largeIcon);

				m_comboBoxDefinition = InventorApplication.CommandManager.ControlDefinitions.AddComboBoxDefinition(displayName, internalName, commandType, dropDownWidthPx, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType);

				m_comboBoxDefinition.Enabled = true;

				ComboBoxDefinition_OnSelectEventDelegate = new ComboBoxDefinitionSink_OnSelectEventHandler(ComboBoxDefinition_OnSelect);
				m_comboBoxDefinition.OnSelect += ComboBoxDefinition_OnSelectEventDelegate;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}

		}

		//Override without an icon
		public ComboBox(string displayName, string internalName, CommandTypesEnum commandType, int dropDownWidthPx, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
		{
			try
			{
				//create button definition
				m_comboBoxDefinition = InventorApplication.CommandManager.ControlDefinitions.AddComboBoxDefinition(displayName, internalName, commandType, dropDownWidthPx, clientId, description, tooltip, Type.Missing, Type.Missing, buttonDisplayType);

				//enable the button
				m_comboBoxDefinition.Enabled = true;

				//connect the button event sink
				ComboBoxDefinition_OnSelectEventDelegate = new ComboBoxDefinitionSink_OnSelectEventHandler(ComboBoxDefinition_OnSelect);
				m_comboBoxDefinition.OnSelect += ComboBoxDefinition_OnSelectEventDelegate;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.ToString());
			}
		}
		public void Populate(string value, int index)
		{
			if (this == null) return;
			m_comboBoxDefinition.AddItem(value, index);

		}
		public void Populate(string value)
		{
			if (this == null) return;
			m_comboBoxDefinition.AddItem(value);

		}
		public void Populate(string[] values)
        {
			if (this == null) return;

			foreach (string val in values)
			{
				m_comboBoxDefinition.AddItem(val);
			}
		}
		public void Populate(List<string> values)
        {

			if (this == null) return;

			foreach(string val in values)
            {
				m_comboBoxDefinition.AddItem(val);
            }
        }
		public void Populate(Dictionary<string, int> itemIndexs)
        {
			if (this == null) return;
			foreach (KeyValuePair<string, int> val in itemIndexs)
			{
				m_comboBoxDefinition.AddItem(val.Key, val.Value);
			}

		}

		abstract protected void ComboBoxDefinition_OnSelect(NameValueMap context);
		#endregion
	}
}
