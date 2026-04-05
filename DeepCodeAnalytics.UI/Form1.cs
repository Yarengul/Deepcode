using DeepCodeAnalytics.Application.Interfaces;
using DeepCodeAnalytics.Domain.Entities;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DeepCodeAnalytics.UI
{
    public partial class Form1 : Form
    {
        private readonly ICodeAnalyzerService _analyzerService;

        /// <summary>
        /// DI Container üzerinden kod analiz servisi arayüze enjekte ediliyor.
        /// Form load aşamasında hazır bekleyecek.
        /// </summary>
        public Form1(ICodeAnalyzerService analyzerService)
        {
            InitializeComponent();
            _analyzerService = analyzerService;
        }

        private async void btnAnalyze_Click(object sender, EventArgs e)
        {
            string code = txtSourceCode.Text;
            if (string.IsNullOrWhiteSpace(code))
            {
                MessageBox.Show("Lütfen analiz edilecek bir kod girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Analiz yapılırken arayüzün (UI) kitlenmemesi ve kullanıcının ikinci defa butona basmasını engellemek için buton pasife alındı
            btnAnalyze.Enabled = false;
            try
            {
                // 3 farklı kural asenkron çalışır ve veriler toplanır
                var issues = await _analyzerService.AnalyzeAsync(code);
                
                // Gelen veriler bir BindingList aracılığıyla DataGridView'a taranıp eklenir. Tablo görünümü oluşur.
                dgvResults.DataSource = new BindingList<AnalysisIssue>(issues);
                
                lblStatus.Text = $"Analiz Tamamlandı. Bulunan sorun sayısı: {issues.Count}";
            }
            catch (Exception ex)
            {
                // Parse sırasında veya analizde beklenmedik hata olursa ekrana yansıtılır.
                MessageBox.Show($"Analiz sırasında bir hata oluştu: {ex.Message}", "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Hata oluştu.";
            }
            finally
            {
                // Bekleme işlemi bittikten sonra formu orijinal kullanımına hazır hale getiriyoruz.
                btnAnalyze.Enabled = true;
            }
        }
    }
}
