using System;

namespace DeepCodeAnalytics.Domain.Entities
{
    /// <summary>
    /// Kod analizi sırasında tespit edilen her bir kural ihlalini (Code Smell/Diagnostic) temsil eder.
    /// Scrum Master'ın gereksinimlerine göre ismi AnalysisIssue olarak belirlenmiştir.
    /// </summary>
    public class AnalysisIssue
    {
        /// <summary>
        /// İhlal edilen kuralın özel kimliği (Örneğin: CA1001, LM001).
        /// </summary>
        public string DiagnosticId { get; set; } = string.Empty;

        /// <summary>
        /// İhlalin kullanıcıya gösterilecek olan anlaşılır detaylı mesajı.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// İhlalin ciddiyet seviyesi (Örneğin: Warning, Error, Info).
        /// </summary>
        public string Severity { get; set; } = string.Empty;

        /// <summary>
        /// İhlalin kod içinde bulunduğu satır numarası.
        /// </summary>
        public int Line { get; set; }

        /// <summary>
        /// İhlalin kod içinde bulunduğu sütun (karakter) numarası.
        /// </summary>
        public int Character { get; set; }
    }
}
