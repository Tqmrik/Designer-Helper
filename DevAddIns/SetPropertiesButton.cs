using Inventor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace DevAddIns
{
    internal class SetPropertiesButton : Button
    {

        #region "Constructors"
        //Use constructors of the base class
        public SetPropertiesButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {

        }
        public SetPropertiesButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
        {

        }
        #endregion


        #region "EventHandling"
        override protected void ButtonDefinition_OnExecute(NameValueMap context)
        {
            //Use json files???
            string currentUserAppDataPath = InventorApplication.CurrentUserAppDataPath;
            EditPropertiesForm editPropertiesForm = new EditPropertiesForm(currentUserAppDataPath);

            currentUserAppDataPath = currentUserAppDataPath.Replace("\\Inventor 2021", "") + "\\ApplicationPlugins\\DevAddIns\\DataSedenumPack\\EditProperties.txt";

            Document activeDocument = InventorApplication.ActiveDocument;

            /*
             * PropsSets
                1 - DisplayName	"Summary Information"	System.String
                2 - DisplayName	"Document Summary Information"	System.String
                3 - DisplayName	"Design Tracking Properties"	System.String
                4 - DisplayName	"User Defined Properties"	System.String
             */

            //foreach (var prop in activeDocument.PropertySets)
            //{
            //var cache = prop;
            //}

            if (activeDocument is null) return;

            //Check for read-only files??
            try
            {
                string[] fileName;
                string filePartNumberProperty;
                string fileDecriptionProperty;
                string checkedByProperty = "";
                string companyNameProperty = "";
                string emptyDate = "01.01.1601"; //Convert to DateTime object???
                DateTime dateTime = DateTime.Today;

                if (!System.IO.File.Exists(currentUserAppDataPath)) editPropertiesForm.ShowDialog();

                StreamReader fileObject = System.IO.File.OpenText(currentUserAppDataPath);
                checkedByProperty = fileObject.ReadLine().Split(':')[1].Trim();
                companyNameProperty = fileObject.ReadLine().Split(':')[1].Trim();

                try
                {
                    string[] separator = { " - " };
                    fileName = activeDocument.DisplayName.Split(separator, 2, StringSplitOptions.RemoveEmptyEntries);
                    filePartNumberProperty = fileName[0];
                    fileDecriptionProperty = System.IO.Path.GetFileNameWithoutExtension(fileName[1]);
                }
                catch (IndexOutOfRangeException e)
                {
                    MessageBox.Show("Не удалось заполнить IProperties, проверьте соответствие наименования файла шаблону: \n Номер - Название");
                    return;
                }


                if (fileName.Length == 2)
                {
                    Transaction oTransaction = InventorApplication.TransactionManager.StartTransaction(InventorApplication.ActiveDocument, "Set IProperties");


                    //--------------------

                    activeDocument.PropertySets[1][1].Value = fileDecriptionProperty; //Summary -> Title
                    activeDocument.PropertySets[1][2].Value = ""; //Summary -> Subject
                    activeDocument.PropertySets[1][3].Value = InventorApplication.GeneralOptions.UserName; //Summary -> Author
                    activeDocument.PropertySets[2][2].Value = "";//Summary->Manager
                    activeDocument.PropertySets[2][3].Value = companyNameProperty.ToUpper();//Summary -> Company
                    activeDocument.PropertySets[2][1].Value = "";//Summary -> Category
                    activeDocument.PropertySets[1][4].Value = ""; //Summary -> Keywords
                    activeDocument.PropertySets[1][5].Value = ""; //Summary -> Comments

                    //--------------------

                    activeDocument.PropertySets[3][2].Value = filePartNumberProperty; //Project -> Part Number
                    //activeDocument.PropertySets[3][37].Value = ""; //Project->Stock Number
                    activeDocument.PropertySets[3][14].Value = fileDecriptionProperty; //Project -> Description
                    activeDocument.PropertySets[1][7].Value = "A"; //Project -> Revision Number
                                                                   //activeDocument.PropertySets[3][3].Value = ""; //Project->Project
                    activeDocument.PropertySets[3][24].Value = InventorApplication.GeneralOptions.UserName;//'Project -> Designer
                    //activeDocument.PropertySets[3][25].Value = ""; Project->Engineer
                    //activeDocument.PropertySets[3][26].Value = ""; Project->Authority
                    //activeDocument.PropertySets[3][4].Value = ""; Project->Cost center
                    //activeDocument.PropertySets[3][21].Value = ""; Project->Estimated cost
                    if (activeDocument.PropertySets[3][1].Value.ToString() == emptyDate)
                    {
                        //01.01.1601 is a checked out flag in Inventor props
                        activeDocument.PropertySets[3][1].Value = dateTime.Date.ToString(); //Project -> Creation Date
                    }
                    //activeDocument.PropertySets[3][12].Value = ""; //Project->Vendor
                    //activeDocument.PropertySets[3][12].Value = ""; //Project->WEB Link


                    //--------------------

                    activeDocument.PropertySets[3][23].Value = ""; //Project -> Status ????????
                    activeDocument.PropertySets[3][5].Value = checkedByProperty; //Status -> Checked By
                    if (activeDocument.PropertySets[3][6].Value.ToString() == emptyDate)
                    {
                        activeDocument.PropertySets[3][6].Value = dateTime.Date; //Project -> Checked Date
                    }

                    activeDocument.PropertySets[3][7].Value = "Voytulevich, Denis"; //Status -> Eng. Approved By
                                                                                    //if(activeDocument.PropertySets[3][8].Value.ToString() == emptyDate)
                                                                                    //{
                                                                                    //    activeDocument.PropertySets[3][8].Value = dateTime.Date.ToString(); //Project->Eng.Approved By
                                                                                    //}

                    //activeDocument.PropertySets[3][19].Value = ""; //Status->Mfg.Approved By
                    //if (activeDocument.PropertySets[3][20].Value.ToString() == emptyDate)
                    //{
                    //        activeDocument.PropertySets[3][20].Value = dateTime.Date.ToString(); //Project->Mfg.Approved By
                    //}

                    if (activeDocument.SubType == "{BBF9FDF1-52DC-11D0-8C04-0800090BE8EC}")
                    {
                        //Also you cannot add twice

                        //foreach (var prop in activeDocument.PropertySets[4])
                        //{
                        //    var cache = prop;
                        //}

                        //Add one more dict and check two dicts

                        List<string> existingProps = new List<string>();
                        foreach (Property drawProp in activeDocument.PropertySets[4])
                        {
                            existingProps.Add(drawProp.Name.ToUpper());
                        }

                        DrawingPropertiesSet propSet = new DrawingPropertiesSet();
                        foreach (var prop in propSet.propNamesDictionary)
                        {
                            if (!existingProps.Contains(prop.Key.ToUpper()))
                            {
                                if (prop.Value == "Date")
                                {
                                    activeDocument.PropertySets[4].Add(new DateTime(1601, 1, 1, 0, 0, 0), prop.Key);
                                }
                                else if (prop.Value == "Text")
                                {
                                    activeDocument.PropertySets[4].Add(String.Empty, prop.Key);
                                }
                            }
                        }


                        //Check if it exist or not, perhaps you might consider using an REGEX, it really fits in there
                        //for example we can check for DATE%n pattern and if it's empty go to next one???
                        activeDocument.PropertySets[4]["DATE1"].Value = dateTime.Date;
                        activeDocument.PropertySets[4]["DATE2"].Value = new DateTime(1601, 1, 1, 0, 0, 0);
                        activeDocument.PropertySets[4]["DATE3"].Value = new DateTime(1601, 1, 1, 0, 0, 0);
                        activeDocument.PropertySets[4]["DocNumber"].Value = filePartNumberProperty;
                        activeDocument.PropertySets[4]["HARDNESS"].Value = String.Empty;
                        activeDocument.PropertySets[4]["MADE1"].Value = InventorApplication.GeneralOptions.UserName;
                        activeDocument.PropertySets[4]["MADE2"].Value = String.Empty;
                        activeDocument.PropertySets[4]["MADE3"].Value = String.Empty;
                        activeDocument.PropertySets[4]["NC1"].Value = "-";
                        activeDocument.PropertySets[4]["NC2"].Value = String.Empty;
                        activeDocument.PropertySets[4]["NC3"].Value = String.Empty;
                        activeDocument.PropertySets[4]["NE1"].Value = "-";
                        activeDocument.PropertySets[4]["NE2"].Value = String.Empty;
                        activeDocument.PropertySets[4]["NE3"].Value = String.Empty;
                        activeDocument.PropertySets[4]["REV1"].Value = "A";
                        activeDocument.PropertySets[4]["REV2"].Value = String.Empty;
                        activeDocument.PropertySets[4]["REV3"].Value = String.Empty;
                        activeDocument.PropertySets[4]["REVIEWED1"].Value = checkedByProperty;
                        activeDocument.PropertySets[4]["REVIEWED2"].Value = String.Empty;
                        activeDocument.PropertySets[4]["REVIEWED3"].Value = String.Empty;
                        activeDocument.PropertySets[4]["SURFACE"].Value = String.Empty;
                        activeDocument.PropertySets[4]["VDS_Category"].Value = "Engineering";

                    }

                    oTransaction.End();
                }
                else
                {
                    MessageBox.Show("Не удалось заполнить IProperties, проверьте соответствие наименования файла шаблону: \n Номер - Название");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\n AddIn: Sedenum Pack");
            }


        }
        #endregion
    }
}
