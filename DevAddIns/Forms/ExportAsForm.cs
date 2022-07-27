﻿using System;
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

        private Translators translator = new Translators();

        Dictionary<bool, CheckState> checkToState = new Dictionary<bool, CheckState>();
        MultilanguageDictionary MLDict = new MultilanguageDictionary();

        bool makePdf = false;
        bool makeStep = false;
        bool makeDxf = false;
        bool makeXt = false;
        bool includeParts = false;

        public ExportAsForm()
        {
            InitializeComponent();
            if(!(activeDocument.isAssemblyDocument() || activeDocument.isWeldmentDocument()))
            {
                includePartsButton.Visible = false;
            }
            xtVersionsBox.SelectedItem = xtVersionsBox.Items[11];
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
                includeParts = true;
                translator.includeParts = true;
            }

            else
            {
                includeParts = false;
                translator.includeParts = false;
            }
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            var rebuildTask = docRebuild(activeDocument);
            rebuildTask.Wait();

            if (makePdf == true) translator.createPDF();
            if (makeDxf == true) translator.createFlatDXF();
            if (makeStep == true) translator.createSTEP();
            if (makeXt == true) translator.createParasolid();
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
            doc.Rebuild();
        }

       
    }
}
