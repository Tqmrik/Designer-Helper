﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace DevAddIns
{
    public partial class IPropertiesForm : Form
    {
        private ElementHost wpfIntegrationHolder;
        private IPropertiesWPFForm wpfForm;
        public IPropertiesForm()
        {
            InitializeComponent();
        }

        private void IPropertiesForm_Load(object sender, EventArgs e)
        {
            wpfIntegrationHolder = new ElementHost();
            wpfIntegrationHolder.Dock = DockStyle.Fill;
            panelHolderForm.Controls.Add(wpfIntegrationHolder);
            wpfForm = new IPropertiesWPFForm();
            wpfForm.InitializeComponent();
            wpfIntegrationHolder.Child = wpfForm;
        }
    }
}
