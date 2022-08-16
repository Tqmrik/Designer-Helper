using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Inventor;

namespace DevAddIns
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class IPropertiesWPFForm : UserControl
    {

        

        private static Inventor.Application inventorApplication;
        public static Inventor.Application _inventorApplication
        {
            get
            {
                return inventorApplication;
            }
            set
            {
                inventorApplication = value;
            }

        }

        //IProperties of the Summary tab 
        private string title;
        private string subject;
        private string author;
        private string manager;
        private string company;
        private string category;
        private string keywords;
        private string comments;

        //IProperties of the Project tab
        private string location;
        private string fileSubtype;
        private string partNumber;
        private string stockNumber;
        private string description;
        private string revisionNumber;
        private string project;
        private string designer;
        private string engineer;
        private string authority;
        private string costCenter;
        private string estimatedCost;
        private string creationDate;
        private string vendor;
        private string webLink;

        //IProperties of the Status tab
        //partNumb
        //stockNumb
        private string status;
        private string designState;
        private string checkedBy;
        private string checkedByDate;
        private string engApprovedBy;
        private string engApprovedByDate;
        private string mfgApprovedBy;
        private string mfgApprovedByDate;


        private Document activeDocument;

        private PropertySets propSet;
        private PropertySets _propSetter
        {
            get
            {
                return propSet;
            }
            set
            {
                propSet = value;
            }
        }


        private Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        
       
        public IPropertiesWPFForm()
        {
            InitializeComponent();
            activeDocument = inventorApplication.ActiveDocument;
            _propSetter = activeDocument.PropertySets;
            try
            {
                PopulateDictionary();
                AssignVariables();
                FillTextBoxes();
            }
            catch (Exception e)
            {
                MessageBox.Show($"{e.Message}\n{e.StackTrace}\n{e.Source}");
            }

        }


        //Wrap in try catch blocks??

        private void PopulateDictionary()
        {
            for (int i = 1; i < activeDocument.PropertySets.Count+1; i++)
            {
                for(int j = 1; j < activeDocument.PropertySets[i].Count+1; j++)
                {
                    keyValuePairs.Add(activeDocument.PropertySets[i][j].DisplayName, activeDocument.PropertySets[i][j].Expression);
                }
            }

            //if (String.IsNullOrEmpty(activeDocument.PropertySets[1][1].Expression))
            //{
            //    activeDocument.PropertySets[1][1].Value = fileDecriptionProperty; 
            //}

            //if (String.IsNullOrEmpty(activeDocument.PropertySets[1][2].Expression))
            //{
            //    activeDocument.PropertySets[1][2].Value = ""; //Summary -> Subject
            //}

            //if (String.IsNullOrEmpty(activeDocument.PropertySets[1][3].Expression))
            //{
            //    activeDocument.PropertySets[1][3].Value = InventorApplication.GeneralOptions.UserName; //Summary -> Author
            //}

            //if (String.IsNullOrEmpty(activeDocument.PropertySets[2][2].Expression))
            //{
            //    activeDocument.PropertySets[2][2].Value = "";//Summary->Manager
            //}

            //if (String.IsNullOrEmpty(activeDocument.PropertySets[2][3].Expression))
            //{
            //    activeDocument.PropertySets[2][3].Value = companyNameProperty;//Summary -> Company
            //}

            //if (String.IsNullOrEmpty(activeDocument.PropertySets[2][1].Expression))
            //{
            //    activeDocument.PropertySets[2][1].Value = "";//Summary -> Category
            //}

            //if (String.IsNullOrEmpty(activeDocument.PropertySets[1][4].Expression))
            //{
            //    activeDocument.PropertySets[1][4].Value = ""; //Summary -> Keywords
            //}

            //if (String.IsNullOrEmpty(activeDocument.PropertySets[1][5].Expression))
            //{
            //    activeDocument.PropertySets[1][5].Value = ""; //Summary -> Comments
            //}

            ////--------------------

            //activeDocument.PropertySets[3][2].Value = filePartNumberProperty; //Project -> Part Number
            //                                                                  //activeDocument.PropertySets[3][37].Value = ""; //Project->Stock Number
            //activeDocument.PropertySets[3][14].Value = fileDecriptionProperty; //Project -> Description
            //if (String.IsNullOrEmpty(activeDocument.PropertySets[1][7].Expression))
            //{
            //    activeDocument.PropertySets[1][7].Value = "A"; //Project -> Revision Number
            //}
            ////activeDocument.PropertySets[3][3].Value = ""; //Project->Project
            //if (String.IsNullOrEmpty(activeDocument.PropertySets[3][24].Expression))
            //{
            //    activeDocument.PropertySets[3][24].Value = InventorApplication.GeneralOptions.UserName; //'Project -> Designer
            //}

            ////activeDocument.PropertySets[3][25].Value = ""; Project->Engineer
            ////activeDocument.PropertySets[3][26].Value = ""; Project->Authority
            ////activeDocument.PropertySets[3][4].Value = ""; Project->Cost center
            ////activeDocument.PropertySets[3][21].Value = ""; Project->Estimated cost

            //if (String.IsNullOrEmpty(activeDocument.PropertySets[3][1].Expression))
            //{
            //    //01.01.1601 is a checked out flag in Inventor props
            //    activeDocument.PropertySets[3][1].Value = dateTime.ToString("d"); //Project -> Creation Date
            //}

            ////activeDocument.PropertySets[3][12].Value = ""; //Project->Vendor
            ////activeDocument.PropertySets[3][12].Value = ""; //Project->WEB Link

            ////--------------------
            //if (String.IsNullOrEmpty(activeDocument.PropertySets[3][23].Expression))
            //{
            //    activeDocument.PropertySets[3][23].Value = ""; //Project -> Status ????????
            //}

            ////if(String.IsNullOrEmpty(Inventor.PropertiesForDesignTrackingPropertiesEnum.kCheckedByDesignTrackingProperties)) //??????

            //if (String.IsNullOrEmpty(activeDocument.PropertySets[3][5].Expression))
            //{
            //    activeDocument.PropertySets[3][5].Value = checkedByProperty; //Status -> Checked By
            //}

            //if (String.IsNullOrEmpty(activeDocument.PropertySets[3][6].Expression))
            //{
            //    activeDocument.PropertySets[3][6].Value = dateTime.ToString("d"); //Project -> Checked Date
            //}

            //if (String.IsNullOrEmpty(activeDocument.PropertySets[3][7].Expression))
            //{
            //    activeDocument.PropertySets[3][7].Value = "Voytulevich, Denis"; //Status -> Eng. Approved By
            //}

            ////if(activeDocument.PropertySets[3][8].Value.ToString() == emptyDate)
            ////{
            ////    activeDocument.PropertySets[3][8].Value = dateTime.Date.ToString(); //Project->Eng.Approved By
            ////}

            ////activeDocument.PropertySets[3][19].Value = ""; //Status->Mfg.Approved By

            ////if (activeDocument.PropertySets[3][20].Value.ToString() == emptyDate)
            ////{
            ////        activeDocument.PropertySets[3][20].Value = dateTime.Date.ToString(); //Project->Mfg.Approved By
            ////}
        }

        private void AssignVariables()
        {
            title = keyValuePairs["Title"] ?? "";
            subject = keyValuePairs["Subject"] ?? "";
            author = keyValuePairs["Author"] ?? "";
            manager = keyValuePairs["Manager"] ?? "";
            company = keyValuePairs["Company"] ?? "";
            category = keyValuePairs["Category"] ?? "";
            keywords = keyValuePairs["Keywords"] ?? "";
            comments = keyValuePairs["Comments"] ?? "";

            location = activeDocument.FullFileName ?? "";
            fileSubtype = DocumentChecker.InventorDocumentType(keyValuePairs["Part Type"]) ?? "";
            partNumber = keyValuePairs["Part Number"] ?? "";
            stockNumber = keyValuePairs["Stock Number"] ?? "";
            description = keyValuePairs["Description"] ?? "";
            revisionNumber = keyValuePairs["Revision Number"] ?? "";
            project = keyValuePairs["Project"] ?? "";
            designer = keyValuePairs["Designer"] ?? "";
            engineer = keyValuePairs["Engineer"] ?? "";
            authority = keyValuePairs["Authority"] ?? "";
            costCenter = keyValuePairs["Cost Center"] ?? "";
            estimatedCost = keyValuePairs["Cost"] ?? "";
            creationDate = keyValuePairs["Date Created"];
            vendor = keyValuePairs["Vendor"] ?? "";
            webLink = keyValuePairs["Catalog web link"] ?? "";

            status = keyValuePairs["Design Status"] ?? "";
            //designState = keyValuePairs[""] ???
            checkedBy = keyValuePairs["Checked by"] ?? "";
            checkedByDate = keyValuePairs["Date Checked"];
            engApprovedBy = keyValuePairs["Engr Approved by"] ?? "";
            engApprovedByDate = keyValuePairs["Date Eng Approved"];
            mfgApprovedBy = keyValuePairs["Mfg Approved by"] ?? "";
            mfgApprovedByDate = keyValuePairs["Date Mfg Approved"];

        }

        private void FillTextBoxes()
        {
            titleBox.Text = title;
            subjectBox.Text = subject;
            authorBox.Text = author;
            managerBox.Text = manager;
            companyBox.Text = company;
            categoryBox.Text = category;
            keywordsBox.Text = keywords;
            commentsBox.Text = comments;

            locationBox.Text = location;
            fileSubtypeBox.Text = fileSubtype;
            partNumberBox.Text = partNumber;
            stockNumberBox.Text = stockNumber;
            descriptionBox.Text = description;
            revisionNumberBox.Text = revisionNumber;
            projectBox.Text = project;
            designerBox.Text = designer;
            engineerBox.Text = engineer;
            authorityBox.Text = authority;
            costCenterBox.Text = costCenter;
            costBox.Text = estimatedCost;
            creationDateBox.Text = creationDate;
            vendorBox.Text = vendor;
            webLinkBox.Text = webLink;

            statusBox.Text = status;
            designStateBox.Text = designState;
            checkedByBox.Text = checkedBy;
            checkedDateBox.Text = checkedByDate;
            engApprovedByBox.Text = engApprovedBy;
            engApprovedDateBox.Text = engApprovedByDate;
            mfgApprovedByBox.Text = mfgApprovedBy;
            mfgApprovedDateBox.Text = mfgApprovedByDate;
        }
    }
}
