
namespace DevAddIns
{
    partial class EditPropertiesForm
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
            this.updateButton = new System.Windows.Forms.Button();
            this.checkedByLabel = new System.Windows.Forms.Label();
            this.companyLabel = new System.Windows.Forms.Label();
            this.checkedByField = new System.Windows.Forms.TextBox();
            this.companyNameField = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // updateButton
            // 
            this.updateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.updateButton.Location = new System.Drawing.Point(14, 102);
            this.updateButton.Margin = new System.Windows.Forms.Padding(5, 10, 5, 10);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(356, 60);
            this.updateButton.TabIndex = 2;
            this.updateButton.Text = "Update Properties";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // checkedByLabel
            // 
            this.checkedByLabel.AutoSize = true;
            this.checkedByLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedByLabel.Location = new System.Drawing.Point(19, 22);
            this.checkedByLabel.Margin = new System.Windows.Forms.Padding(10, 10, 3, 0);
            this.checkedByLabel.Name = "checkedByLabel";
            this.checkedByLabel.Size = new System.Drawing.Size(96, 20);
            this.checkedByLabel.TabIndex = 3;
            this.checkedByLabel.Text = "Checked by:";
            // 
            // companyLabel
            // 
            this.companyLabel.AutoSize = true;
            this.companyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.companyLabel.Location = new System.Drawing.Point(19, 66);
            this.companyLabel.Margin = new System.Windows.Forms.Padding(10, 0, 3, 0);
            this.companyLabel.Name = "companyLabel";
            this.companyLabel.Size = new System.Drawing.Size(80, 20);
            this.companyLabel.TabIndex = 3;
            this.companyLabel.Text = "Company:";
            // 
            // checkedByField
            // 
            this.checkedByField.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.checkedByField.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedByField.Location = new System.Drawing.Point(148, 19);
            this.checkedByField.Margin = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.checkedByField.Name = "checkedByField";
            this.checkedByField.Size = new System.Drawing.Size(217, 26);
            this.checkedByField.TabIndex = 0;
            // 
            // companyNameField
            // 
            this.companyNameField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.companyNameField.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.companyNameField.Location = new System.Drawing.Point(148, 63);
            this.companyNameField.Margin = new System.Windows.Forms.Padding(10, 15, 10, 3);
            this.companyNameField.Name = "companyNameField";
            this.companyNameField.Size = new System.Drawing.Size(217, 26);
            this.companyNameField.TabIndex = 1;
            // 
            // EditPropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 181);
            this.Controls.Add(this.companyNameField);
            this.Controls.Add(this.checkedByField);
            this.Controls.Add(this.companyLabel);
            this.Controls.Add(this.checkedByLabel);
            this.Controls.Add(this.updateButton);
            this.Name = "EditPropertiesForm";
            this.Text = "EditIProperties";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Label checkedByLabel;
        private System.Windows.Forms.Label companyLabel;
        private System.Windows.Forms.TextBox checkedByField;
        private System.Windows.Forms.TextBox companyNameField;
    }
}