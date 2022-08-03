using Inventor;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;



namespace DevAddIns
{
    internal class ChangeToMetricButton : Button
    {
        #region "Constructors"
        //Use constructors of the base class
        public ChangeToMetricButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {

        }
        public ChangeToMetricButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
        {

        }
        #endregion


        #region "EventHandling"
        override protected void ButtonDefinition_OnExecute(NameValueMap context)
        {
            
            Document activeDocument = InventorApplication.ActiveDocument;
                if (activeDocument == null)
                {
                    return;
                }

            activeDocument.UnitsOfMeasure.LengthUnits = UnitsTypeEnum.kMillimeterLengthUnits;
            activeDocument.UnitsOfMeasure.MassUnits = UnitsTypeEnum.kKilogramMassUnits;
            activeDocument.UnitsOfMeasure.AngleUnits = UnitsTypeEnum.kDegreeAngleUnits;
        }
        #endregion
    }
}

