namespace DeepCodeAnalytics.UI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnAnalizEt;
        private System.Windows.Forms.RichTextBox rtbKodGiris;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnAnalizEt = new System.Windows.Forms.Button();
            this.rtbKodGiris = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnAnalizEt
            // 
            this.btnAnalizEt.Location = new System.Drawing.Point(12, 12);
            this.btnAnalizEt.Name = "btnAnalizEt";
            this.btnAnalizEt.Size = new System.Drawing.Size(75, 23);
            this.btnAnalizEt.TabIndex = 0;
            this.btnAnalizEt.Text = "Analiz Et";
            this.btnAnalizEt.UseVisualStyleBackColor = true;
            this.btnAnalizEt.Click += new System.EventHandler(this.btnAnalizEt_Click);
            // 
            // rtbKodGiris
            // 
            this.rtbKodGiris.Location = new System.Drawing.Point(12, 41);
            this.rtbKodGiris.Name = "rtbKodGiris";
            this.rtbKodGiris.Size = new System.Drawing.Size(776, 397);
            this.rtbKodGiris.TabIndex = 1;
            this.rtbKodGiris.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rtbKodGiris);
            this.Controls.Add(this.btnAnalizEt);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
