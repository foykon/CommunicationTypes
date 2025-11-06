# CommunicationTypes

Bu proje, farklı servisler arasındaki iletişim yöntemlerini örneklerle göstermeyi amaçlamaktadır.  
Her iletişim tipi kendi klasörü altında ayrı bir örnek olarak düzenlenmiştir.

## İçerik

### 1. BasicsOfPublisherAndConsumer
Temel düzeyde mesaj gönderimi ve tüketimini gösteren örnektir.  
Publisher basit bir mesaj yayınlar, Consumer bu mesajı alır ve işler.

### 2. HTTP
Klasik request-response yapısının .NET servisleri arasında kullanımını gösterir.  
`Service` ve `Service2` arasında doğrudan HTTP isteğiyle iletişim sağlanır.

### 3. gRPC
Servisler arası yüksek performanslı, tip güvenli iletişimi gRPC ile ele alır.  
`Service3` ve `Service4` örnekleri client–server mantığını içerir.

### 4. MassTransit
RabbitMQ tabanlı asenkron iletişim için **MassTransit** kütüphanesinin kullanımını gösterir.  
Alt klasörler:

- **Publisher / Consumer** → Temel publish–subscribe örneği.  
- **ReqResPatternMassTransit** → Request–Response deseniyle mesaj alışverişi.  
- **WorkerService.Publisher / Consumer** → Worker Service üzerinden MassTransit kullanımı.

### 5. MessageBroker
RabbitMQ’yu doğrudan (MassTransit olmadan) kullanarak mesaj alışverişi yapılır.  
`Service5` ve `Service6` arasında queue tabanlı iletişim örneği gösterilir.

### 6. Shared
Tüm örneklerde ortak kullanılan veri modelleri (`Messages`) burada bulunur.  
Ayrı solution’lar arasında paylaşılan **dll referansı** olarak kullanılabilir.

---

## Amaç
Bu çözüm, .NET tabanlı servisler arası iletişim yaklaşımlarını tek bir yerde karşılaştırmalı olarak incelemek için hazırlanmıştır.  
Her klasör bağımsız çalışır ve farklı senaryolarda tercih edilebilecek iletişim yöntemlerini örnekler.
