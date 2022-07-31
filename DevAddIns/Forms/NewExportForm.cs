using System;
using System.Windows;
using System.Windows.Forms.Integration;
using System.Windows.Media;
using System.Windows.Forms;

namespace DevAddIns
{
    
    public partial class NewExportForm : Form
    {
        private static Inventor.Application m_inventorApplication;
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

        private ElementHost ctrlHost;
        private UserControl55 wpfAddressCtrl;

        public NewExportForm()
        {
            InitializeComponent();
        }

        private void NewExportForm_Load(object sender, EventArgs e)
        {
            

            ctrlHost = new ElementHost();
            ctrlHost.Dock = DockStyle.Fill;
            panel1.Controls.Add(ctrlHost);
            wpfAddressCtrl = new UserControl55();
            wpfAddressCtrl.InitializeComponent();
            ctrlHost.Child = wpfAddressCtrl;
        }

    }
}
