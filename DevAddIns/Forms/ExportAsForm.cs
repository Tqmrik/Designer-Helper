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
        const string directoryPlaceholder = "Directory name...";
        string directPath;

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

        private PDF_Translator pdfTranslator = new PDF_Translator();
        private STEP_Translator stepTranslator = new STEP_Translator();
        private DXF_Translator dxfTranslator = new DXF_Translator();
        private Parasolid_Translator parasolidTranslator = new Parasolid_Translator();

        Dictionary<bool, CheckState> checkToState = new Dictionary<bool, CheckState>();
        MultilanguageDictionary MLDict = new MultilanguageDictionary();

        bool makePdf = false;
        bool makeStep = false;
        bool makeDxf = false;
        bool makeXt = false;
        bool singleDir = false;

        //bool includeParts = false;
        //bool packAssembly = false;

        public ExportAsForm()
        {
            InitializeComponent();
            ApplyPlaceHolderStyleTextBox(directoryNameTextBox, directoryPlaceholder);            
            if (!(activeDocument.IsAssemblyDocument() || activeDocument.IsWeldmentDocument()))
            {
                includePartsButton.Visible = false;
            }
            xtVersionsBox.SelectedItem = xtVersionsBox.Items[11];
        }


        private void singleDirectoryCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (singleDirectoryCheckBox.Checked)
            {
                singleDir = true;
                directoryNameTextBox.ReadOnly = false;
            }
            else
            {
                singleDir = false;
                directoryNameTextBox.ReadOnly = true;
            }
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
        private void xtCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (xtCheckBox.Checked) makeXt = true;
            else makeXt = false;
        }


        private void includePartsButton_CheckedChanged(object sender, EventArgs e)
        {
            if (includePartsButton.Checked)
            {
                //includeParts = true;
                Translator_Object.includeParts = true;
                packAssemblyButton.Enabled = false;
            }

            else
            {
                //includeParts = false;
                Translator_Object.includeParts = false;
                packAssemblyButton.Enabled = true;
            }
        }


        private void packAssemblyButton_CheckedChanged(object sender, EventArgs e)
        {
            if (packAssemblyButton.Checked)
            {
                //packAssembly = true;
                Translator_Object.packAssembly = true;
                includePartsButton.Enabled = false;
            }

            else
            {
                //packAssembly = false;
                Translator_Object.packAssembly = false;
                includePartsButton.Enabled = true;
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            //TODO: Implement missing files feature
            if(singleDir)
            {
                directPath = System.IO.Path.GetDirectoryName(activeDocument.FullFileName);

                if((String.IsNullOrEmpty(directoryNameTextBox.Text) || directoryNameTextBox.Text == directoryPlaceholder) && singleDir)
                {
                    string directoryName = "\\Export" + DateTime.Today.ToString("yyyy-MM-dd") + "_" + DateTime.Now.TimeOfDay.ToString("%h%m%s");
                    Directory.CreateDirectory(directPath + directoryName);
                    directPath += directoryName;
                }
                else if(singleDir && !(directoryNameTextBox.Text == directoryPlaceholder))
                {
                    Directory.CreateDirectory(directPath + "\\" + directoryNameTextBox.Text);
                    directPath += "\\" + directoryNameTextBox.Text;
                }

                pdfTranslator = new PDF_Translator(directPath);
                stepTranslator = new STEP_Translator(directPath);
                dxfTranslator = new DXF_Translator(directPath);
                parasolidTranslator = new Parasolid_Translator();
            }

            var rebuildTask = docRebuild(activeDocument);
            rebuildTask.Wait();
            if (makePdf == true) pdfTranslator.CreatePDF(activeDocument);
            if (makeDxf == true) dxfTranslator.createFlatDXF(activeDocument);
            if (makeStep == true) stepTranslator.CreateSTEP(activeDocument);
            if (makeXt == true) parasolidTranslator.createParasolid();

            System.Diagnostics.Process.Start("explorer.exe", $"\"{directPath}\"");

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
        
        private static async Task docRebuild(Document doc)
        {
            //doc.Rebuild();
        }

        private void directoryNameTextBox_Enter(object sender, EventArgs e)
        {
            if(directoryNameTextBox.Text == directoryPlaceholder)
            {
                ApplyRegularStyleTextBox(directoryNameTextBox);
            }
        }

        private void directoryNameTextBox_Leave(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(directoryNameTextBox.Text))
            {
                ApplyPlaceHolderStyleTextBox(directoryNameTextBox, directoryPlaceholder);
            }
        }


        private void ApplyRegularStyleTextBox(System.Windows.Forms.TextBox textBox)
        {
            directoryNameTextBox.Text = "";
            directoryNameTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            directoryNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }

        private void ApplyPlaceHolderStyleTextBox(System.Windows.Forms.TextBox textBox, string placeholderString)
        {
            textBox.Text = placeholderString;
            textBox.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        }
    }
}
