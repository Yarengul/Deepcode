using DeepCodeAnalytics.Application.DTOs;
using DeepCodeAnalytics.Application.Parsers;

namespace DeepCodeAnalytics.Tests;

/// <summary>
/// AiResponseParser'ın doğru çalışıp çalışmadığını ve
/// yanlış pozitif (False Positive) oranlarını ölçmek için
/// hazırlanmış test senaryoları.
/// 
/// NOT: Bu testler manuel çalıştırılabilir bir yapıdadır.
/// İleride xUnit veya NUnit ile entegre edilebilir.
/// </summary>
public class FalsePositiveTestScenarios
{
    private readonly AiResponseParser _parser = new();

    /// <summary>
    /// Tüm test senaryolarını çalıştırır ve sonuçları konsola yazar.
    /// </summary>
    public static void RunAll()
    {
        var test = new FalsePositiveTestScenarios();

        Console.WriteLine("=== FALSE POSITIVE TEST SENARYOLARI ===\n");

        test.Test_GecerliJson_BasariylaParseEdilmeli();
        test.Test_MarkdownBlokluJson_TemizlenipParsedEdilmeli();
        test.Test_BozukJson_FallbackIssueIcerikliDonmeli();
        test.Test_BosString_FallbackDonmeli();
        test.Test_SadeceSuggestionVar_IssueBosOlmali();

        Console.WriteLine("\n=== TÜM TESTLER TAMAMLANDI ===");
    }

    /// <summary>
    /// SENARYO 1: Temiz ve geçerli bir JSON geldiğinde doğru parse edilmeli.
    /// False Positive riski: Yanlışlıkla hata olmayan kodu hata olarak işaretleme.
    /// </summary>
    public void Test_GecerliJson_BasariylaParseEdilmeli()
    {
        // Düzenli bir C# kodunun analiz sonucu — hiç issue olmamalı
        string temizKodYaniti = @"{
            ""issues"": [],
            ""suggestions"": [
                {
                    ""suggestionText"": ""Async metod adına 'Async' son eki eklenebilir"",
                    ""proposedCode"": ""public async Task DoWorkAsync()""
                }
            ]
        }";

        var sonuc = _parser.Parse(temizKodYaniti);

        // Temiz kod için False Positive kontrolü: issue listesi boş olmalı
        bool testGecti = sonuc.Issues.Count == 0 && sonuc.Suggestions.Count == 1;
        YazSonuc("SENARYO 1 - Temiz JSON Parse", testGecti);
    }

    /// <summary>
    /// SENARYO 2: Gemini bazen yanıtı ```json ... ``` içinde sarmalayabilir.
    /// Parser bunu temizleyip doğru parse etmeli.
    /// </summary>
    public void Test_MarkdownBlokluJson_TemizlenipParsedEdilmeli()
    {
        // Gemini'nin gerçek hayatta döndürdüğü format (Markdown sarmalı)
        string markdownliYanit = @"```json
        {
            ""issues"": [
                { ""message"": ""Değişken adı küçük harfle başlıyor"", ""severity"": ""Low"" }
            ],
            ""suggestions"": []
        }
        ```";

        var sonuc = _parser.Parse(markdownliYanit);

        bool testGecti = sonuc.Issues.Count == 1 && sonuc.Issues[0].Severity == "Low";
        YazSonuc("SENARYO 2 - Markdown Sarmalı JSON", testGecti);
    }

    /// <summary>
    /// SENARYO 3: Tamamen bozuk (geçersiz) bir JSON geldiğinde sistem çökmemeli.
    /// Fallback olarak "Critical" seviyesinde bir hata mesajı dönmeli.
    /// </summary>
    public void Test_BozukJson_FallbackIssueIcerikliDonmeli()
    {
        // API'den hiç JSON olmayan bozuk bir yanıt geldi
        string bozukYanit = "Bu bir JSON değil, sadece metin.";

        var sonuc = _parser.Parse(bozukYanit);

        // Sistem çökmemeli, fallback Issue dönmeli
        bool testGecti = sonuc.Issues.Count > 0 && sonuc.Issues[0].Severity == "Critical";
        YazSonuc("SENARYO 3 - Bozuk JSON Fallback", testGecti);
    }

    /// <summary>
    /// SENARYO 4: Boş string gelirse sistem çökmemeli.
    /// </summary>
    public void Test_BosString_FallbackDonmeli()
    {
        string bosYanit = "";

        var sonuc = _parser.Parse(bosYanit);

        // Boş yanıtta da uygulama ayakta kalmalı
        bool testGecti = sonuc != null;
        YazSonuc("SENARYO 4 - Boş Yanıt Dayanıklılık", testGecti);
    }

    /// <summary>
    /// SENARYO 5: Sadece suggestion içeren, issue'suz bir kod analizi.
    /// İyi yazılmış kod için yanlış issue üretilmemeli (False Positive = 0).
    /// </summary>
    public void Test_SadeceSuggestionVar_IssueBosOlmali()
    {
        // Çok iyi yazılmış kod → hiç hata yok, sadece küçük bir öneri var
        string iyiKodYaniti = @"{
            ""issues"": [],
            ""suggestions"": [
                {
                    ""suggestionText"": ""Performans için StringBuilder kullanılabilir"",
                    ""proposedCode"": ""var sb = new StringBuilder();""
                }
            ]
        }";

        var sonuc = _parser.Parse(iyiKodYaniti);

        // False Positive testi: İyi kod için Issue ÜRETİLMEMELİ
        bool falsePositiveYok = sonuc.Issues.Count == 0;
        YazSonuc("SENARYO 5 - False Positive = 0 (İyi Kod)", falsePositiveYok);
    }

    // Yardımcı konsol çıktısı
    private static void YazSonuc(string testAdi, bool gecti)
    {
        string ikon = gecti ? "✅" : "❌";
        Console.WriteLine($"{ikon} {testAdi}: {(gecti ? "GEÇTİ" : "BAŞARISIZ")}");
    }
}
