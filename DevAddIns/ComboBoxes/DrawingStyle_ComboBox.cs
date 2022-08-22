using Inventor;
using Microsoft.VisualBasic.Compatibility.VB6;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DevAddIns
{
    internal class DrawingStyle_ComboBox : ComboBox_Object
    {

        const string eskdDrawingStyle = "ESKD";
        const string defaultDrawingStyle = "Default";
        const string eskdAddClassID = "{005B21FC-8537-4926-9F57-3A3216C294C3}";

        #region "Constructors"
        //All the arguments are from inventor's method to add a comboBox
        public DrawingStyle_ComboBox(string displayName, string internalName, CommandTypesEnum commandType, int dropDownWidthPx, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType) : base(displayName, internalName, commandType, dropDownWidthPx, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {
            Populate(new string[]{ defaultDrawingStyle, eskdDrawingStyle });
        }

        //Override without an icon
        public DrawingStyle_ComboBox(string displayName, string internalName, CommandTypesEnum commandType, int dropDownWidthPx, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType) : base(displayName, internalName, commandType, dropDownWidthPx, clientId, description, tooltip, buttonDisplayType)
        {
            Populate(new string[] { defaultDrawingStyle, eskdDrawingStyle });
        }
        public DrawingStyle_ComboBox(string displayName, string internalName, CommandTypesEnum commandType, int dropDownWidthPx, string clientId, string description, string tooltip) : base(displayName, internalName, commandType, dropDownWidthPx, clientId, description, tooltip)
        {
            Populate(new string[] { defaultDrawingStyle, eskdDrawingStyle });
        }

        #endregion

        #region "EventHAndlers"
        override protected void ComboBoxDefinition_OnSelect(NameValueMap context)
        {
            switch(m_comboBoxDefinition.Text)
            {
                case eskdDrawingStyle:
                    InventorApplication.ApplicationAddIns.ItemById[eskdAddClassID].Activate();
                    return;
                default:
                    InventorApplication.ApplicationAddIns.ItemById[eskdAddClassID].Deactivate();
                    return;
            }
        }
        #endregion
    }
}
