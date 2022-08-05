using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private IDocument bomDocument;
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
                bomDocument = new partDocum(activeDocument, activeDocument);
                returnPartList.Add(bomDocument);
            }

            if(activeDocument.isSheetMetalDocument())
            {
                bomDocument = new sheetMetalDocum(activeDocument, activeDocument);
                returnPartList.Add(bomDocument);

            }
            if(activeDocument.isAssemblyDocument())
            {

                bomDocument = new assemblyDocum(activeDocument, activeDocument);
                returnPartList.Add(bomDocument);

                foreach(Document doc in activeDocument.AllReferencedDocuments)
                {
                    //TODO: rewrite to the case statement?????
                    if(doc.isPartDocument())
                    {
                        bomDocument = new partDocum(doc, activeDocument);
                        returnPartList.Add(bomDocument);
                    }
                    else if(doc.isSheetMetalDocument())
                    {
                        bomDocument = new sheetMetalDocum(doc, activeDocument);
                        returnPartList.Add(bomDocument);
                    }
                    else if(doc.isAssemblyDocument())
                    {
                        bomDocument = new assemblyDocum(doc, activeDocument);
                        returnPartList.Add(bomDocument);
                    }
                    else if(doc.isWeldmentDocument())
                    {
                        bomDocument = new weldmentDocum();
                        returnPartList.Add(bomDocument);
                    }
                }

            }

            foreach(IDocument doc in returnPartList)
            {
                if(doc.SubType == "{9C464203-9BAE-11D3-8BAD-0060B0CE6BB4}")
                {
                    metalThicknessDataField.Visibility = Visibility.Visible;
                    break;
                }
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

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");//Allow only integers
            e.Handled = regex.IsMatch(e.Text);
        }

        private void radioSheetsInRangePDF_Checked(object sender, RoutedEventArgs e)
        {
            SheetRangeFromPDF.IsEnabled = true;
            SheetRangeToPDF.IsEnabled = true;
        }

        private void radioSheetsInRangePDF_Unchecked(object sender, RoutedEventArgs e)
        {
            SheetRangeFromPDF.IsEnabled = false;
            SheetRangeToPDF.IsEnabled = false;
        }

        private void comboBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");//Allow only integers
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

//NOTE: Data bindings: https://docs.microsoft.com/en-us/dotnet/api/system.windows.controls.datagrid?view=windowsdesktop-6.0-->
//NOTE: Star on dimensions: https://stackoverflow.com/questions/1768293/what-does-the-wpf-star-do-width-100
//NOTE: Binding in WPF: https://docs.microsoft.com/en-us/dotnet/desktop/wpf/data/?view=netdesktop-6.0
//NOTE: Bind modes: https://docs.microsoft.com/en-us/dotnet/api/system.windows.data.bindingmode?view=windowsdesktop-6.0#system-windows-data-bindingmode-twoway
//NOTE: Width binding: https://stackoverflow.com/questions/36458766/custom-expression-to-define-width-and-height-in-wpf
//NOTE: Integers only in a textBox: https://abundantcode.com/how-to-allow-only-numeric-input-in-a-textbox-in-wpf/

//TODO: Add item number???
//TODO: Display drawings in the list???
//TODO: Add Icons and stuff
//TODO: DXF settings 
