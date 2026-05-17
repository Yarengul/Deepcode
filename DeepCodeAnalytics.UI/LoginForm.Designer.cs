namespace DeepCodeAnalytics.UI
{
   partial class LoginForm
    {
        /// <summary>
        /// Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Kullanılan tüm kaynakları temizler.
        /// </summary>
        /// <param name="disposing">Yönetilen kaynaklar elden çıkarılacaksa true; aksi halde false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        } 

        #region Windows Form Tasarımcısı üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot.
        /// Bu metodun içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlLoginContainer = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.pnlUsernameContainer = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.pnlPasswordContainer = new DeepCodeAnalytics.UI.Controls.RoundedPanel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new DeepCodeAnalytics.UI.Controls.RoundedButton();
            
            this.pnlLoginContainer.SuspendLayout();
            this.pnlUsernameContainer.SuspendLayout();
            this.pnlPasswordContainer.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // pnlLoginContainer
            // 
            this.pnlLoginContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(27))))); // Zinc-900 (#18181b)
            this.pnlLoginContainer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42))))); // Zinc-800 (#27272a)
            this.pnlLoginContainer.BorderRadius = 16;
            this.pnlLoginContainer.BorderSize = 1;
            this.pnlLoginContainer.Controls.Add(this.lblTitle);
            this.pnlLoginContainer.Controls.Add(this.lblSubtitle);
            this.pnlLoginContainer.Controls.Add(this.lblUsername);
            this.pnlLoginContainer.Controls.Add(this.pnlUsernameContainer);
            this.pnlLoginContainer.Controls.Add(this.lblPassword);
            this.pnlLoginContainer.Controls.Add(this.pnlPasswordContainer);
            this.pnlLoginContainer.Controls.Add(this.btnLogin);
            this.pnlLoginContainer.DrawShadow = true;
            this.pnlLoginContainer.Location = new System.Drawing.Point(50, 40);
            this.pnlLoginContainer.Name = "pnlLoginContainer";
            this.pnlLoginContainer.Size = new System.Drawing.Size(340, 340);
            this.pnlLoginContainer.TabIndex = 0;
            
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(245))))); // Zinc-100 (#f4f4f5)
            this.lblTitle.Location = new System.Drawing.Point(20, 30); // Dikey simetri için Y=30 yapıldı (Yukarıdan 30px boşluk)
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "DeepCode Analytics";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(197)))), ((int)(((byte)(253))))); // Şık soluk mavi (#93c5fd)
            this.lblSubtitle.Location = new System.Drawing.Point(20, 65); // İstek doğrultusunda boşluk açıldı (Başlıkla arasında tam 5px boşluk)
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(300, 20);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Yapay Zeka Destekli Kod Güvenlik Platformu";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.BackColor = System.Drawing.Color.Transparent;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(170))))); // Zinc-400 (#a1a1aa)
            this.lblUsername.Location = new System.Drawing.Point(30, 100);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(81, 17);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "Kullanıcı Adı";
            
            // 
            // pnlUsernameContainer
            // 
            this.pnlUsernameContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42))))); // Zinc-800 (#27272a)
            this.pnlUsernameContainer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48))))); // İstek doğrultusunda daha yumuşak, sönük gri kenarlık (#2d2d30)
            this.pnlUsernameContainer.BorderRadius = 8;
            this.pnlUsernameContainer.BorderSize = 1;
            this.pnlUsernameContainer.Controls.Add(this.txtUsername);
            this.pnlUsernameContainer.DrawShadow = false;
            this.pnlUsernameContainer.Location = new System.Drawing.Point(30, 121);
            this.pnlUsernameContainer.Name = "pnlUsernameContainer";
            this.pnlUsernameContainer.Size = new System.Drawing.Size(280, 36);
            this.pnlUsernameContainer.TabIndex = 3;
            
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42))))); // Zinc-800
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(245))))); // Zinc-100 (#f4f4f5)
            this.txtUsername.Location = new System.Drawing.Point(13, 9); // İstek doğrultusunda X=13px yapılarak sol kenar boşluğu (Left Padding) rahatlatıldı
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(254, 18); // X kayması nedeniyle genişlik dengelendi (254px)
            this.txtUsername.TabIndex = 0;
            
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(161)))), ((int)(((byte)(170))))); // Zinc-400 (#a1a1aa)
            this.lblPassword.Location = new System.Drawing.Point(30, 169);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(35, 17);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Şifre";
            
            // 
            // pnlPasswordContainer
            // 
            this.pnlPasswordContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42))))); // Zinc-800
            this.pnlPasswordContainer.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48))))); // İstek doğrultusunda daha yumuşak, sönük gri kenarlık (#2d2d30)
            this.pnlPasswordContainer.BorderRadius = 8;
            this.pnlPasswordContainer.BorderSize = 1;
            this.pnlPasswordContainer.Controls.Add(this.txtPassword);
            this.pnlPasswordContainer.DrawShadow = false;
            this.pnlPasswordContainer.Location = new System.Drawing.Point(30, 190);
            this.pnlPasswordContainer.Name = "pnlPasswordContainer";
            this.pnlPasswordContainer.Size = new System.Drawing.Size(280, 36);
            this.pnlPasswordContainer.TabIndex = 5;
            
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(42))))); // Zinc-800
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(245))))); // Zinc-100 (#f4f4f5)
            this.txtPassword.Location = new System.Drawing.Point(13, 9); // Sol kenar boşluğu rahatlatıldı
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(254, 18);
            this.txtPassword.TabIndex = 0;
            
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235))))); // Premium Canlı Teknoloji Mavisi (#2563eb)
            this.btnLogin.BorderColor = System.Drawing.Color.Transparent;
            this.btnLogin.BorderRadius = 10;
            this.btnLogin.BorderSize = 0;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(30, 268); // Y=268 yapıldı. Yüksekliği 42px ile birleştiğinde alt sınıra uzaklığı da tam 30px oldu! (Mükemmel Dikey Simetri)
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(280, 42);
            this.btnLogin.TabIndex = 6;
            this.btnLogin.Text = "Giriş Yap";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(9)))), ((int)(((byte)(9)))), ((int)(((byte)(11))))); // Premium Zinc-950 modern koyu arka plan (#09090b)
            this.ClientSize = new System.Drawing.Size(440, 420);
            this.Controls.Add(this.pnlLoginContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DeepCode Analytics - Güvenli Giriş";
            
            this.pnlLoginContainer.ResumeLayout(false);
            this.pnlLoginContainer.PerformLayout();
            this.pnlUsernameContainer.ResumeLayout(false);
            this.pnlUsernameContainer.PerformLayout();
            this.pnlPasswordContainer.ResumeLayout(false);
            this.pnlPasswordContainer.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlLoginContainer;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblUsername;
        private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlUsernameContainer;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private DeepCodeAnalytics.UI.Controls.RoundedPanel pnlPasswordContainer;
        private System.Windows.Forms.TextBox txtPassword;
        private DeepCodeAnalytics.UI.Controls.RoundedButton btnLogin;
    }
}
