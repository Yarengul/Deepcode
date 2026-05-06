using System.Collections.Generic;
using DeepCodeAnalytics.Domain.Models;

namespace DeepCodeAnalytics.Application.Services
{
    // UI Ekibinin sonuçları göstermek için kullandığı Türkçe modeller
    public class AnalizSonucu
    {
        public List<KodSorunu> Sorunlar { get; set; } = new List<KodSorunu>();
        public List<YapayZekaOnerisi> Oneriler { get; set; } = new List<YapayZekaOnerisi>();
    }

    public class KodSorunu
    {
        public string? Baslik { get; set; }
        public string? Mesaj { get; set; }
        public int Satir { get; set; }
        public string? OnemDerecesi { get; set; }
    }

    public class YapayZekaOnerisi
    {
        public string? Oneri { get; set; }
    }

    /// <summary>
    /// UI ve Backend (AnalyzeService) arasındaki iletişimi sağlayan Bridge (Köprü) servisi.
    /// Backend'den dönen İngilizce/Teknik modelleri UI'ın beklediği Türkçe modellere (Mapping) çevirir.
    /// </summary>
    public class KodAnalizServisi
    {
        private readonly AnalyzeService _analyzeService;

        public KodAnalizServisi(AnalyzeService analyzeService)
        {
            _analyzeService = analyzeService;
        }

        /// <summary>
        /// UI'dan gelen ham C# kodunu alır, analiz eder ve UI modeline çevirip döndürür.
        /// </summary>
        /// <param name="kod">Analiz edilecek C# kodu</param>
        /// <returns>UI katmanının beklediği AnalizSonucu nesnesi</returns>
        public AnalizSonucu KoduAnalizEt(string kod)
        {
            // 1. Gerçek backend servisini çağır
            var diagnostics = _analyzeService.Analyze(kod);

            // 2. UI'ın beklediği modele çevir (Mapping)
            var sonuc = new AnalizSonucu();

            if (diagnostics != null)
            {
                foreach (var diag in diagnostics)
                {
                    sonuc.Sorunlar.Add(new KodSorunu
                    {
                        Baslik = diag.Title,
                        Mesaj = diag.Message,
                        Satir = diag.Line,
                        OnemDerecesi = SeverityCevir(diag.Severity)
                    });
                }
            }

            return sonuc;
        }

        /// <summary>
        /// Backend'den gelen İngilizce önem derecesini UI için Türkçeye çevirir.
        /// </summary>
        private string SeverityCevir(string severity)
        {
            switch (severity?.ToLower())
            {
                case "critical": return "Kritik";
                case "high": return "Yüksek";
                case "medium": return "Orta";
                case "low": return "Düşük";
                default: return severity ?? "Bilinmiyor";
            }
        }
    }
}
