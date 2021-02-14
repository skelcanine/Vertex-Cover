# Vertex Cover
 
Tepe Örtüsü (Vertex Cover) Problemi
	Bu matematiksel bir graf teori problemidir. G grafındaki ayrıtlar kümesindeki her ayrıtın bağlı olduğu herhangi bir uçtaki tepenin oluşturduğu kümeye tepe örtüsü denir. Bir graf birden fazla örtü tepesine sahip olabilir. Minimum tepe örtüsü problemi bilgisayar bilimlerinde NP-zor sınıftan klasik bir optimizasyon problemidir. Karar verme versiyonu, Karp’ın 21 NP-tam probleminden biri olup hesaplamalı karmaşıklık teorisinin klasik NP-tam bir problemi olmaktadır. Karar verme versiyonundan kasıt verilen G grafı ve k pozitif sayısı için en fazla k tepe içeren bir örtü tepesinin olup olmadığına karar vermektir. Örtü tepesi problemi sabit parametreli direkt bir problem olup sabit parametreli karmaşıklık teorisinin ana problemidir.
	G=(V,E) n-tepeli , q ayrıtlı
tepe kümesi V={v_1,v_2,…,v_n} 
ayrıt kümesi E={e_1=(v_1,v_2),e_2=(v_2,v_3),…,e_q} 
olan bir yönsüz graf olsun. V’nin öyle bir alt kümesi V' vardır ki uv∈E→u ∈ V'  ve v ∈ V'  yani E ayrıt kümesindeki her ayrıtın en az bir uç tepesi V'  kümesinde olmalıdır ve bu kümeye tepe örtüsü denir. Bu tepe kümesi G grafındaki tüm ayrıtları örter. 

Aşağıdaki örnekte kırmızı noktalı tepeler bir V' örtü kümesine örnektir.
![x](https://github.com/skelcanine/Vertex-Cover/blob/main/images/1.png?raw=true)
   
Minimum tepe örtüsü olabilecek en küçük V' alt tepe örtüsü kümesidir. Tepe örtüsünün minimum büyüklüğü τ=|V' | ile ifade edilir. Aşağıda bir önceki örneğin minimum tepe örtüsü kümesi kırmızı olarak işaretlenmiştir.
![x](https://github.com/skelcanine/Vertex-Cover/blob/main/images/2.png?raw=true)
 
V' kümesinin en küçük halini bulmak için olası tüm alt kümeleri kontrol etmek gerekir. Bir kümenin tüm alt kümelerinin sayısı 2^n olduğu için bu problem NP-tam sınıftan bir problemdir. Bu problemin kesin çözümünün büyük verilerde polinomiyal zamanda bulunamayacağı anlamına gelmektedir.
Özel birkaç durumda bu problem polinomiyal sürede çözülebilir.
	G grafı iki parçalı bir graf ise Kőnig teoremi sayesinde minimum tepe örtüsü problemi polinomiyal sürede çözülebilmektedir.
	Eğer G grafı bir ağaç ise minimum tepe örtüsü problemini, en uç yapraktaki tepeyi alıp bununla bağlantılı tüm tepeleri ve ayrıtları ağaçtan çıkarıp bu işlemi ayrıt kalmayana kadar yaparak polinomiyal bir zamanda çözebiliriz.

Program Hakkında
Program bir grafta örtü tepesini 2 algoritmik yöntem ile bulmaktadır. 
Grafı programa yüklemek için JSON formatında girdi dosyasını graf yükle butonuna tıklayıp seçebiliriz. Diğer bir yöntem olarak programın kendi özelliği olarak 16 tepeye kadar rastgele bir graf üretebiliriz. N sayısı girildiğinde 1...n e kadar tepeler adlandırılır. Daha sonra her bir tepe rastgele olarak diğer tepelere bağlanır. Sonuç olarak O(n^2) ile bir graf üretilmiş olur.

Grafı programa yüklendikten sonra veya rastgele bir graf oluşturduktan sonra algoritmalar uygulanabilir.
İlk algoritma (Kesin çözüm)
Bu algoritma olası tüm olasılıkları deneyerek en küçük kümeyi seçer. Örneğin n tepeli bir graf için minimum tepe örtüsünü bulan bu algoritma bütün alt kümeleri deneyerek en az elemanlı alt kümeyi seçer. Bu seçme işinin karmaşıklığı alt küme sayısından dolayı O(2^n) dir. Yani büyük girdiler için polinomiyal zamanda yanıt vermemektedir.

ALGORİTMA
Adım1: G grafının tepeler kümesinin boş alt kümesi hariç tüm kümeler, A adlı bir liste oluşturulup bu listeye eklenir.
Adım2: Oluşturulan A listesi, elemanlarının kardinalitesine göre küçükten büyüğe sıralanır.
Adım3: A listesindeki ilk eleman yani en düşük kardinaliteli eleman tepe kümesi olarak seçilir.
Adım4: G grafının E ayrıtlar tepesinin bir kopyası E' oluşturulur.
Adım5: Adım3’te seçilen tepe kümesinin her elemanı için Adım4’te oluşturulan E' ayrıtlar kümesinde bu elemanlara bağlı ayrıtlar E' ‘den çıkarılır.
Adım6: Eğer E' kümesinde eleman kalmadıysa Adım3’te seçilen tepe kümesi minimum tepe örtüsüne karşılık gelmektedir.
Adım7: Eğer E' kümesinde eleman kalmışsa Adım3’te seçilen küme tepe örtüsü değildir. A kümesinden Adım3’te seçilen tepe kümesi çıkartılarak Adım3’e tekrar gidilir.

İkinci algoritma (2-yaklaşık)

Bu algoritma kesin çözüm algoritmasına karşın kısa zamanda yanıt verebilmektedir. Karmaşıklığı n ayrıtlı bir graf için O(n) dir.
Fakat bu algoritmanın verdiği sonuç kesin çözümün maksimum 2 katı büyüklüğünde bir kümeyi sonuç olarak vermektedir. Yani verdiği sonuç kesin çözüm algoritmasından uzak bir sonuç verebilir.

Kanıt
A, aşağıdaki APPROX-VERTEX-COVER’ın 3.a satırının seçtiği ayrıtlar kümesini belirtsin. A’daki ayrıtları örtmek için herhangi bir tepe örtüsü-özellikle, bir C^* optimal örtüsü- A’daki her bir ayrıtın en az bir uç noktasını içermelidir. A’daki iki ayrıt aynı uç noktayı paylaşamaz; çünkü bir ayrıt 3.a satırında bir kere seçildiğinde onun uç noktalarına bitişik diğer bütün ayrıtlar 3.c satırında E^' kümesinden silinir. Dolayısıyla A’daki hiçbir iki ayrıt C^* kümesinden aynı tepeyle örtülemez. Ve bir optimal tepe örtüsünün boyutu üzerinde
|C^* |≥|A|
alt satırına sahip oluruz. Satır 3.a’nın her bir yürütülmesi uç noktalarının hiçbiri C’de olmayan bir ayrıtı seçer ve bu döndürülen tepe örtüsünün boyutu üzerinde bir üst sınır (aslında bir tam üst sınır) verir:
|C|=2|A|.
elde ettiğimiz denklemleri birleştirerek
|C|=2|A|
        ≤|C^* |
ifadesini elde ederiz ve böylece teorem kanıtlanır.∎
ALGORITMA
APPROX-VERTEX COVER(G)
	C=∅
	E^'=G.E
	while  E'≠∅
	(u,v),E^' kümesinin keyfi ayrıtı olsun
	C=C∪{u,v}
	E^' kümesinden u ya da v ye bitişik olan ayrıtlar çıkarılır
	return C

Algoritma Adımları
Adım1: Graftaki ayrıtların rastgele sıralamasından oluşan E kümesini oluştur.
Adım2: VC boş bir küme olsun. (Minimum örtü tepesi olacak)
Adım3: E kümesi boş değil ise bu kümeden rastgele bir kenar seçilir. Bu küme boş ise algoritma tamamlanmıştır.
Adım4: Adım3 te seçilen ayrıtın uçlarındaki tepeler u ve v olsun.
Adım5: Eğer Adım4’te bulunan u ve v tepelerinden herhangi birisi VC kümesinde bulunuyorsa. Adım3’te seçilen ayrıt E kümesinden çıkartılır. Adım3’e geri dönülür.

Adım6: Bu u ve v tepelerinin her ikisi de VC kümesinde değilse bu tepeler VC kümesine eklenir
Adım7: E kümesinden Adım4’te bulunan u ve v tepelere bitişik tüm ayrıtlar çıkarılır. Adım3’e geri dönülür.

Örneğin
Kesin algoritma için değerlerimiz şu şekilde olsun
"vertices":{"1":1,"3":2,"2":1,"4":3,"5":1},"edges":["1-3","2-4","3-4","4-5"]
Bize verdiği sonuç şu şekilde olacaktır
 
 ![x](https://github.com/skelcanine/Vertex-Cover/blob/main/images/3.png?raw=true)


Yaklaşık algoritma için değerlerimiz şu şekilde olsun
"vertices":{"1":2,"2":3,"4":5,"3":3,"5":4,"6":3,"7":3,"8":2,"9":2,"10":1},"edges":["1-2","1-4","2-3","2-4","3-4","3-5","4-5","4-6","5-6","5-7","6-7","7-8","8-9","9-10"]
Bize verdiği sonuç şu şekilde olacaktır
 ![x](https://github.com/skelcanine/Vertex-Cover/blob/main/images/4.png?raw=true)


