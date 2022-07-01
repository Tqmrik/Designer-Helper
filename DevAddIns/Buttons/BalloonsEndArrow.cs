using Inventor;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DevAddIns
{
    internal class BalloonsEndArrow : Button
    {
        #region "Constructors"
        //Use constructors of the base class
        public BalloonsEndArrow(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {

        }
        public BalloonsEndArrow(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
        {

        }
        #endregion


        #region "EventHandling"
        override protected void ButtonDefinition_OnExecute(NameValueMap context)
        {
            SelectSet ballons = InventorApplication.ActiveDocument.SelectSet;
            foreach(var obj in ballons)
            {
                if(obj is Balloon)
                {
                    var temp = (Balloon)obj;
                    temp.Leader.ArrowheadType = ArrowheadTypeEnum.kSmallDotArrowheadType;
                }
            }
        }
        #endregion
    }
}

