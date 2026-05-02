# DeepCode Analytics - Tasarım ve Sistem Mimarisi

Bu doküman, C# ve .NET 8 üzerinde Roslyn SDK altyapısı kullanılarak geliştirilen "DeepCode Analytics" kod analiz aracının mimari yapısını, analiz süreçlerini ve odaklanılan temel "Code Smell" (Kod Kokusu) kural setlerini teknik bir dille özetlemektedir.

## 1. Sistem Mimarisi (High-Level)

Projemiz, kodun sürdürülebilirliğini (maintainability) artırmak ve bağımlılıkları tek yönde sınırlandırmak amacıyla **Clean Architecture** prensiplerine uygun olarak 3 temel katmana (+1 UI) ayrılmıştır:

*   **Domain Katmanı:**
    Sistemin en izole katmanıdır. Hiçbir dış servise veya teknoloji paketine bağımlılığı yoktur. Sadece işin öz modeli olan varlıkları (örneğin; analiz sonucunu temsil eden `AnalysisIssue` modeli) barındırır.
*   **Application Katmanı:**
    Uygulamanın kullanım senaryolarını tanımlar. İsteklerin nasıl karşılanacağına dair sözleşmeleri (`ICodeAnalyzerService`) içerir. İş mantığını yönetir ancak dışsal analiz araçlarını (Roslyn) doğrudan tanımaz, yalnızca arayüzleri tanır.
*   **Infrastructure (Altyapı) Katmanı:**
    Projenin dış dünyayla, sistem araçlarıyla veya Roslyn SDK (`Microsoft.CodeAnalysis`) API'leriyle iletişim kurduğu katmandır. Application katmanının belirlediği arayüzleri, somut sınıflara (Örn: `RoslynAnalyzerService`) dönüştürerek hayata geçirir. Tüm diagnostik kuralları bu katmanda yer alır.
*   **Arayüz (UI) Katmanı:**
    Dependency Injection (DI) kullanılarak yapılandırılmış, son kullanıcının veri girdiği modüldür. İşlemleri doğrudan yapmaz; Application arayüzü sayesinde isteklerini bildirip ekranda listeleme yapar.

**İletişim Akışı:** Kullanıcı arayüzü yalnızca `Application` (Interface) katmanına istek gönderir. `Application` katmanı bu isteği IoC prensibi ile `Infrastructure`'daki Roslyn motoruna yönlendirir. Üretilen ihlal nesneleri `Domain` katmanında bulunan `AnalysisIssue` objesiyle sarmalanarak ekrana geri gönderilir.

---

## 2. Roslyn ile Analiz Süreci Adımları

Girdi olarak verilen C# kodunun, analiz sürecinden geçip hata listesi olarak döndüğü 5 temel adım şunlardır:

1.  **Parsing (Söz Dizimi Analizi):** Sisteme metin olarak giren ham C# kaynak kodu (Source Code), Roslyn Syntax API'si aracılığıyla hiyerarşik bir **Abstract Syntax Tree (AST)** yapısına çözümlenir (`CSharpSyntaxTree.ParseText()`). Kod artık metin değil, bir düğümler (nodes) ağacıdır.
2.  **Compilation (Derleme Ağacının Oluşturulması):** Sözdizimi ağacı, kodun semantik (anlamsal) yapısını anlamlandırabilmek için projenin temel C# .NET referanslarıyla (mscorlib vb.) birleştirilerek güvenli ve sanal bir derleme modülünün (Compilation objesi) içerisine yerleştirilir.
3.  **Diagnostic Analizi (Analyzer Enjeksiyonu):** Önceden belirlenen resmi Roslyn analiz kurallarımız (`LongMethodAnalyzer`, `EmptyCatchAnalyzer`, `MagicNumberAnalyzer`) bu derleme (Compilation) motoruna entegre edilir (`WithAnalyzers()`).
4.  **İşletim ve İhlal Tespiti:** Motor, gelen AST düğümleri üzerinde bizim yazmış olduğumuz analizör sınıflarını asenkron bir biçimde koşturur (`GetAllDiagnosticsAsync()`). Kurala uymayan bir noktaya denk gelinirse Roslyn o satırı ve sütunu işaretleyerek bir Diagnostik fırlatır.
5.  **Mapping (Sonuçların Haritalanması):** Roslyn'in kendi dilindeki karmaşık uyarı ve hata nesneleri tutulup elenir; sadece bizim projemize ait olan uyarılar, sistemimizin ortak iş modeline (`AnalysisIssue`) map'lenerek kullanıcı arayüzüne gönderilir.

---

## 3. İlk Faz Kapsamındaki Code Smell Teknik Tanımları

Analitik motorumuzun tespit etmek üzere tasarlandığı ve kod kalitesini riske atan 3 majör hata tipi şunlardır:

### SM001: Long Method (Aşırı Uzun Metot)
*   **Tanım:** Bir fonksiyon veya yordamın, kendisinden beklenen tek bir amacı gerçekleştirmesinin çok ötesine geçerek onlarca ifadeyi (statement) işgâl etmesidir. *Single Responsibility Principle (SRP)* ihlali niteliği taşır.
*   **Tespit Yaklaşımı:** SyntaxTree içerisindeki AST düğümlerinden `MethodDeclarationSyntax` (Metot tanımları) ele alınır. İlgili metodun iş kodlarını barındıran gövdesinde (Body) yer alan toplam ifade sayısı sayılır ve 20 birimlik kabul edilen eşik değerini aşması halinde raporlanarak test edilebilirliği tehdit ettiği belirtilir.

### SM002: Empty Catch Block (Boş Catch Bloğu)
*   **Tanım:** Uygulamanın normal akışı sırasında karşısına çıkan anormalliklerin/hataların (Exception) `try-catch` bloklarında yakalanmasına rağmen, `catch` içerisinde hiçbir işlem yapılmadan yutularak (Exception Swallowing) kodun sessiz bir şekilde devam etmesi durumudur.
*   **Tespit Yaklaşımı:** `CatchClauseSyntax` düğümleri denetlenerek, hata yönetim bloğunun içi incelenir. Eğer catch bloğu içi tamamen boşsa (ya da sadece developer notlarından vb. ibaretse) hata sessize alındığı için doğrudan raporlanır.

### SM003: Magic Number (Sihirli ve Sabit Rakamlar)
*   **Tanım:** Neyi ifade ettiği belirsiz olan, ancak doğrudan çalıştırılan kod mimarisinin içine gömülmüş kaba sabitlerin (Örn: `delay == 1500`) sistemin iskeletinde taşınmasıdır.
*   **Tespit Yaklaşımı:** Koddaki sayısal metin bağımsız yapıları (`NumericLiteralExpression`) aranır. `0`, `1` ya da `-1` gibi endeks başlangıçları dışındaki sayılar, bir `var`, `const` ya da `readonly` sabite tanımlanmadan salt olarak iş mantığının içine bırakıldıysa izole edilerek refactoring için raporlanır.
