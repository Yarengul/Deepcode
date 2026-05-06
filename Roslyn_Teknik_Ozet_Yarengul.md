# DeepCodeAnalytics - Roslyn Teknik Özeti (Yarengül İçin)

Selam Yarengül, bu doküman projemizin kalbi olan **Roslyn** altyapısını ve yazdığımız analiz sisteminin (analyzer'ların) mimarisini anlaman için hazırlandı.

## 1. Roslyn Nedir?
Roslyn, .NET'in açık kaynaklı derleyici (compiler) platformudur. Klasik derleyicilerin aksine "kara kutu" değildir; C# kodunu okuyup anlamamıza, üzerinde gezinmemize ve kurallar yazmamıza olanak tanıyan **Compiler as a Service (CaaS)** yapısı sunar. 

Biz projede kodun sadece metin halini değil, Roslyn'in bu kodu anladığı dilbilgisi ağacı olan **SyntaxTree** (Sözdizimi Ağacı) halini kullanıyoruz.

## 2. SyntaxTree ve CSharpSyntaxWalker/Tree
Bize verilen C# kodunu `CSharpSyntaxTree.ParseText(code)` komutuyla ayrıştırdığımızda elimizde bir ağaç olur.
- **Root (Kök):** Kodun en başıdır (CompilationUnit).
- **Nodes (Düğümler):** Class'lar, metotlar, try-catch blokları vb.
- **Tokens (Jetonlar):** Keyword'ler (`public`, `class`, `int`) veya noktalama işaretleri (`{`, `}`).

Ağaç üzerinde gezinmek için iki yöntem kullanırız:
1. **Walker (Ziyaretçi) Pattern (`CSharpSyntaxWalker`):** Roslyn'in kendi sınıfını miras alarak sadece ilgilendiğimiz node tiplerini (örneğin `VisitMethodDeclaration`) ezdiğimiz (override) yaklaşım.
2. **LINQ ile Sorgulama (`DescendantNodes()`):** Projemizde genellikle tercih ettiğimiz, daha pratik olan yol. Ağacın tüm alt elemanlarını düz bir liste gibi alıp, sadece ilgilendiğimiz SyntaxKind'ları (örneğin `VariableDeclaratorSyntax`) filtreleriz.

## 3. Sistem Mimarimiz ve `ICodeAnalyzer`
Yeni bir kural eklemeyi çok kolay hale getiren bir mimari kurduk:

```csharp
public interface ICodeAnalyzer
{
    List<AnalysisDiagnostic> Analyze(SyntaxTree tree);
}
```

Her Code Smell için (Örn: `MagicNumberAnalyzer`, `NamingConventionAnalyzer`) bu arayüzü uygulayan (implement eden) sınıflar yazdık. Bir analyzer sadece kendi işine odaklanır ve bir `AnalysisDiagnostic` listesi döndürür.

## 4. `AnalyzeService` Nasıl Çalışır?
Bütün yükü taşıyan orkestratör servisimizdir.
1. `DeepCodeAnalytics.UI` veya bir API'den gelen ham C# kodunu alır.
2. Roslyn ile bu kodu `SyntaxTree` nesnesine çevirir.
3. Kendisine kayıtlı (Inject edilmiş) olan **tüm ICodeAnalyzer sınıflarını sırayla çalıştırır** ve bu ağacı onlara verir.
4. Tüm analyzer'lardan gelen hataları (diagnostics) birleştirip satır numarasına göre sıralayarak ekrana/dışarıya döndürür.

### False Positive'ler ile Mücadele
Kod analizi yaparken kodun doğru kısımlarını yanlışlıkla hata gibi göstermek (False Positive) yaygındır. 
Örneğin `NamingConventionAnalyzer` sadece tek harfli değişkenlere kızarken, olay dinleyicilerindeki `e` (EventArgs) veya döngülerdeki `c` (char) değişkenlerinin kodun doğası gereği olduğunu bildiğimizden bunları "İzin Verilenler" listesine ekledik. Böylece Yarengül, analiz sonuçlarını incelerken gereksiz gürültüden (noise) kurtulmuş olur.

*Soruların olursa projeyi incelerken bu dokümandan ve yazılan yorum satırlarından faydalanabilirsin! Başarılar!*
