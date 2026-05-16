# DeepCode Analytics - AI Destekli Kod Analiz Platformu

DeepCode Analytics, Roslyn tabanlı statik analiz ve RAG (Retrieval-Augmented Generation) destekli yapay zeka analizini birleştiren profesyonel bir kod analiz platformudur.

## 🚀 Başlangıç

Projeyi yerel bilgisayarınızda çalıştırmak için aşağıdaki adımları izleyin.

### 📋 Gereksinimler

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (veya üzeri)
- Windows (WinForms arayüzü için gereklidir)

### 🛠️ Kurulum Adımları

1. **Depoyu Klonlayın:**
   ```bash
   git clone <repo-url>
   cd "DeepCode YGA"
   ```

2. **API Anahtarlarını Yapılandırın:**
   `DeepCodeAnalytics.UI` klasörü içindeki `appsettings.Example.json` dosyasını kopyalayıp adını `appsettings.json` olarak değiştirin ve kendi API anahtarlarınızı girin:
   - Gemini API Key
   - Groq API Key
   - OpenRouter API Key

3. **Vektör Veritabanı:**
   Proje kök dizinindeki `codesearchnet_vector_db.json` ve `tfidf_vocab.json` dosyaları halihazırda eğitilmiş 846 kayıtlık siber güvenlik ve genel kod verisini içerir. Bu dosyalar uygulama çalıştığında otomatik olarak `bin` klasörüne kopyalanacaktır.

4. **Uygulamayı Başlatın:**
   ```bash
   dotnet run --project DeepCodeAnalytics.UI
   ```

## 🧠 Özellikler

- **Çift Motorlu Analiz:** Roslyn motoru ile kesin hataları (SQL Injection, Hardcoded Secrets vb.) bulurken, AI motoru ile bağlamsal öneriler sunar.
- **Lokal RAG Sistemi:** 846 kayıtlık özel güvenlik veri seti üzerinden yerel TF-IDF vektör araması yaparak AI'ya doğru bağlam sağlar.
- **Çoklu Model Desteği:** Gemini, Groq ve OpenRouter üzerinden farklı yapay zeka modellerini kullanabilirsiniz.
- **Hız ve Verimlilik:** Token limit koruması ve asenkron işlem yapısı ile büyük dosyaları dahi hızla analiz eder.

## 📁 Proje Yapısı

- `DeepCodeAnalytics.UI`: WinForms tabanlı kullanıcı arayüzü.
- `DeepCodeAnalytics.Application`: İş mantığı, servisler ve arayüzler.
- `DeepCodeAnalytics.Infrastructure`: Roslyn analyzer'lar, API servisleri ve RAG implementasyonu.
- `DeepCodeAnalytics.Domain`: Veri modelleri ve çekirdek yapılar.
- `DeepCodeAnalytics.DatasetBuilder`: RAG veritabanını oluşturmak için kullanılan yardımcı araç.

## 🤝 Katkıda Bulunma

1. Yeni bir feature branch oluşturun.
2. Değişikliklerinizi yapın.
3. Pull Request açmadan önce `AnalyzeService` testlerini geçtiğinizden emin olun.
