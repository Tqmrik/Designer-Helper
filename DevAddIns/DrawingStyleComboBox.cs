using Inventor;
using Microsoft.VisualBasic.Compatibility.VB6;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DevAddIns
{
    internal class DrawingStyleComboBox : ComboBox
    {

        #region "Constructors"
        //All the arguments are from inventor's method to add a comboBox
        public DrawingStyleComboBox(string displayName, string internalName, CommandTypesEnum commandType, int dropDownWidthPx, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType) : base(displayName, internalName, commandType, dropDownWidthPx, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {
        }

        //Override without an icon
        public DrawingStyleComboBox(string displayName, string internalName, CommandTypesEnum commandType, int dropDownWidthPx, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType) : base(displayName, internalName, commandType, dropDownWidthPx, clientId, description, tooltip, buttonDisplayType)
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
