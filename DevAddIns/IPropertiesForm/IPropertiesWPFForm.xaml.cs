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
        private Document activeDocument;
        private Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        private PropertySets propSet;

        public static Inventor.Application invApp
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
        private PropertySets propSetter
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
        


        public IPropertiesWPFForm()
        {
            InitializeComponent();
            activeDocument = inventorApplication.ActiveDocument;
            propSetter = activeDocument.PropertySets;
            populateDictionary();
        }




        private void populateDictionary()
        {
            for (int i = 1; i < activeDocument.PropertySets.Count+1; i++)
            {
                for(int j = 1; j < activeDocument.PropertySets[i].Count+1; j++)
                {
                    keyValuePairs.Add(activeDocument.PropertySets[i][j].DisplayName, activeDocument.PropertySets[i][j].Expression);
                }
            }

            var s = 2;


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
    }
}
