using Inventor;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace DevAddIns
{
    internal class SetProperties_Button : Button_Object
    {

        #region "Constructors"
        //Use constructors of the base class
        public SetProperties_Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Image standardIcon, Image largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {

        }
        public SetProperties_Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Icon standardIcon, Icon largeIcon, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, standardIcon, largeIcon, buttonDisplayType)
        {

        }
        public SetProperties_Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
            : base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
        {

        }
        #endregion


        #region "EventHandling"
        override protected void ButtonDefinition_OnExecute(NameValueMap context)
        {
            //Use json files???
            string currentUserAppDataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            EditPropertiesForm editPropertiesForm = new EditPropertiesForm(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData));

            ApplicationAddIn sedenumAppAddIn = InventorApplication.ApplicationAddIns.ItemById["{fb869b0a-a71f-4590-89fc-ff707daa96c3}".ToUpperInvariant()];
            string addInPath = System.IO.Path.GetDirectoryName(sedenumAppAddIn.Location);

            if (!System.IO.Directory.Exists(currentUserAppDataPath))
            {
                System.IO.Directory.CreateDirectory(currentUserAppDataPath);
            }

            currentUserAppDataPath = addInPath + "\\EditProperties.txt";

            if (!System.IO.File.Exists(currentUserAppDataPath))
            {
                var fileStream = System.IO.File.Create(currentUserAppDataPath);
                fileStream.Close();
            }



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

                    if(String.IsNullOrEmpty(activeDocument.PropertySets[1][1].Expression))
                    {
                        activeDocument.PropertySets[1][1].Value = fileDecriptionProperty; //Summary -> Title
                    }

                    if (String.IsNullOrEmpty(activeDocument.PropertySets[1][2].Expression))
                    {
                        activeDocument.PropertySets[1][2].Value = ""; //Summary -> Subject
                    }

                    if (String.IsNullOrEmpty(activeDocument.PropertySets[1][3].Expression))
                    {
                        activeDocument.PropertySets[1][3].Value = InventorApplication.GeneralOptions.UserName; //Summary -> Author
                    }

                    if (String.IsNullOrEmpty(activeDocument.PropertySets[2][2].Expression))
                    {
                        activeDocument.PropertySets[2][2].Value = "";//Summary->Manager
                    }

                    if (String.IsNullOrEmpty(activeDocument.PropertySets[2][3].Expression))
                    {
                        activeDocument.PropertySets[2][3].Value = companyNameProperty;//Summary -> Company
                    }

                    if (String.IsNullOrEmpty(activeDocument.PropertySets[2][1].Expression))
                    {
                        activeDocument.PropertySets[2][1].Value = "";//Summary -> Category
                    }

                    if (String.IsNullOrEmpty(activeDocument.PropertySets[1][4].Expression))
                    {
                        activeDocument.PropertySets[1][4].Value = ""; //Summary -> Keywords
                    }

                    if (String.IsNullOrEmpty(activeDocument.PropertySets[1][5].Expression))
                    {
                        activeDocument.PropertySets[1][5].Value = ""; //Summary -> Comments
                    }

                    //--------------------

                    activeDocument.PropertySets[3][2].Value = filePartNumberProperty; //Project -> Part Number
                    //activeDocument.PropertySets[3][37].Value = ""; //Project->Stock Number
                    activeDocument.PropertySets[3][14].Value = fileDecriptionProperty; //Project -> Description
                    if(String.IsNullOrEmpty(activeDocument.PropertySets[1][7].Expression))
                    {
                        activeDocument.PropertySets[1][7].Value = "A"; //Project -> Revision Number
                    }
                                                                   //activeDocument.PropertySets[3][3].Value = ""; //Project->Project
                    if(String.IsNullOrEmpty(activeDocument.PropertySets[3][24].Expression))
                    {
                        activeDocument.PropertySets[3][24].Value = InventorApplication.GeneralOptions.UserName; //'Project -> Designer
                    }
                    
                    //activeDocument.PropertySets[3][25].Value = ""; Project->Engineer
                    //activeDocument.PropertySets[3][26].Value = ""; Project->Authority
                    //activeDocument.PropertySets[3][4].Value = ""; Project->Cost center
                    //activeDocument.PropertySets[3][21].Value = ""; Project->Estimated cost

                    if (String.IsNullOrEmpty(activeDocument.PropertySets[3][1].Expression))
                    {
                        //01.01.1601 is a checked out flag in Inventor props
                        activeDocument.PropertySets[3][1].Value = dateTime.ToString("d"); //Project -> Creation Date
                    }

                    //activeDocument.PropertySets[3][12].Value = ""; //Project->Vendor
                    //activeDocument.PropertySets[3][12].Value = ""; //Project->WEB Link

                    //--------------------
                    if (String.IsNullOrEmpty(activeDocument.PropertySets[3][23].Expression))
                    {
                        activeDocument.PropertySets[3][23].Value = ""; //Project -> Status ????????
                    }

                    //if(String.IsNullOrEmpty(Inventor.PropertiesForDesignTrackingPropertiesEnum.kCheckedByDesignTrackingProperties)) //??????

                    if(String.IsNullOrEmpty(activeDocument.PropertySets[3][5].Expression))
                    {
                        activeDocument.PropertySets[3][5].Value = checkedByProperty; //Status -> Checked By
                    }

                    if (String.IsNullOrEmpty(activeDocument.PropertySets[3][6].Expression))
                    {
                        activeDocument.PropertySets[3][6].Value = dateTime.ToString("d"); //Project -> Checked Date
                    }

                    if (String.IsNullOrEmpty(activeDocument.PropertySets[3][7].Expression))
                    {
                        activeDocument.PropertySets[3][7].Value = "Voytulevich, Denis"; //Status -> Eng. Approved By
                    }
                    
                    //if(activeDocument.PropertySets[3][8].Value.ToString() == emptyDate)
                    //{
                    //    activeDocument.PropertySets[3][8].Value = dateTime.Date.ToString(); //Project->Eng.Approved By
                    //}

                    //activeDocument.PropertySets[3][19].Value = ""; //Status->Mfg.Approved By

                    //if (activeDocument.PropertySets[3][20].Value.ToString() == emptyDate)
                    //{
                    //        activeDocument.PropertySets[3][20].Value = dateTime.Date.ToString(); //Project->Mfg.Approved By
                    //}

                    if (activeDocument.IsDrawingDocument())
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
                        if(String.IsNullOrEmpty(activeDocument.PropertySets[4]["DATE1"].Expression))
                        {
                            activeDocument.PropertySets[4]["DATE1"].Value = dateTime.Date;
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["DATE2"].Expression))
                        {
                            activeDocument.PropertySets[4]["DATE2"].Value = new DateTime(1601, 1, 1, 0, 0, 0);
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["DATE3"].Expression))
                        {
                            activeDocument.PropertySets[4]["DATE3"].Value = new DateTime(1601, 1, 1, 0, 0, 0);
                        }
                        
                        activeDocument.PropertySets[4]["DocNumber"].Value = filePartNumberProperty;

                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["HARDNESS"].Expression))
                        {
                            activeDocument.PropertySets[4]["HARDNESS"].Value = String.Empty;
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["MADE1"].Expression))
                        {
                            activeDocument.PropertySets[4]["MADE1"].Value = activeDocument.PropertySets[3][24].Value;
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["MADE2"].Expression))
                        {
                            activeDocument.PropertySets[4]["MADE2"].Value = String.Empty;
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["MADE3"].Expression))
                        {
                            activeDocument.PropertySets[4]["MADE3"].Value = String.Empty;
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["NC1"].Expression))
                        {
                            activeDocument.PropertySets[4]["NC1"].Value = "-";
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["NC2"].Expression))
                        {
                            activeDocument.PropertySets[4]["NC2"].Value = String.Empty;
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["NC3"].Expression))
                        {
                            activeDocument.PropertySets[4]["NC3"].Value = String.Empty;
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["NE1"].Expression))
                        {
                            activeDocument.PropertySets[4]["NE1"].Value = "-";
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["NE2"].Expression))
                        {
                            activeDocument.PropertySets[4]["NE2"].Value = String.Empty;
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["NE3"].Expression))
                        {
                            activeDocument.PropertySets[4]["NE3"].Value = String.Empty;
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["REV1"].Expression))
                        {
                            activeDocument.PropertySets[4]["REV1"].Value = "A";
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["REV2"].Expression))
                        {
                            activeDocument.PropertySets[4]["REV2"].Value = String.Empty;
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["REV3"].Expression))
                        {
                            activeDocument.PropertySets[4]["REV3"].Value = String.Empty;
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["REVIEWED1"].Expression))
                        {
                            activeDocument.PropertySets[4]["REVIEWED1"].Value = activeDocument.PropertySets[3][5].Value;
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["REVIEWED2"].Expression))
                        {
                            activeDocument.PropertySets[4]["REVIEWED2"].Value = String.Empty;
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["REVIEWED3"].Expression))
                        {
                            activeDocument.PropertySets[4]["REVIEWED3"].Value = String.Empty;
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["SURFACE"].Expression))
                        {
                            activeDocument.PropertySets[4]["SURFACE"].Value = String.Empty;
                        }
                        if (String.IsNullOrEmpty(activeDocument.PropertySets[4]["VDS_Category"].Expression))
                        {
                            activeDocument.PropertySets[4]["VDS_Category"].Value = "Engineering";
                        }
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
