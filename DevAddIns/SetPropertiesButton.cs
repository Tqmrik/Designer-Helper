using Inventor;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;


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
            Document activeDocument = InventorApplication.ActiveDocument;
            string filePath = "C:\\Temp\\InventorMacrosSetIProperties.txt";
            EditPropertiesForm editPropertiesForm = new EditPropertiesForm();

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

            try
            {
                string[] fileName;
                string filePartNumberProperty;
                string fileDecriptionProperty;
                string checkedByProperty = "";
                string companyNameProperty = "";
                string emptyDate = "01.01.1601"; //Convert to DateTime object???
                DateTime dateTime = DateTime.Today;

                if (System.IO.File.Exists(filePath))
                {
                    StreamReader fileObject = System.IO.File.OpenText(filePath);
                    checkedByProperty = fileObject.ReadLine().Split(':')[1];
                    companyNameProperty = fileObject.ReadLine().Split(':')[1];
                }
                else
                {
                    editPropertiesForm.Show();
                    if (System.IO.File.Exists(filePath))
                    {
                        StreamReader fileObject = System.IO.File.OpenText(filePath);
                        checkedByProperty = fileObject.ReadLine().Split(':')[1];
                        companyNameProperty = fileObject.ReadLine().Split(':')[1];
                    }
                }

                try
                {
                    string[] separator = { " - " };
                    fileName = activeDocument.DisplayName.Split(separator, 2, StringSplitOptions.RemoveEmptyEntries);
                    filePartNumberProperty = fileName[0];
                    fileDecriptionProperty = System.IO.Path.GetFileNameWithoutExtension(fileName[1]);
                }
                catch(IndexOutOfRangeException e)
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

                        ////activeDocument.PropertySets[4][1].Value = "";
                        //activeDocument.PropertySets[4][2].Value = ""; //Not working
                        //activeDocument.PropertySets[4][3].Value = 
                        //activeDocument.PropertySets[4][4].Value = "";
                        ////activeDocument.PropertySets[4][5].Value = "";
                        //activeDocument.PropertySets[4][6].Value = InventorApplication.GeneralOptions.UserName;
                        ////activeDocument.PropertySets[4][7].Value = "";
                        ////activeDocument.PropertySets[4][8].Value = "";
                        //activeDocument.PropertySets[4][9].Value = "-";
                        ////activeDocument.PropertySets[4][10].Value = "";
                        ////activeDocument.PropertySets[4][11].Value = "";
                        //activeDocument.PropertySets[4][12].Value = "-";
                        ////activeDocument.PropertySets[4][13].Value = "";
                        ////activeDocument.PropertySets[4][14].Value = "";
                        //activeDocument.PropertySets[4][15].Value = "A";
                        ////activeDocument.PropertySets[4][16].Value = "";
                        ////activeDocument.PropertySets[4][17].Value = "";
                        //activeDocument.PropertySets[4][18].Value = checkedByProperty;
                        ////activeDocument.PropertySets[4][19].Value = "";
                        ////activeDocument.PropertySets[4][20].Value = "";
                        ////activeDocument.PropertySets[4][21].Value = "";
                        ////activeDocument.PropertySets[4][22].Value = "";


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

/*
 * 
 * Option Explicit

Public Sub SetIProps()

    Dim activeDoc As Document
    Dim fso As Object
    Dim textFile As Object
    Dim fileNameSplit As Variant
    Dim filePath As String
    
    Set activeDoc = ThisApplication.ActiveDocument
    
    Set fso = CreateObject("Scripting.FileSystemObject")
    


    If activeDoc Is Nothing Then
    'Exit Sub if you are on a Zero Document
        Exit Sub
    End If
    
    
    'Props set
    Dim descriptionProp As Variant
    Dim checkedByProp As Variant
    Dim companyNameProp As Variant
    
    filePath = "C:\Temp\InventorMacrosSetIProperties.txt" 'File name of the form's file
    
    
    If fso.FileExists(filePath) Then 'Check if file exists, if so read values from the file, otherwise call UserForm
        Set textFile = fso.OpenTextFile(filePath, 1)
        checkedByProp = Split(textFile.ReadLine, " : ")[1]
        companyNameProp = Split(textFile.ReadLine, " : ")[1]
    Else
        setPropertiesForm.Show
        If fso.FileExists(filePath) Then 'If someone closed file by red cross then just exit whole sub
            Set textFile = fso.OpenTextFile(filePath, 1)
            checkedByProp = Split(textFile.ReadLine, " : ")[1]
            companyNameProp = Split(textFile.ReadLine, " : ")[1]
            Exit Sub
        End If
    End If
    
    
    fileNameSplit = Split(activeDoc.DisplayName, " - ", 2) 'Split file name by -
    
    
    If (UBound(fileNameSplit) = 1) Then 'If split is successful then continue, otherwise displays message
    
        ' Create a new transaction to wrap the change of the IProperties
        ' into a single undo.
        Dim oTrans As Transaction
        Set oTrans = ThisApplication.TransactionManager.StartTransaction( _
        ThisApplication.ActiveDocument, _
        "Change IProperties")
        
        descriptionProp = fso.GetBaseName(fileNameSplit[1]) 'Get rid of file extension
        
        activeDoc.PropertySets[1][1].Value = descriptionProp 'Summary -> Title
        activeDoc.PropertySets[1][2].Value = "" 'Summary -> Subject
        activeDoc.PropertySets[1][3].Value = ThisApplication.GeneralOptions.UserName 'Summary -> Author
        '//activeDoc.PropertySets[2][2].Value = "" 'Summary -> Manager
        activeDoc.PropertySets[2][3].Value = UCase(companyNameProp) 'Summary -> Company
        
        activeDoc.PropertySets[2][1].Value = "" 'Summary -> Category
        activeDoc.PropertySets[1][4].Value = "" 'Summary -> Keywords
        activeDoc.PropertySets[1](5).Value = "" 'Summary -> Comments

              
        
        activeDoc.PropertySets[3][2].Value = fileNameSplit(0) 'Project -> Part Number
        '//activeDoc.PropertySets[3](37).Value = "" 'Project -> Stock Number
        activeDoc.PropertySets[3](14).Value = descriptionProp 'Project -> Description
        activeDoc.PropertySets[1](7).Value = "A" 'Project -> Revision Number
        '//activeDoc.PropertySets[3][3].Value = "" 'Project -> Project
        activeDoc.PropertySets[3](24).Value = ThisApplication.GeneralOptions.UserName 'Project -> Designer
        '//activeDoc.PropertySets[3](25).Value = "" 'Project -> Engineer
        '//activeDoc.PropertySets[3](26).Value = "" 'Project -> Authority
        '//activeDoc.PropertySets[3][4].Value = "" 'Project -> Cost center
        'activeDoc.PropertySets[3](21).Value = "" 'Project -> Estimated cost
        If activeDoc.PropertySets[3][1].Value = "01.01.1601" Then '01.01.1601 is a checked out flag in Inventor props
            activeDoc.PropertySets[3][1].Value = DateTime.Date 'Project -> Creation Date
        End If
        '//activeDoc.PropertySets[3](12).Value = "" 'Project -> Vendor
        '//activeDoc.PropertySets[3](12).Value = "" 'Project -> WEB Link
        
        
        
        activeDoc.PropertySets[3](23).Value = "" 'Project -> Status ????????
        activeDoc.PropertySets[3](5).Value = checkedByProp 'Status -> Checked By
        If activeDoc.PropertySets[3](6).Value = "01.01.1601" Then '01.01.1601 is a checked out flag in Inventor props
            activeDoc.PropertySets[3](6).Value = DateTime.Date 'Project -> Checked Date
        End If
        activeDoc.PropertySets[3](7).Value = "Voytulevich, Denis" 'Status -> Eng. Approved By
        'If activeDoc.PropertySets[3](8).Value = "01.01.1601" Then '01.01.1601 is a checked out flag in Inventor props
        '    activeDoc.PropertySets[3](8).Value = DateTime.Date 'Project -> Eng. Approved By
        'End If
        '//activeDoc.PropertySets[3](19).Value = "" 'Status -> Mfg. Approved By
        'If activeDoc.PropertySets[3](20).Value = "01.01.1601" Then '01.01.1601 is a checked out flag in Inventor props
        '    activeDoc.PropertySets[3](20).Value = DateTime.Date 'Project -> Mfg. Approved By
        'End If
        
        
        On Error Resume Next 'Be aware that won't work on the part document
        
        'activeDoc.PropertySets[4][1].Value = ""
        activeDoc.PropertySets[4][2].Value = "" 'Not working
        activeDoc.PropertySets[4][3].Value = Str(DateTime.Date)
        'activeDoc.PropertySets[4][4].Value = ""
        'activeDoc.PropertySets[4](5).Value = ""
        activeDoc.PropertySets[4](6).Value = ThisApplication.GeneralOptions.UserName
        'activeDoc.PropertySets[4](7).Value = ""
        'activeDoc.PropertySets[4](8).Value = ""
        activeDoc.PropertySets[4](9).Value = "-"
        'activeDoc.PropertySets[4](10).Value = ""
        'activeDoc.PropertySets[4](11).Value = ""
        activeDoc.PropertySets[4](12).Value = "-"
        'activeDoc.PropertySets[4](13).Value = ""
        'activeDoc.PropertySets[4](14).Value = ""
        activeDoc.PropertySets[4](15).Value = "A"
        'activeDoc.PropertySets[4](16).Value = ""
        'activeDoc.PropertySets[4](17).Value = ""
        activeDoc.PropertySets[4](18).Value = checkedByProp
        'activeDoc.PropertySets[4](19).Value = ""
        'activeDoc.PropertySets[4](20).Value = ""
        'activeDoc.PropertySets[4](21).Value = ""
        'activeDoc.PropertySets[4](22).Value = ""
        
        oTrans.End
    Else
        MsgBox "Не удалось заполнить IProperties, проверьте соответствие наименования файла шаблону:" & vbNewLine & "Номер - Название"
    End If
    
    
End Sub



Public Function GetFilePath()

    Dim activeDoc As Document
    Set activeDoc = ThisApplication.ActiveDocument
    
    If activeDoc Is Nothing Then
        Exit Function
    End If
    
    GetFilePath = activeDoc.FullFileName
    
End Function




Public Sub EditIPropsFile()

    Dim activeDoc As Document
    Set activeDoc = ThisApplication.ActiveDocument
    
    If activeDoc Is Nothing Then
        Exit Sub
    End If
    
    setPropertiesForm.Show
        
End Sub


 * 
 */