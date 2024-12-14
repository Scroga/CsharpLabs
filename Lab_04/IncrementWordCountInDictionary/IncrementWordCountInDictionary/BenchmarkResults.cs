----------------------------------------------------------------------------------------------
| Scenar 1																		             |
|--------------------------------------------------------------------------------------------|
| Method                   | Mean        | Error     | StdDev    | Rank | Gen0   | Allocated |
|------------------------- |------------:|----------:|----------:|-----:|-------:|----------:|
| RunIncrementWordCount_V3 |    49.17 ns |  1.010 ns |  1.630 ns |    1 | 0.0258 |     216 B |
| RunIncrementWordCount_V2 |    55.03 ns |  1.089 ns |  0.909 ns |    2 | 0.0258 |     216 B |
| RunIncrementWordCount_V1 | 5,807.93 ns | 15.086 ns | 13.373 ns |    3 | 0.1068 |     912 B |
----------------------------------------------------------------------------------------------

Predpokladal jsem, ze reseni s IncrementWordCount_V1 bude pomalejsi, protoze (jak bylo zmineno na cviceni)
zachytavani vyjimek v programu je docela pomale. Take myslel jsem si, ze nejjednodussi reseni
(IncrementWordCount_V2) bude nejrychlejsi.

Myslim, ze reseni cislo 3 je rychlejsi, protoze pouzitim funkce TryGetValue() provadime kontrolu klice a ziskavame
hodnotu v jednem kroku, zatimco v reseni cislo 2 se klic hleda dvakrat, jednou behem ContainKey() a podruhe
pri prirazovani wordToCountDictionary[word] = 1. Proto je pocet operaci v reseni cislo 3 je o neco nizsi.


-----------------------------------------------------------------------------------
| Scenar 2																		  |
|---------------------------------------------------------------------------------|
| Method           | Mean       | Error    | StdDev   | Rank | Gen0   | Allocated |
|----------------- |-----------:|---------:|---------:|-----:|-------:|----------:|
| SimpleDictionary |   991.8 ns |  7.43 ns |  6.59 ns |    1 | 0.1373 |    1152 B |
| SortedList       | 2,889.5 ns | 47.09 ns | 52.34 ns |    2 | 0.0687 |     592 B |
| SortedDictionary | 3,472.4 ns | 45.60 ns | 57.67 ns |    3 | 0.0916 |     776 B |
-----------------------------------------------------------------------------------

Zpocatku me prekvapilo, ze reseni s prostym slovnikem bude nejrychlejsi, ale po zamysleni nad 
vysledkem mi to prislo samozrejme.

Myslim, ze reseni s prostym slovnikem je rychlejsi proto, ze "chytre datove struktury", jako 
SortedDictionary a SortedList, se snazi vzdy udrzovat data serazena, tedy pri kazdem pridani noveho prvku 
data znovu seradi, zatimco v reseni s prostym slovnikem setridime data jednou, a to prave pred vypisovanim 
vysledku, z cehoz vyplyva, ze pocet operaci v reseni s prostym slovnikem bude mensi nez v resenich s ostatnimi 
datovymi struktury. 




Zaver: SortedList nebo SortedDictionary by se mel pouzivat pouze tehdy, kdyz potrebujeme mit data setridena 
v prubehu celeho programu, coz v uloze Cetnost slov neni nutne. Tedy reseni s prostym slovnikem bude vhodne 
pro tuto ulohu, protoze potrebujeme setridit jen vysledek. O privnim scenari muzu rici, ze reseni cislo 3
by se nemelo byt vubec pouzito, zatimco reseni cislo 1 a 2 se lisi v rychlosti, ale neni to zavazny rozdil,
obe mohou byt pouzity.