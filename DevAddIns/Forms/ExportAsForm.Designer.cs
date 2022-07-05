
namespace DevAddIns
{
    partial class ExportAsForm
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
            this.pdfCheckBox = new System.Windows.Forms.CheckBox();
            this.stepCheckBox = new System.Windows.Forms.CheckBox();
            this.dxfCheckBox = new System.Windows.Forms.CheckBox();
            this.exportButton = new System.Windows.Forms.Button();
            this.includePartsButton = new System.Windows.Forms.CheckBox();
            this.checkAllBox = new System.Windows.Forms.CheckBox();
            this.xtCheckBox = new System.Windows.Forms.CheckBox();
            this.xtVersionsBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // pdfCheckBox
            // 
            this.pdfCheckBox.AutoSize = true;
            this.pdfCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pdfCheckBox.Location = new System.Drawing.Point(12, 12);
            this.pdfCheckBox.Name = "pdfCheckBox";
            this.pdfCheckBox.Size = new System.Drawing.Size(127, 24);
            this.pdfCheckBox.TabIndex = 1;
            this.pdfCheckBox.Text = ".pdf (Drawing)";
            this.pdfCheckBox.UseVisualStyleBackColor = true;
            this.pdfCheckBox.CheckedChanged += new System.EventHandler(this.pdfCheckBox_CheckedChanged);
            // 
            // stepCheckBox
            // 
            this.stepCheckBox.AutoSize = true;
            this.stepCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.stepCheckBox.Location = new System.Drawing.Point(12, 42);
            this.stepCheckBox.Name = "stepCheckBox";
            this.stepCheckBox.Size = new System.Drawing.Size(63, 24);
            this.stepCheckBox.TabIndex = 2;
            this.stepCheckBox.Text = ".step";
            this.stepCheckBox.UseVisualStyleBackColor = true;
            this.stepCheckBox.CheckedChanged += new System.EventHandler(this.stepCheckBox_CheckedChanged);
            // 
            // dxfCheckBox
            // 
            this.dxfCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dxfCheckBox.AutoSize = true;
            this.dxfCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dxfCheckBox.Location = new System.Drawing.Point(12, 72);
            this.dxfCheckBox.Name = "dxfCheckBox";
            this.dxfCheckBox.Size = new System.Drawing.Size(150, 24);
            this.dxfCheckBox.TabIndex = 3;
            this.dxfCheckBox.Text = ".dxf (Flat Pattern)";
            this.dxfCheckBox.UseVisualStyleBackColor = true;
            this.dxfCheckBox.CheckedChanged += new System.EventHandler(this.dxfCheckBox_CheckedChanged);
            // 
            // exportButton
            // 
            this.exportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exportButton.Location = new System.Drawing.Point(12, 174);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(198, 35);
            this.exportButton.TabIndex = 0;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // includePartsButton
            // 
            this.includePartsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.includePartsButton.AutoSize = true;
            this.includePartsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.includePartsButton.Location = new System.Drawing.Point(122, 148);
            this.includePartsButton.Name = "includePartsButton";
            this.includePartsButton.Size = new System.Drawing.Size(88, 17);
            this.includePartsButton.TabIndex = 4;
            this.includePartsButton.Text = "Include Parts";
            this.includePartsButton.UseVisualStyleBackColor = true;
            this.includePartsButton.CheckedChanged += new System.EventHandler(this.includePartsButton_CheckedChanged);
            // 
            // checkAllBox
            // 
            this.checkAllBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.checkAllBox.AutoSize = true;
            this.checkAllBox.Location = new System.Drawing.Point(12, 148);
            this.checkAllBox.Name = "checkAllBox";
            this.checkAllBox.Size = new System.Drawing.Size(74, 17);
            this.checkAllBox.TabIndex = 5;
            this.checkAllBox.Text = "All formats";
            this.checkAllBox.UseVisualStyleBackColor = true;
            this.checkAllBox.CheckedChanged += new System.EventHandler(this.checkAllBox_CheckedChanged);
            // 
            // xtCheckBox
            // 
            this.xtCheckBox.AutoSize = true;
            this.xtCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xtCheckBox.Location = new System.Drawing.Point(12, 102);
            this.xtCheckBox.Name = "xtCheckBox";
            this.xtCheckBox.Size = new System.Drawing.Size(53, 24);
            this.xtCheckBox.TabIndex = 6;
            this.xtCheckBox.Text = ".x_t";
            this.xtCheckBox.UseVisualStyleBackColor = true;
            this.xtCheckBox.Visible = false;
            this.xtCheckBox.CheckedChanged += new System.EventHandler(this.xtCheckBox_CheckedChanged);
            // 
            // xtVersionsBox
            // 
            this.xtVersionsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.xtVersionsBox.FormattingEnabled = true;
            this.xtVersionsBox.Items.AddRange(new object[] {
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31"});
            this.xtVersionsBox.Location = new System.Drawing.Point(71, 100);
            this.xtVersionsBox.Name = "xtVersionsBox";
            this.xtVersionsBox.Size = new System.Drawing.Size(77, 28);
            this.xtVersionsBox.TabIndex = 7;
            this.xtVersionsBox.Text = "Select the version";
            this.xtVersionsBox.Visible = false;
            // 
            // ExportAsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(222, 223);
            this.Controls.Add(this.xtVersionsBox);
            this.Controls.Add(this.xtCheckBox);
            this.Controls.Add(this.checkAllBox);
            this.Controls.Add(this.includePartsButton);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.dxfCheckBox);
            this.Controls.Add(this.stepCheckBox);
            this.Controls.Add(this.pdfCheckBox);
            this.Name = "ExportAsForm";
            this.Text = "Export To";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox pdfCheckBox;
        private System.Windows.Forms.CheckBox stepCheckBox;
        private System.Windows.Forms.CheckBox dxfCheckBox;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.CheckBox includePartsButton;
        private System.Windows.Forms.CheckBox checkAllBox;
        private System.Windows.Forms.CheckBox xtCheckBox;
        private System.Windows.Forms.ComboBox xtVersionsBox;
    }
}