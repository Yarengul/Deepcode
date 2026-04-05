namespace DeepCodeAnalytics.UI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.RichTextBox txtSourceCode;
        private System.Windows.Forms.Button btnAnalyze;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.Label lblStatus;

        /// <summary>
        /// Form bileşenleri ve bellek yönetimi (Disposing).
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Kullanıcının kod yazıp analiz tuşuna basması için oluşturulmuş basit arayüzün elemanlarını deklare eder.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSourceCode = new System.Windows.Forms.RichTextBox();
            this.btnAnalyze = new System.Windows.Forms.Button();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSourceCode
            // 
            this.txtSourceCode.Font = new System.Drawing.Font("Consolas", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSourceCode.Location = new System.Drawing.Point(12, 12);
            this.txtSourceCode.Name = "txtSourceCode";
            this.txtSourceCode.Size = new System.Drawing.Size(760, 250);
            this.txtSourceCode.TabIndex = 0;
            // İçerisinde bilerek hata üretilmiş Scrum Master Demo kodu
            this.txtSourceCode.Text = "using System;\n\nclass TestClass \n{\n    void ExampleMethod() \n    {\n        int delay = 1000; // SM003: Magic number buraya vuracak! \n        try \n        {\n             Console.WriteLine(delay);\n        }\n        catch(Exception ex) \n        {\n             // SM002: Yutulmuş gizli exception !\n        }\n    }\n}";
            // 
            // btnAnalyze
            // 
            this.btnAnalyze.Location = new System.Drawing.Point(12, 275);
            this.btnAnalyze.Name = "btnAnalyze";
            this.btnAnalyze.Size = new System.Drawing.Size(120, 35);
            this.btnAnalyze.TabIndex = 1;
            this.btnAnalyze.Text = "Analiz Et";
            this.btnAnalyze.UseVisualStyleBackColor = true;
            this.btnAnalyze.Click += new System.EventHandler(this.btnAnalyze_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(150, 282);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(262, 20);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Analiz işlemi için hazır. Kodu girin.";
            // 
            // dgvResults
            // 
            this.dgvResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(12, 325);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.RowTemplate.Height = 29;
            this.dgvResults.Size = new System.Drawing.Size(760, 215);
            this.dgvResults.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnAnalyze);
            this.Controls.Add(this.txtSourceCode);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DeepCode Analytics - Roslyn Analyzer";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
