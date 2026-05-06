using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DeepCodeAnalytics.Application.Services;
using DeepCodeAnalytics.Application.Interfaces;
using DeepCodeAnalytics.Infrastructure.Analyzers;

namespace DeepCodeAnalytics.UI
{
    public partial class Form1 : Form
    {
        private KodAnalizServisi _kodAnalizServisi;

        public Form1()
        {
            InitializeComponent();
            
            // Backend servisinin ve bağımlılıklarının başlatılması (DI simülasyonu)
            var analyzers = new List<ICodeAnalyzer>
            {
                new MagicNumberAnalyzer(),
                new EmptyCatchAnalyzer(),
                new LongMethodAnalyzer()
            };
            var analyzeService = new AnalyzeService(analyzers);
            
            // Köprü görevi gören KodAnalizServisi'nin oluşturulması
            _kodAnalizServisi = new KodAnalizServisi(analyzeService);
        }

        // UI ekibinin sonuçları ekrana basmak için eklediği metot
        public void SonuclariGoster(AnalizSonucu sonuc)
        {
            // Bulunan sorunların UI üzerinde gösterilmesi
            if (sonuc.Sorunlar.Count == 0)
            {
                MessageBox.Show("Harika! Kodunuzda herhangi bir sorun bulunamadı.", "Analiz Sonucu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string mesaj = $"Analiz tamamlandı. Toplam {sonuc.Sorunlar.Count} sorun bulundu:\n\n";
            foreach (var sorun in sonuc.Sorunlar)
            {
                mesaj += $"- [{sorun.OnemDerecesi}] Satır {sorun.Satir}: {sorun.Baslik}\n";
            }

            MessageBox.Show(mesaj, "Analiz Sonucu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // UI üzerindeki 'Analiz Et' butonunun Click event'i
        private void btnAnalizEt_Click(object sender, EventArgs e)
        {
            // 1. Ekrandaki kod giriş alanından string olarak C# kodunu al
            string kod = rtbKodGiris.Text;

            if (string.IsNullOrWhiteSpace(kod))
            {
                MessageBox.Show("Lütfen analiz edilecek bir kod giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. KodAnalizServisi.KoduAnalizEt() metoduna kodu gönder ve sonucu al
            var sonuc = _kodAnalizServisi.KoduAnalizEt(kod);

            // 3. Dönen AnalizSonucu objesini SonuclariGoster() metoduna parametre olarak ver
            SonuclariGoster(sonuc);
        }
    }
}
