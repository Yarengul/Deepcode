# Sprint 4 Code Smell İstatistikleri (Gerçek Dünya Projeleri)

Bu rapor, açık kaynaklı GitHub projelerinden (`dotnet/roslyn`, `dotnet/aspnetcore`, `eShopOnWeb` vb.) rastgele seçilen .cs dosyalarının Roslyn tabanlı analyzer'larımızdan geçirilmesi sonucu oluşturulmuştur.

| Dosya Adı | Magic Number | Long Method | Empty Catch | Naming Convention | Toplam |
|-----------|--------------|-------------|-------------|-------------------|--------|
| `LanguageParser.cs` | 4 | 117 | 0 | 46 | **167** |
| `OrderController.cs` | 0 | 0 | 0 | 0 | **0** |
| `OrderService.cs` | 0 | 0 | 0 | 0 | **0** |
| `DbContext.cs` | 0 | 5 | 0 | 0 | **5** |
| `DbSet.cs` | 0 | 0 | 0 | 0 | **0** |
| `JsonConvert.cs` | 2 | 1 | 0 | 0 | **3** |
| `JsonTextReader.cs` | 22 | 19 | 0 | 2 | **43** |
| `Mapper.cs` | 0 | 0 | 0 | 0 | **0** |
| `MapperConfiguration.cs` | 1 | 2 | 0 | 1 | **4** |
| `AbstractValidator.cs` | 0 | 1 | 0 | 1 | **2** |
| `DefaultValidatorOptions.cs` | 0 | 0 | 0 | 0 | **0** |
| **GENEL TOPLAM** | **29** | **145** | **0** | **50** | **224** |

## Çıkarımlar ve İyileştirmeler
- **False Positive Engelleme**: Testler sırasında `NamingConventionAnalyzer`'ın olay dinleyicilerindeki (event handlers) `e` harfini veya döngülerdeki `c` (char) değişkenlerini hata olarak işaretlediği tespit edildi. Bu yüzden `AllowedSingleCharVariables` listesi güncellendi (`e`, `c`, `_` eklendi).
- **Magic Number İyileştirmesi**: 0, 1, -1'in yanı sıra çift/tek ve yüzdelik hesaplamalarda sıklıkla kullanılan 2, 10 ve 100 sayıları da yoksayılanlar listesine eklenerek daha verimli sonuçlar elde edildi.
