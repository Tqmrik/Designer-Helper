﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Inventor;

namespace DevAddIns
{


    public partial class EditPropertiesForm : Form
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

        private string filePath { get; set; }

        public EditPropertiesForm()
        {
            InitializeComponent();
        }
        public EditPropertiesForm(string path)
        {
            InitializeComponent();
            filePath = path;

        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                string checkedBy = checkedByField.Text;
                string companyName = companyNameField.Text;

                if (checkedBy == String.Empty && companyName == String.Empty)
                {
                    return;
                }



                string currentUserAppDataPath = filePath;
                currentUserAppDataPath = currentUserAppDataPath + "\\Autodesk\\ApplicationPlugins\\DevAddIns\\AddInData";

                ApplicationAddIn sedenumAppAddIn = InventorApplication.ApplicationAddIns.ItemById["{fb869b0a-a71f-4590-89fc-ff707daa96c3}".ToUpperInvariant()];
                string addInPath = System.IO.Path.GetDirectoryName(sedenumAppAddIn.Location);



                if (!System.IO.Directory.Exists(currentUserAppDataPath))
                {
                     System.IO.Directory.CreateDirectory(currentUserAppDataPath);
                }

                currentUserAppDataPath = addInPath + "\\EditProperties.txt";

                if (!System.IO.File.Exists(currentUserAppDataPath))
                {
                    var fileStream = System.IO.File.Create(currentUserAppDataPath);
                    fileStream.Close();
                }

                StreamWriter textFileStream = new StreamWriter(currentUserAppDataPath);
                
                textFileStream.WriteLine("Checked : " + checkedBy);
                textFileStream.WriteLine("Company : " + companyName);

                textFileStream.Close();

                Close(); //Close current window
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message + "\nAddIn: Sedenum Pack \nMethod: EditPropertiesForm");
            }
        }
    }
}