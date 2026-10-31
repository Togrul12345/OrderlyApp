Bu layihəyə ilk başlayarkən öncə database strukturunu qurmaqla başladım.Arxitekturanı isə Onion olaraq seçdim.Category entity-sini qurdum,onu restoranlarla və productlarla əlaqələndirdim.
Category entity-si 
One-to-many əlaqəsi burda mövcuddur.Məntiqən bir category bir neçə restorana aid ola bilər və bir category-də bir neçə product mövcud ola bilər.
Order entity-si 
Sonra Order tablesini qurdum.burda da one-to many əlaqə var.Bir orderin bir neçə orderitem-ı ola bilər.Bir order yalnızca bir restorana aid ola bilər və bir orderin yalnızca bir orderstatusu ola bilər.Bunun əksi də doğrudur,yəni bir orderstatusda bir neçə order,bir restoranda bir neçə order ola bilər.Həmçinin burda calculatetotalprice və ChangeStatus biznes məntiqlərini də bu entitynin içərisində yazdım
Orderİtem entity-si
Orderİtem entity-sini isə Order və Product-la one-to many olaraq əlaqələndirdim.Yəni bir order-də bir neçə orderitem eləcədə bir producta aid bir neçə Orderİtems ola bilər
OrderStatus entity-si
Bu entity isə orderlərin statusunu saxlamaq üçün nəzərdə tutulub.Lakin kod tərəfində OrderStatusları mənimsədərkən OrderStatusEnum istifadə etmişəm.Enum istifadə etməyimin səbəbi odur ki Statusların konkret olaraq bir neçə dəyərləri olur,onlar artmır və ya azalmır.Əgər OrderStatus entitysi davamlı olaraq artsaydı və ya azalsaydı bunu kod tərəfində enumla mənimsətmək düzgün yol olmazdı.Çunki hər yaranan status üçün yeni enum tipi yaratmaq lazım olardı.Eləcədə OrderStatusla order arasında one-to-many əlaqə var.Yəni bir orderStatusda bir neçə order ola bilər.Entity-də Orders collectionunu Hashset-ə mənimsətməyimin səbəbi odur ki təkrarlanmaların qarşısı alınsın.Bundan əlavə əgər contains metodu işlədəcəyəmsə bu hashsetdə daha sürətli olur.
Product entity-si
Bu entity-də də many-to-many əlaqə var.Ama mən kodda yanlışlıqla Product və Restourant arasında əlaqəni one-to many olaraq vermışəm.Bu many-to-many olmalı idi.Həmçinin bir product sadəcə 1 category-ə aid ola bilər.1 Product-a aid bir neçə orderitems ola bilər
Restourant entity-si
Restourant entity-sində də one-to-many əlaqəsi var.Bir restoranda bir neçə Categories,Products və Orders ola bilər.
RestourantCategory
Sonuncu olaraq isə bu entity-ni modellədim.
bu many-to many-a aid tabledir.Yəni bir restorana bir neçə category aid ola bilər,bir Category isə bir neçə restorana aid ola bilər.

Bu entitylərin fluent apidə konfiqurasiyasını yazarkən Category-ləri,OrderStatusları və RestourantCategory-ə aid entityləri seeddata olaraq doldurdum.Daha sonra migrate edərək databaseyə save etdim.

Bu yaratdığım entity-lərin biznes məntiqini mediatorla yazdım.Ama bunun necə yazılması biznesin tələblərindən asılı olaraq dəyişə bilər.Hər entity-nin servicesi üçün uyğun metodları repositorylər içində yazdım və bu metodları handlerlər daxilində çağıraraq prosesləri icra etdim.Həmçinin fluent validation tətbiq etdim.Fluent validationun mediator səviyyəsində işlətmək üçün validationBehaviour sinfi əlavə etdim.

Layihəni işə salmaq üçün localda tableləri database-yə migrate etmək daha sonra, run etmək lazımdır.Layihədə nisbətən çoxlu funksionallıq olduğundan 4 saat sərf etdim.Ama daha da tez bitə bilərdi.Daha çox vaxtım olsaydı etmək istədiyim şeylər:
1.Automapper implementasiyası
2.Global exception handler
3.Əlavə olaraq entity-ləri create və ya update etdiyim zaman Muəyyən entity-lərin id-sinə görə yoxlama edə bilərdim.yəni bəzi entityləri create etdiyimiz zaman foreign keyə bərabər olan ədəd databazedə olmaya bilər
