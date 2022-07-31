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
    public partial class FileExportControl : UserControl
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


        public FileExportControl()
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

            //TODO: Traverse the collection to see if there is sheetMetal part and add new datagrid column if so
            //TODO: Add a search bar 
            //TODO: add export checkboxes
            //TODO: add export all parts checkbox
            //TODO: Add error list to see which parts didn't converted correctly
            //TODO: calm down a bit writting todos, I feel sick already :S

            return returnPartList;
        }

        private void Ribbon_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

//NOTE: Data bindings: https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.datagrid?view=windowsdesktop-6.0-->
//NOTE: Star on dimensions: https://stackoverflow.com/questions/1768293/what-does-the-wpf-star-do-width-100
//NOTE: Binding in WPF: https://docs.microsoft.com/en-us/dotnet/desktop/wpf/data/?view=netdesktop-6.0
//NOTE: Bind modes: https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.bindingmode?view=windowsdesktop-6.0#system-windows-data-bindingmode-twoway
//NOTE: Width binding: https://stackoverflow.com/questions/36458766/custom-expression-to-define-width-and-height-in-wpf


//TODO: Add item number???
//TODO: Display drawings in the list???
//TODO: Add Icons and stuff
//TODO: DXF settings 
