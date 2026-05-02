using System;

namespace DeepCodeAnalytics.Domain.Models
{
    /// <summary>
    /// Kod analizörlerinden dönen standart hata bildirim modeli.
    /// UI veya farklı katmanlarda bu model üzerinden listeleme/raporlama yapılır.
    /// </summary>
    public class AnalysisDiagnostic
    {
        /// <summary>
        /// Kuralın veya bulgunun kısa başlığı (Örn: "Sihirli Rakam (Magic Number)")
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Bulgunun önem derecesi: "Low", "Medium", "High", "Critical"
        /// </summary>
        public string Severity { get; set; }

        /// <summary>
        /// Hatanın bulunduğu satır numarası
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// Hatanın detaylı açıklaması
        /// </summary>
        public string Message { get; set; }

        public AnalysisDiagnostic(string title, string severity, int line, string message)
        {
            Title = title;
            Severity = severity;
            Line = line;
            Message = message;
        }
    }
}
