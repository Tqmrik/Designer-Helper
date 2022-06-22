using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventor;



namespace DevAddIns
{

    public partial class ExportAsForm : Form
    {
        private static Inventor.Application m_inventorApplication;
        public static Inventor.Application InventorApplication
        {
            set
            {
                m_inventorApplication = value;
            }
            get
            {
                return m_inventorApplication;
            }
        }
        private Document activeDocument
        {
            get
            {
                return InventorApplication.ActiveDocument;
            }
        }
        private string fPath
        {
            get
            {
                return activeDocument.FullDocumentName;
            }
        }
        private string revision
        {
            get
            {
                return activeDocument.PropertySets[1][7].Value.ToString();
            }

        }

        private Translators translator = new Translators();

        private bool pdfButtonState { get; set; }
        private bool stepButtonState { get; set; }
        private bool dxfButtonState { get; set; }

        Dictionary<bool, CheckState> checkToState = new Dictionary<bool, CheckState>();
        MultilanguageDictionary MLDict = new MultilanguageDictionary();

        bool makePdf = false;
        bool makeStep = false;
        bool makeDxf = false;
        bool rememberChoice = false;

        public ExportAsForm()
        {
            InitializeComponent();
        }



        private void pdfCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(pdfCheckBox.Checked) makePdf = true;
            else makePdf = false;
        }

        private void stepCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (stepCheckBox.Checked) makeStep = true;
            else makeStep = false;
        }

        private void dxfCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (dxfCheckBox.Checked) makeDxf = true;
            else makeDxf = false;
        }

        private void rememberTheChoiceButton_CheckedChanged(object sender, EventArgs e)
        {
            if (rememberTheChoiceButton.Checked) 
            {
                rememberChoice = true;
            }
            
            else rememberChoice = false;
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            if (makePdf == true) translator.createPDF();
            if (makeDxf == true) translator.createFlatDXF();
            if (makeStep == true) translator.createSTEP();
            Close();
        }

        private void checkAllBox_CheckedChanged(object sender, EventArgs e)
        {
            if(checkAllBox.Checked)
            {
                pdfCheckBox.Checked = true;
                makePdf = true;
                dxfCheckBox.Checked = true;
                makeDxf = true;
                stepCheckBox.Checked = true;
                makeStep = true;
            }
            else
            {
                pdfCheckBox.Checked = false;
                makePdf = false;
                dxfCheckBox.Checked = false;
                makeDxf = false;
                stepCheckBox.Checked = false;
                makeStep = false;
            }
        }      
    }
}
