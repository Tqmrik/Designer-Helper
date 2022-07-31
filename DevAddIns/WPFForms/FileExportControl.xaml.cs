using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UserControl55.xaml
    /// </summary>
    public partial class UserControl55 : UserControl
    {

        private static Inventor.Application m_inventorApplication;
        private Document activeDocument;
        private IDocument partDocument;
        public static Inventor.Application InventorApplication 
        {
            get
            {
                return m_inventorApplication;
            }
            set
            {
                m_inventorApplication = value;
            }
        }


        public UserControl55()
        {
            InitializeComponent();

            ObservableCollection<IDocument> partList = GetPartList();
            dataGrid.DataContext = partList;
        }

        private ObservableCollection<IDocument> GetPartList()
        {
            ObservableCollection<IDocument> returnPartList = new ObservableCollection<IDocument>();
            activeDocument = m_inventorApplication.ActiveDocument;

            if(activeDocument.isPartDocument())
            {
                partDocument = new partDocum(activeDocument, activeDocument);
                returnPartList.Add(partDocument);
            }

            return returnPartList;
        }

        private void Ribbon_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

//NOTE: Data bindings: https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.datagrid?view=windowsdesktop-6.0-->
