﻿
namespace DevAddIns
{
    partial class IPropertiesForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelHolderForm = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.panelHolderForm);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(484, 611);
            this.panel1.TabIndex = 0;
            // 
            // panelHolderForm
            // 
            this.panelHolderForm.Location = new System.Drawing.Point(12, 12);
            this.panelHolderForm.Margin = new System.Windows.Forms.Padding(3, 3, 3, 30);
            this.panelHolderForm.Name = "panelHolderForm";
            this.panelHolderForm.Size = new System.Drawing.Size(460, 560);
            this.panelHolderForm.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(147, 580);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 21);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(279, 581);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 19);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(372, 580);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 21);
            this.button3.TabIndex = 3;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // IPropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 611);
            this.Controls.Add(this.panel1);
            this.HelpButton = true;
            this.MaximumSize = new System.Drawing.Size(500, 650);
            this.MinimumSize = new System.Drawing.Size(500, 650);
            this.Name = "IPropertiesForm";
            this.Text = "IPropertiesForm";
            this.Load += new System.EventHandler(this.IPropertiesForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelHolderForm;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}