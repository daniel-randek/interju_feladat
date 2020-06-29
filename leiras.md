A feladat megoldásához a https://restcountries.eu/ API-t használtam. Azért erre esett a választásom, mert az első találatok között volt, elég ország és országtulajdonság volt benne és könnyen használhatónak tűnt.
Végül amellett döntöttem, hogy egyszerűen a saját gépemre telepítettem, mert az, hogy ha vm-re telepíteném, azt jelentené, hogy könnyebben hordozható lesz, nem befolyásol véletlenül sem más programokat a gépen, nem volt elég ahhoz, hogy vm-re telepítsem.
A PostgreSQL mellett döntöttem, mert ez volt a feladat leírásban és bár eddig még nem foglalkoztam vele, használata nem tűnt annyira nehéznek, hogy csak ezért mást használjak.

A megoldás alapvetően 4 class-ból épül fel, a Country.cs, amiben az országok adatmodellje található, a WebApi.cs, ami az adatok letöltéséért és deszerializációjáért felelős, a CountryDBContext.cs ami az adatbázis feltöltéséért felelős és a Program.cs ahonnan futtatható a program.
Használatához a Program.cs-ben kell átírni a CountryDBContext connection stringjét.
