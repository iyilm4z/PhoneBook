
#PhoneBook

##Açıklama
Solution'daki Admin projesi aslında MVC'nin area feature'dır. Bu nedenle projenin directory yolu Web projesi içindeki Areas(gizli) klasörüdür. Bir area'yı tamammen farklı bir proje olarak kullanmak hem development esnasında kolaylıklar sağlamakta hemde modular architecture için best practice'dır.
Developlemt esnasındaki kolaylığa alt klasörler arasında kaybolmamayı gösterebiliriz. Modular architecture için faydası ise tamamen farklı bir proje olan Admin, herhangi bir projede host edilerek kullanıma hazır hale getirilebilir. Admin projesinin build adresinin host edildiği projenin bin'i olduğuna dikkat etmmek gerekir. Nadiren çıkan problemlerde Admin projesinin ayrı build olması birçok problemi çözecektir.

##Not 
Global.asax'ın Application_Start event method'u üzerindn DB'ye seed edilen datalar ile DB init edilmektedir.

##Kullanılan Frameworklar

1-) MVC 5 
2-) EntityFramework 6 (ORM for DAL)
3-) Autofac (DI)
4-) Automapper (Mapping between UI, BLL and DAL models)
5-) FluentValidation (Validating UI model inputs)
