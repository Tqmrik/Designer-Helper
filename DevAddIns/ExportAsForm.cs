using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevAddIns
{
    public partial class ExportAsForm : Form
    {

        bool makePdf = false;
        bool makeStep = false;
        bool makeDxf = false;
        bool rememberChoice = false;

        public ExportAsForm()
        {
            InitializeComponent();
            rememberTheChoiceButton.Checked = false;
            //TODO: Add check from file;
            //TODO: Add fast export so that the last remembered state is exported
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
            if (rememberTheChoiceButton.Checked) rememberChoice = true;
            else rememberChoice = false;
        }

        private void exportButton_Click(object sender, EventArgs e)
        {

        }

        
    }
}
