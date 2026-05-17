using System;
using System.Drawing;
using System.Windows.Forms;
using DeepCodeAnalytics.Application.Services;

namespace DeepCodeAnalytics.UI
{
    public partial class LoginForm : Form
    {
        private readonly AnalizYoneticisi _analizYoneticisi;
        private const string UsernamePlaceholder = "Kullanıcı adınızı girin...";
        private const string PasswordPlaceholder = "Şifrenizi girin...";

        // Pixel-Perfect Tasarım Renk Paletleri (Maksimum Uyum)
        private static readonly Color ColorZinc100 = Color.FromArgb(244, 244, 245);       // #f4f4f5 (Yazı rengi)
        private static readonly Color ColorZinc500 = Color.FromArgb(113, 113, 122);       // #71717a (Soluk placeholder)
        private static readonly Color ColorZinc800Border = Color.FromArgb(45, 45, 48);     // #2d2d30 (Yumuşatılmış sönük aktif olmayan kenarlık)
        private static readonly Color ColorBlue500 = Color.FromArgb(59, 130, 246);        // #3b82f6 (Focus Border & Hover)
        private static readonly Color ColorBlue600 = Color.FromArgb(37, 99, 235);        // #2563eb (Normal buton)
        private static readonly Color ColorBlue700 = Color.FromArgb(29, 78, 216);        // #1d4ed8 (MouseDown)

        public LoginForm(AnalizYoneticisi analizYoneticisi)
        {
            _analizYoneticisi = analizYoneticisi;
            InitializeComponent();
            SetupPlaceholders();
            SetupFocusEffects();
            SetupButtonEffects();
        }

        /// <summary>
        /// Giriş kutuları (TextBox) için dinamik placeholder (ghost text) mantığını ayarlar.
        /// </summary>
        private void SetupPlaceholders()
        {
            // Kullanıcı adı placeholder ayarları - varsayılan olarak "admin" pre-fill edilir
            txtUsername.Text = "admin";
            txtUsername.ForeColor = ColorZinc100;
            
            txtUsername.Enter += (s, e) =>
            {
                if (txtUsername.Text == UsernamePlaceholder)
                {
                    txtUsername.Text = "";
                    txtUsername.ForeColor = ColorZinc100;
                }
            };
            
            txtUsername.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    txtUsername.Text = UsernamePlaceholder;
                    txtUsername.ForeColor = ColorZinc500;
                }
            };

            // Şifre placeholder ayarları - varsayılan olarak "admin" pre-fill edilir
            txtPassword.Text = "admin";
            txtPassword.ForeColor = ColorZinc100;
            txtPassword.UseSystemPasswordChar = true; // Şifre maskelenmiş olarak başlar
            
            txtPassword.Enter += (s, e) =>
            {
                if (txtPassword.Text == PasswordPlaceholder)
                {
                    txtPassword.Text = "";
                    txtPassword.ForeColor = ColorZinc100;
                    txtPassword.UseSystemPasswordChar = true; // Şifre girerken maskele (•)
                }
            };
            
            txtPassword.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    txtPassword.UseSystemPasswordChar = false; // Şifre boşsa maskelemeyi kapat
                    txtPassword.Text = PasswordPlaceholder;
                    txtPassword.ForeColor = ColorZinc500;
                }
            };
        }

        /// <summary>
        /// Kutulara odaklanıldığında container panellerine şık mavi çerçeve (Focus Border) parlaması verir.
        /// </summary>
        private void SetupFocusEffects()
        {
            // Kullanıcı adı kutusu odaklanma kontrolü
            txtUsername.Enter += (s, e) =>
            {
                pnlUsernameContainer.BorderColor = ColorBlue500;
                pnlUsernameContainer.Invalidate();
            };
            txtUsername.Leave += (s, e) =>
            {
                pnlUsernameContainer.BorderColor = ColorZinc800Border; // Sönük gri kenarlığa geri dön
                pnlUsernameContainer.Invalidate();
            };

            // Şifre kutusu odaklanma kontrolü
            txtPassword.Enter += (s, e) =>
            {
                pnlPasswordContainer.BorderColor = ColorBlue500;
                pnlPasswordContainer.Invalidate();
            };
            txtPassword.Leave += (s, e) =>
            {
                pnlPasswordContainer.BorderColor = ColorZinc800Border; // Sönük gri kenarlığa geri dön
                pnlPasswordContainer.Invalidate();
            };
        }

        /// <summary>
        /// Giriş butonunun fare ile etkileşimini dinamik olarak yönetir.
        /// </summary>
        private void SetupButtonEffects()
        {
            // Fare butonun üstüne geldiğinde (Hover)
            btnLogin.MouseEnter += (s, e) =>
            {
                btnLogin.BackColor = ColorBlue500;
            };

            // Fare butondan ayrıldığında (Normal)
            btnLogin.MouseLeave += (s, e) =>
            {
                btnLogin.BackColor = ColorBlue600;
            };

            // Butona basıldığında (Click)
            btnLogin.MouseDown += (s, e) =>
            {
                btnLogin.BackColor = ColorBlue700;
            };

            // Butondan tık kaldırıldığında (Up)
            btnLogin.MouseUp += (s, e) =>
            {
                btnLogin.BackColor = ColorBlue500;
            };
        }

        private void btnLogin_Click(object? sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Placeholder kontrolünü de içeren alan doğrulaması
            if (string.IsNullOrEmpty(username) || username == UsernamePlaceholder || 
                string.IsNullOrEmpty(password) || password == PasswordPlaceholder)
            {
                MessageBox.Show("Lütfen tüm alanları doldurun!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Test Giriş Kontrolü (admin/admin)
            if (username == "admin" && password == "admin")
            {
                this.Hide();
                Form1 mainForm = new Form1(_analizYoneticisi);
                mainForm.Closed += (s, args) =>
                {
                    if (!mainForm.IsLoggingOut)
                    {
                        this.Close();
                    }
                };
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Hatalı kullanıcı adı veya şifre!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Oturum kapatıldığında giriş formunu temizler ve yeniden gösterir.
        /// </summary>
        public void ResetFormForLogout()
        {
            txtUsername.Text = "admin";
            txtUsername.ForeColor = ColorZinc100;

            txtPassword.UseSystemPasswordChar = true;
            txtPassword.Text = "admin";
            txtPassword.ForeColor = ColorZinc100;

            this.Show();
        }
    }
}