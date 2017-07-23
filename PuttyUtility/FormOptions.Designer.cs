namespace PuttyUtility
{
    partial class FormOptions
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
            this.labelPuttyExe = new System.Windows.Forms.Label();
            this.textBoxPuttyExe = new System.Windows.Forms.TextBox();
            this.buttonPuttyExeBrowse = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelPuttyExe
            // 
            this.labelPuttyExe.AutoSize = true;
            this.labelPuttyExe.Location = new System.Drawing.Point(12, 17);
            this.labelPuttyExe.Name = "labelPuttyExe";
            this.labelPuttyExe.Size = new System.Drawing.Size(52, 13);
            this.labelPuttyExe.TabIndex = 0;
            this.labelPuttyExe.Text = "Putty Exe";
            // 
            // textBoxPuttyExe
            // 
            this.textBoxPuttyExe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPuttyExe.Location = new System.Drawing.Point(70, 14);
            this.textBoxPuttyExe.Name = "textBoxPuttyExe";
            this.textBoxPuttyExe.Size = new System.Drawing.Size(173, 20);
            this.textBoxPuttyExe.TabIndex = 1;
            // 
            // buttonPuttyExeBrowse
            // 
            this.buttonPuttyExeBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPuttyExeBrowse.Location = new System.Drawing.Point(249, 12);
            this.buttonPuttyExeBrowse.Name = "buttonPuttyExeBrowse";
            this.buttonPuttyExeBrowse.Size = new System.Drawing.Size(23, 23);
            this.buttonPuttyExeBrowse.TabIndex = 2;
            this.buttonPuttyExeBrowse.Text = "...";
            this.buttonPuttyExeBrowse.UseVisualStyleBackColor = true;
            this.buttonPuttyExeBrowse.Click += new System.EventHandler(this.buttonPuttyExeBrowse_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(197, 226);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(116, 226);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormOptions
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(280, 257);
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonPuttyExeBrowse);
            this.Controls.Add(this.textBoxPuttyExe);
            this.Controls.Add(this.labelPuttyExe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOptions";
            this.ShowIcon = false;
            this.Text = "Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPuttyExe;
        private System.Windows.Forms.TextBox textBoxPuttyExe;
        private System.Windows.Forms.Button buttonPuttyExeBrowse;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}