using Inventor;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace DevAddIns
{
    internal class UpdatePropertiesRevisionButton : Button
    {
        #region "Constructors"
        //Use constructors of the base class
        public UpdatePropertiesRevisionButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {

        }
        public UpdatePropertiesRevisionButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
        {

        }
        #endregion


        #region "EventHandling"
        override protected void ButtonDefinition_OnExecute(NameValueMap context)
        {
            string currentUserAppDataPath = InventorApplication.CurrentUserAppDataPath;
            EditPropertiesForm editPropertiesForm = new EditPropertiesForm(currentUserAppDataPath);
            currentUserAppDataPath = currentUserAppDataPath.Replace("\\Inventor 2021", "") + "\\ApplicationPlugins\\DevAddIns\\DataSedenumPack\\EditProperties.txt";

            if (!System.IO.File.Exists(currentUserAppDataPath)) editPropertiesForm.ShowDialog();

            StreamReader fileObject = System.IO.File.OpenText(currentUserAppDataPath);
            string checkedByProperty = fileObject.ReadLine().Split(':')[1].Trim();
            fileObject.Close();

            string nextRevision = "A";
            string maxValue = String.Empty;


            Document activeDocument = InventorApplication.ActiveDocument;
            if (activeDocument is null) return;

            Transaction oTransaction = InventorApplication.TransactionManager.StartTransaction(InventorApplication.ActiveDocument, "Update Revision");

            //Would need to really think about that part 
            foreach(var row in new string[] {"REV1", "REV2", "REV3" })
            {
                if (activeDocument.PropertySets[4][row].Value == String.Empty)
                {
                    maxValue = row;
                    nextRevision = ((char)(Char.Parse(nextRevision) + 1)).ToString();
                    break;
                }
                else if (maxValue == String.Empty)
                {

                    maxValue = row;
                    nextRevision = activeDocument.PropertySets[4][row].Value.ToString();
                    continue;
                }
                else if (Char.Parse(nextRevision) > Char.Parse(maxValue))
                {
                    nextRevision = ((char)((char)activeDocument.PropertySets[4][row].Value + 1)).ToString();
                    maxValue = row;
                }
            }
            //end logic

            string lastCharacter = maxValue[maxValue.Length - 1].ToString();

            activeDocument.PropertySets[4][maxValue].Value = nextRevision;
            activeDocument.PropertySets[4]["DATE" + lastCharacter].Value = DateTime.Today;
            activeDocument.PropertySets[4]["MADE" + lastCharacter].Value = InventorApplication.GeneralOptions.UserName;
            activeDocument.PropertySets[4]["NC" + lastCharacter].Value = "-";
            activeDocument.PropertySets[4]["NE" + lastCharacter].Value = "-";
            activeDocument.PropertySets[4]["REVIEWED" + maxValue[maxValue.Length - 1]].Value = checkedByProperty;

            oTransaction.End();
        }
        #endregion
    }
}

