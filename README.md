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
PhoneBookDbContext: ContactService'teki veriye erişmek için ContactService projesinden referans alır.
ReportDbContext: Raporları saklar (Rapor ID, Lokasyon, PersonCount, PhoneCount, Durum)


AKIŞ:
Yeni rapor istenince (bir lokasyon girilir), veri kuyrukta bekletilir.
BackgroundService, bu kuyruğu dinler ve veritabanından gerekli hesaplamaları yapar.
Rapor tamamlanınca durumu Completed olur.


Test:
ContactService çalıştırılır, Swagger’dan kişi ve iletişim bilgileri eklenir.
ReportService çalıştırılır.
Swagger’dan POST /api/report ile konum belirtilir (örn: "ankara").
Arka planda servis çalışır, veriyi işler.
GET /api/report/{id} ile sonucu görüntüleyebilirsin.

