  DeepCode Analytics: AI-Powered Hybrid Code AnalysisDeepCode Analytics, C# projelerindeki teknik borçları (Technical Debt) ve yapısal hataları tespit etmek için Microsoft Roslyn SDK'nın kesinliğini ve Google Gemini AI'nın anlamsal gücünü birleştiren hibrit bir statik analiz aracıdır.

 Proje Hedefleri 

Hibrit Analiz:Roslyn ile %100 doğru sentaks taraması ve Gemini ile akıllı refactoring önerileri.
Yüksek Doğruluk: Hibrit model sayesinde %90+ doğruluk oranı.Hız: Veri optimizasyonu ile 1 saniyenin altında analiz sonuçları.
Temiz Kod: Yazılımcılara sadece hataları değil, çözüm yollarını da öğreten bir rehber.

Teknik Altyapı:
Platform: Windows Desktop (C# WinForms)
Statik Analiz: Microsoft Roslyn SDK (C# Compiler Platform)
Yapay Zeka: Google Gemini API (Generative AI)Metodoloji  Agile / Scrum

Ekip ve Rol Dağılımı:
1) Yarengül Kocaoğlu: Scrum Master & ArchitectIEEE Standartlarında dökümantasyon (SRS, SDD), UML modelleme, QA süreç yönetimi ve proje koordinasyonu.
2) Nergis Albayrak: Backend Developer Roslyn SDK entegrasyonu, AST (Abstract Syntax Tree) analizi, kural tabanlı kod kokusu (Code Smell) motoru.
3) Cem Deniz Şahin: AI & Integration Eng. Gemini API asenkron veri akışı, Prompt Engineering, doğruluk testleri ve performans metrikleri.
4) Muhammed Hatip: UI/UX Developer WinForms Dashboard tasarımı, dinamik kod editörü entegrasyonu ve analiz raporlama arayüzleri.

    Proje Yol Haritası:
1) Sprint 1: Kapsam Analizi & Temel Altyapı Kurulumu (Checkpoint-1)
2) Sprint 2: Gereksinim Analizi & UML Modelleme (Checkpoint-2)
3) Sprint 3: Mimari Tasarım & İlk Geliştirmeler (Checkpoint-3)
4) Sprint 4: Gelişmiş Özellikler & AI Entegrasyonu (Checkpoint-4)
5) Sprint 5: Final Testleri & Dağıtım (Checkpoint-5)
            Kurulum ve Çalıştırma
 (Proje geliştirme aşamasındadır)
1) Bu depoyu klonlayın: git clone https://github.com/yarengul/DeepCode.git
2) Visual Studio 2022+ ile çözümü açın.
3) NuGet paketlerini (Roslyn, Newtonsoft.Json vb.) geri yükleyin.
4) Kendi Gemini API anahtarınızı yapılandırma dosyasına ekleyin.
5) Projeyi Build edip çalıştırın.
   Bu proje Fırat Üniversitesi Yazılım Mühendisliği Bölümü kapsamında geliştirilmektedir.

## Sprint 3 - Analyzer Tests

Sprint 3 kapsamında kod analiz kurallarını (Code Smells) algılayacak çekirdek sınıflar oluşturulmuş ve `AnalyzeService` üzerinde entegre edilmiştir.

### 1. Magic Number Analyzer
Hatalı Kod Örneği:
```csharp
public void Calculate()
{
    double result = 3.14 * 5; // 3.14 ve 5 magic number
}
```
Beklenen Servis Çıktısı (JSON):
```json
[
  {
    "Title": "Sihirli Rakam (Magic Number)",
    "Severity": "Medium",
    "Line": 3,
    "Message": "'3.14' değeri doğrudan kod içine yazılmış. Anlamlı bir isimlendirilmiş sabite (const/readonly) atanmalıdır."
  },
  {
    "Title": "Sihirli Rakam (Magic Number)",
    "Severity": "Medium",
    "Line": 3,
    "Message": "'5' değeri doğrudan kod içine yazılmış. Anlamlı bir isimlendirilmiş sabite (const/readonly) atanmalıdır."
  }
]
```

### 2. Empty Catch Analyzer
Hatalı Kod Örneği:
```csharp
try {
    DoSomething();
} catch (Exception ex) {
    // Sadece yorum var, hata yutuluyor
}
```
Beklenen Servis Çıktısı (JSON):
```json
[
  {
    "Title": "Boş Catch Bloğu (Empty Catch Block)",
    "Severity": "High",
    "Line": 3,
    "Message": "Catch bloğu içinde hiçbir işlem yapılmıyor. Hata nesnesi yutuluyor ve loglanmıyor."
  }
]
```

### 3. Long Method Analyzer
Hatalı Kod Örneği:
```csharp
public void VeryLongMethod()
{
    // ... >30 satır kod ...
}
```
Beklenen Servis Çıktısı (JSON):
```json
[
  {
    "Title": "Uzun Metot (Long Method)",
    "Severity": "High",
    "Line": 1,
    "Message": "'VeryLongMethod' metodu 35 satır uzunluğunda. Maksimum izin verilen sınır 30 satırdır. Metodu daha küçük parçalara bölmeyi düşünün."
  }
]
```

### 4. Naming Convention Analyzer
Hatalı Kod Örneği:
```csharp
public void calculateTotal() // Küçük harfle başlıyor
{
    int a = 5; // a tek harfli
}
```
Beklenen Servis Çıktısı (JSON):
```json
[
  {
    "Title": "İsimlendirme Hatası (Naming Convention)",
    "Severity": "Medium",
    "Line": 1,
    "Message": "'calculateTotal' metodu küçük harfle başlıyor. Metot isimleri PascalCase olmalıdır."
  },
  {
    "Title": "İsimlendirme Hatası (Naming Convention)",
    "Severity": "Medium",
    "Line": 3,
    "Message": "'a' değişkeni tek harfli. Anlamlı ve açıklayıcı bir isim kullanılmalıdır (izin verilenler: i, j, k, x, y, z)."
  }
]
```
