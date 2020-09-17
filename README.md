# Otopark Otomasyonu

## Proje Hakkında
Bu yazılım Otopark işletmeleri için Katmanlı Mimari kullanılarak C# diliyle yazılmıştır. Herhangi bir somut objeye gerek duymadan sadece bilgisayar üzerinden çalışmaktadır. Birden fazla cihazdan(bilgisayar) bir otoparkı yönetebilmek için MySQL veritabanı tercih edilmiştir.

## Kurulum Dosyaları
MySQL destekli SQL dosyası DataAccess>Concrete>MySQL klasörü altında Backup.sql olarak adlandırılmıştır.
* [Xampp](https://www.apachefriends.org/tr/index.html) - MySQL veritabanını kullanabilmek için gereken program.
* [Wampp](https://www.wampserver.com/en/) - Xampp'a alternatif program.

## İlk Kurulumdan Sonra
Giriş sayfasına girilmesi gereken varsayılan giriş bilgileridir, Personel Düzenleme sayfasından düzenlenebilir.
```sh
Kullanıcı Adı - "admin"
Şifre - "123"
```

## Özellikler

  - Park alanlarını tut-bırak ile dinamik olarak düzenleyebilme
  - Hesaplama algoritmasıyla otomatik ücret hesaplama
  - Çoklu dil desteği (Şuan için sadece DataGrid)
  - Araç tipi belirleyerek fiyatlandırmayı özelleştirebilme
  - Park Alanı ekranında hızlı araç kayıt edebilme
  - Personel ekleyerek personelin gerçekleştirdiği işlemleri görebilme
  - Marka, Model, Araç, Müşteri, Personel, Park, Fiyatlandırma gibi işlemlerin hepsinin CRUD işlemleri bulunmaktadır.

### Görseller
Bilgilendirme: "Marka, Model, Araç, Müşteri, Personel, Park, Fiyatlandırma gibi sayfalar görsellere eklenmemiştir."
##### Giriş Sayfası

![Image of Yaktocat](https://raw.githubusercontent.com/ozanercan/Otopark/master/resimler/Login.jpg)

##### Ana Sayfa

![Image of Yaktocat](https://raw.githubusercontent.com/ozanercan/Otopark/master/resimler/Main%20Page.jpg)

##### Park Alanı Düzenleme Safası

![Image of Yaktocat](https://raw.githubusercontent.com/ozanercan/Otopark/master/resimler/Park%20Alan%C4%B1%20D%C3%BCzenleme.jpg)

##### Park Alanına Araç Ekleme Penceresi

![Image of Yaktocat](https://raw.githubusercontent.com/ozanercan/Otopark/master/resimler/Park%20Alan%C4%B1%20Kay%C4%B1t.JPG)

##### Aktif Park Alanı Penceresi

![Image of Yaktocat](https://raw.githubusercontent.com/ozanercan/Otopark/master/resimler/Park%20Alan%20Bilgi.JPG)

### Lisans
MIT
