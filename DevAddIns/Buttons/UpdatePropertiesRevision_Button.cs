using Inventor;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace DevAddIns
{
    internal class UpdatePropertiesRevision_Button : Button_Object
    {
        #region "Constructors"
        //Use constructors of the base class
        public UpdatePropertiesRevision_Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {

        }
        public UpdatePropertiesRevision_Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
        {

        }
        #endregion


        #region "EventHandling"
        override protected void ButtonDefinition_OnExecute(NameValueMap context)
        {
            
            Document activeDocument = InventorApplication.ActiveDocument;
            if (activeDocument is null) return;
            
            string Revision = String.Empty;
            string maxValue = String.Empty;

            try
            {
                //Would need to really think about that part 
                foreach (var row in new string[] { "REV1", "REV2", "REV3" })
                {
                    string cache = activeDocument.PropertySets[4][row].Value.ToString();
                    if (row == "REV1" && cache == String.Empty) //Case if first row is empty
                    {
                        revisionIterator(Revision, maxValue, activeDocument);
                        return;
                    }

                    if (Revision == String.Empty && maxValue == String.Empty) //First iteration
                    {
                        Revision = cache;
                        maxValue = row;
                    }
                    else if (cache == string.Empty) //Case if any other row is empty
                    {
                        revisionIterator(Revision[Revision.Length - 1].ToString(), maxValue, activeDocument);
                        return;
                    }
                    else if (cache[cache.Length - 1] > Revision[Revision.Length - 1])
                    {
                        //Case if there is no empty rows left and we are forced to search for the max char
                        Revision = cache;
                        maxValue = row;
                    }
                }
                //Case if there is no empty rows, so the maxed row must be replaced
                revisionIterator(Revision[Revision.Length - 1].ToString(), maxValue, activeDocument);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message + "\nAddIn: Sedenum Pack\nMethod: UpdatePropertiesRevision");
            }

            
        }

        public void revisionIterator(string Revision, string maxValue, Document activeDocument)
        {
            //bool propExist = false;

            //foreach(Property prop in activeDocument.PropertySets[4])
            //{
            //    if (prop.Name == "_PrefixRevesionSedenum") propExist = true;
            //}

            //if (!propExist)
            //{
            //    activeDocument.PropertySets[4].Add(String.Empty, "_PrefixRevesionSedenum");
            //}

            //Property prefixProperty = activeDocument.PropertySets[4]["_PrefixRevesionSedenum"];


            if (Revision == String.Empty)
            {
                //If there is no max revision then it must be a first revision
                Revision = "A";
                maxValue = "REV1";
            }
            else
            {
                //Add the revision(next char in the unicode)
                Revision = ((char)(Char.Parse(Revision) + 1)).ToString();
                //if(Char.Parse(Revision) > 'Z')
                //{
                //    Revision = "A";
                //    prefixProperty.Value += "A";

                //}

                //Logic here: Currect maxVal = 2 => (2 % 3 + 1) = 3; Current maxVal = 3 => (3 % 3 + 1) = 1
                maxValue = "REV" + ((int.Parse(maxValue[maxValue.Length - 1].ToString()) % 3) + 1).ToString();
                
            }

            // TODO: revisionIterator -> Change file to something else + add new file
            EditPropertiesForm editPropertiesForm = new EditPropertiesForm(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData));

            if (!System.IO.File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData))) editPropertiesForm.ShowDialog();

            StreamReader fileObject = System.IO.File.OpenText(GlobalVar.editPropertiesFile);
            string checkedByProperty = fileObject.ReadLine().Split(':')[1].Trim();
            fileObject.Close();

            Transaction oTransaction = InventorApplication.TransactionManager.StartTransaction(InventorApplication.ActiveDocument, "Update Revision");

            string lastCharacter = maxValue[maxValue.Length - 1].ToString();

            activeDocument.PropertySets[4][maxValue].Value = Revision; //Custom revision
            activeDocument.PropertySets[4]["DATE" + lastCharacter].Value = DateTime.Today;
            activeDocument.PropertySets[4]["MADE" + lastCharacter].Value = InventorApplication.GeneralOptions.UserName;
            activeDocument.PropertySets[4]["NC" + lastCharacter].Value = "-";
            activeDocument.PropertySets[4]["NE" + lastCharacter].Value = "-";
            activeDocument.PropertySets[4]["REVIEWED" + maxValue[maxValue.Length - 1]].Value = checkedByProperty;

            activeDocument.PropertySets[1][7].Value = Revision; //Document revision in properties

            oTransaction.End();
        }
        #endregion
    }
}

// TODO: Also update the revision in the classic properties