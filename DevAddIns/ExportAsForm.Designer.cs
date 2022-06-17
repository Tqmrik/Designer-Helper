
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
            this.rememberTheChoiceButton = new System.Windows.Forms.CheckBox();
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
            this.stepCheckBox.Size = new System.Drawing.Size(244, 24);
            this.stepCheckBox.TabIndex = 2;
            this.stepCheckBox.Text = ".step (Referenced documents)";
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
            this.dxfCheckBox.Size = new System.Drawing.Size(182, 24);
            this.dxfCheckBox.TabIndex = 3;
            this.dxfCheckBox.Text = ".dxf (Flat Pattern only)";
            this.dxfCheckBox.UseVisualStyleBackColor = true;
            this.dxfCheckBox.CheckedChanged += new System.EventHandler(this.dxfCheckBox_CheckedChanged);
            // 
            // exportButton
            // 
            this.exportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.exportButton.Location = new System.Drawing.Point(12, 144);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(260, 35);
            this.exportButton.TabIndex = 0;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // rememberTheChoiceButton
            // 
            this.rememberTheChoiceButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rememberTheChoiceButton.AutoSize = true;
            this.rememberTheChoiceButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rememberTheChoiceButton.Location = new System.Drawing.Point(65, 118);
            this.rememberTheChoiceButton.Name = "rememberTheChoiceButton";
            this.rememberTheChoiceButton.Size = new System.Drawing.Size(159, 20);
            this.rememberTheChoiceButton.TabIndex = 4;
            this.rememberTheChoiceButton.Text = "Remember the choice";
            this.rememberTheChoiceButton.UseVisualStyleBackColor = true;
            this.rememberTheChoiceButton.Visible = false;
            this.rememberTheChoiceButton.CheckedChanged += new System.EventHandler(this.rememberTheChoiceButton_CheckedChanged);
            // 
            // ExportAsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 191);
            this.Controls.Add(this.rememberTheChoiceButton);
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
        private System.Windows.Forms.CheckBox rememberTheChoiceButton;
    }
}