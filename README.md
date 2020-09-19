# Otopark Otomasyonu

## Proje Hakkında
Bu yazılım Otopark işletmeleri için Katmanlı Mimari kullanılarak C# diliyle yazılmıştır. Herhangi bir somut objeye gerek duymadan sadece bilgisayar üzerinden çalışmaktadır. Birden fazla cihazdan(bilgisayar) bir otoparkı yönetebilmek için MySQL veritabanı tercih edilmiştir.

## Kurulum Dosyaları
MySQL destekli SQL dosyası DataAccess>Concrete>MySQL klasörü altında Backup.sql olarak adlandırılmıştır.
* [Xampp](https://www.apachefriends.org/tr/index.html) - MySQL veritabanını kullanabilmek için gereken program.
* [Wampp](https://www.wampserver.com/en/) - Xampp'a alternatif program.
* [SQL Dosyası](https://github.com/ozanercan/Otoparkv2/blob/master/DataAccess/Concrete/MySQL/Backup.sql) - Veritabanı alanları için sql dosyası.


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

![Image of Yaktocat](https://i.imgyukle.com/2020/09/19/xwRz7o.jpg)

##### Ana Sayfa

![Image of Yaktocat](https://i.imgyukle.com/2020/09/19/xwRWr1.jpg)

##### Park Alanı Düzenleme Safası

![Image of Yaktocat](https://i.imgyukle.com/2020/09/19/xwRlEG.jpg)

##### Park Alanına Araç Ekleme Penceresi

![Image of Yaktocat](https://i.imgyukle.com/2020/09/19/xwR2bA.jpg)

##### Aktif Park Alanı Penceresi

![Image of Yaktocat](https://i.imgyukle.com/2020/09/19/xwRK5U.jpg)

### Lisans
MIT
