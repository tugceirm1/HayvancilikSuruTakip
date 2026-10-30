# Hayvancılık Sürü Takip ve Sağlık Kaydı

## Projenin Amacı
Hayvanların kimlik, aşı, sağlık ve verim bilgilerinin dijital ortamda kayıt altına alınmasını ve takip edilmesini sağlamak.

## Kullanılacak Teknolojiler
- C#
- Windows Forms
- SQL Server
- Visual Studio

## Proje Modülleri
- Hayvan kayıt ve kimlik yönetimi
- Aşı ve sağlık takibi
- Süt verimi kayıtları
- Uyarı ve hatırlatma sistemi
- Raporlama

## Proje Mimarisi
Katmanlı mimari kullanılacaktır.

- Forms: Kullanıcı arayüzü
- Models: Veri modelleri
- Data: Veritabanı işlemleri
- Services: İş kuralları

## Veritabanı Tabloları
- Hayvanlar
- AsiKayitlari
- VerimKayitlari
- Uyarilar

## Proje Durumu
## Proje Durumu

### 3. Hafta – Proje Kurulumu
- Visual Studio ile C# Windows Forms projesi oluşturuldu.
- Katmanlı mimari ve veritabanı tabloları tasarlandı.
- GitHub deposu oluşturuldu.

### 4. Hafta – Veritabanı İşlemleri
- SQL Server bağlantısı kuruldu.
- Hayvan kayıtlarını listeleme, ekleme, güncelleme ve silme işlemleri geliştirildi.
- Veritabanı SQL dosyası projeye eklendi.

### 5. Hafta – Kullanıcı Yönetimi ve Güvenlik
- Kullanıcı kayıt ve giriş ekranları oluşturuldu.
- PBKDF2-SHA256 ile güvenli parola hashleme eklendi.
- Oturum yönetimi geliştirildi.
- Çiftçi ve Uzman rolleri oluşturuldu.
- Rol bazlı erişim kontrolü uygulandı.
- 5 hatalı girişte 15 dakika hesap kilitleme testi gerçekleştirildi.

**Güncel Durum:** 5. hafta geliştirmeleri tamamlandı. Son güvenlik kontrolleri ve raporlama aşamasındadır.
