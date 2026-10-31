Bu layihəyə ilk başlayarkən öncə database strukturunu qurmaqla başladım. Arxitekturanı isə Onion olaraq seçdim.

1. Entity Strukturu və Əlaqələr
Category Entity-si:
One-to-many əlaqəsi burda mövcuddur. Məntiqən bir category bir neçə restorana aid ola bilər və bir category-də bir neçə product mövcud ola bilər.

Order Entity-si:
Sonra Order tablesini qurdum. Burda da one-to-many əlaqə var: 

Bir orderin bir neçə orderitem-ı ola bilər.

Bir order yalnızca bir restorana aid ola bilər və bir orderin yalnızca bir orderstatusu ola bilər.

Bunun əksi də doğrudur, yəni bir orderstatusda bir neçə order, bir restoranda bir neçə order ola bilər. Həmçinin burda CalculateTotalPrice və ChangeStatus biznes məntiqlərini də bu entity-nin içərisində yazdım.

OrderItem Entity-si:
OrderItem entity-sini isə Order və Product-la one-to-many olaraq əlaqələndirdim. Yəni bir order-də bir neçə orderitem, eləcə də bir product-a aid bir neçə OrderItems ola bilər.

OrderStatus Entity-si:
Bu entity isə orderlərin statusunu saxlamaq üçün nəzərdə tutulub. Lakin kod tərəfində OrderStatus-ları mənimsədərkən OrderStatusEnum istifadə etmişəm. Enum istifadə etməyimin səbəbi odur ki, statusların konkret olaraq bir neçə dəyərləri olur, onlar artmır və ya azalmır.

Əgər OrderStatus entity-si davamlı olaraq artsaydı və ya azalsaydı, bunu kod tərəfində enum-la mənimsətmək düzgün yol olmazdı. Çünki hər yaranan status üçün yeni enum tipi yaratmaq lazım olardı.

Eləcə də OrderStatus-la order arasında one-to-many əlaqə var (yəni bir orderStatus-da bir neçə order ola bilər). Entity-də Orders kolleksiyasını HashSet-ə mənimsətməyimin səbəbi odur ki, təkrarlanmaların qarşısı alınsın. Bundan əlavə, əgər Contains metodu işlədəcəyəmsə, bu, HashSet-də daha sürətli olur.

Product Entity-si:
Bu entity-də də many-to-many əlaqə var. Amma mən kodda yanlışlıqla Product və Restourant arasında əlaqəni one-to-many olaraq vermişəm — bu, many-to-many olmalı idi. Həmçinin bir product sadəcə 1 category-ə aid ola bilər, 1 product-a aid bir neçə orderitems ola bilər.

Restourant Entity-si:
Restourant entity-sində də one-to-many əlaqəsi var. Bir restoranda bir neçə Categories, Products və Orders ola bilər.

RestourantCategory Entity-si:
Sonuncu olaraq isə bu entity-ni modellədim. Bu, many-to-many-a aid table-dir. Yəni bir restorana bir neçə category aid ola bilər, bir Category isə bir neçə restorana aid ola bilər.

2. Konfiqurasiya və Biznes Məntiqi
Bu entity-lərin Fluent API-də konfiqurasiyasını yazarkən Category-ləri, OrderStatus-ları və RestourantCategory-ə aid entity-ləri Seed Data olaraq doldurdum. Daha sonra migrate edərək database-yə save etdim.

Bu yaratdığım entity-lərin biznes məntiqini Mediator ilə yazdım. Amma bunun necə yazılması biznesin tələblərindən asılı olaraq dəyişə bilər. Hər entity-nin service-i üçün uyğun metodları repository-lər içində yazdım və bu metodları handler-lər daxilində çağıraraq prosesləri icra etdim. Həmçinin Fluent Validation tətbiq etdim. Fluent Validation-u mediator səviyyəsində işlətmək üçün ValidationBehaviour sinfi əlavə etdim.

3. İcra və Gələcək Planlar
Layihəni işə salmaq üçün local-da table-ləri database-yə migrate etmək, daha sonra run etmək lazımdır. Layihədə nisbətən çoxlu funksionallıq olduğundan 4 saat sərf etdim, amma daha da tez bitə bilərdi.

Daha çox vaxtım olsaydı etmək istədiyim şeylər:

1.AutoMapper implementasiyası

2.Global Exception Handler

3.Əlavə olaraq entity-ləri create və ya update etdiyim zaman müəyyən entity-lərin ID-sinə görə yoxlama etmək (yəni bəzi entity-ləri create etdiyimiz zaman foreign key-ə bərabər olan ədəd database-də olmaya bilər).
