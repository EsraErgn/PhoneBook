İki mikroservisten oluşmaktadır:

1-ContactService: Kişi ve iletişim bilgilerini yönetir.

2-ReportService: Belirli lokasyonlara göre rapor oluşturur.



1-ContactService:

Person modeli: Kişinin ad, soyad, şirket bilgilerini tutar.

ContactInfo modeli: Kişiye ait iletişim bilgilerini (Telefon, Lokasyon, Email) tutar.

PhoneBookDbContext: Persons ve ContactInfos tablolarına erişim sağlar.

Veritabanı:PostgreSQL

Tablolar: Persons, ContactInfos


2-ReportService:
Belirli bir konum (örneğin: Ankara) için kaç kişi olduğunu ve toplam kaç telefon numarası olduğunu raporlar.

Asenkron işleyiş: Rapor oluşturulunca bir kuyrukta bekler, BackgroundService bu kuyruğu dinler ve işi işler.

Veritabanı:PostgreSQL

PhoneBookDbContext: ContactService'teki veriye erişmek için ContactService projesinden referans alır.

ReportDbContext: Raporları saklar (Rapor ID, Lokasyon, PersonCount, PhoneCount, Durum)


Akış:
Yeni rapor istenince (bir lokasyon girilir), veri kuyrukta bekletilir.
BackgroundService, bu kuyruğu dinler ve veritabanından gerekli hesaplamaları yapar.
Rapor tamamlanınca durumu Completed olur.

Teknolojiler:
- .NET 8 Web API
- Entity Framework Core
- PostgreSQL
- BackgroundService (asenkron işleyiş)
- In-memory Queue (kuyruk sistemi)


  
Test Senaryosu:
ContactService çalıştırılır, Swagger’dan kişi ve iletişim bilgileri eklenir.
ReportService çalıştırılır.
Swagger’dan POST /api/report ile konum belirtilir (örn: "ankara").
Arka planda servis çalışır, veriyi işler.
GET /api/report/{id} ile sonucu görüntüleyebilirsin.




--Örnek Endpointler--

///Persons

GET /api/Persons: Tüm kişileri getir
--Örnek:
GET /api/Persons


POST /api/Persons: Yeni kişi ekle
-- Örnek:
{
  "name": "Örnek",
  "surname": "Örnek",
  "company": "ABC A.Ş."
}


GET /api/Persons/{id}: ID'ye göre kişi getir

DELETE /api/Persons/{id}: Kişiyi sil

GET /api/Persons/{id}/details: Kişi ve iletişim detaylarını getir


///ContactInfos

POST /api/ContactInfos: İletişim bilgisi ekle
--Örnek:
{
  "personId": "GUID",
  "type": 0, // Phone = 0, Email = 1, Location = 2
  "content": "05555555555"
}


GET /api/ContactInfos/person/{personId}: Kişiye ait iletişim bilgilerini getir

DELETE /api/ContactInfos/{id}: İletişim bilgisini sil


///Reports


POST /api/Reports/create: Rapor isteği oluştur
--Örnek:
{
  "location": "ankara"
}


GET /api/Reports/getreports: Tüm raporları getir

GET /api/Reports/details/{id}: Rapor detayını getir


///ReportData

GET /api/report-data/reports/{location}: Belirli lokasyon için kişi/telefon sayısı getirir


