# Development-of-a-Real-Time-Strategy-Game-in-Unity
Absolventská práce - Jan Radoňský

Přehled hlavních skriptů
BuildManager.cs

BuildManager je centrální skript pro systém stavění budov. Řídí celý proces stavění od výběru budovy v UI až po její umístění na mapě. Vytváří ghost preview budovy, přichytává ji na tilemapu a ověřuje, zda je možné budovu na daném místě postavit. Po úspěšném umístění registruje obsazené buňky mapy, aby zabránil překrývání dalších staveb.

BuildButton.cs

BuildButton propojuje uživatelské rozhraní se systémem stavění. Každé tlačítko v UI reprezentuje jednu budovu a obsahuje odkaz na její BuildingData. Po kliknutí na tlačítko je spuštěn proces stavění v BuildManageru. Tento přístup umožňuje snadné přidávání nových budov bez úprav kódu.

BuildingFootprint.cs

BuildingFootprint definuje prostor, který budova zabírá na tilemapě. Skript pracuje s velikostí budovy v počtu buněk a umožňuje nastavit dodatečný ochranný okraj kolem budovy. Tento okraj slouží k vytvoření zakázané zóny, aby nebylo možné stavět budovy příliš blízko u sebe.

BuildOccupancy.cs

BuildOccupancy spravuje informace o obsazených buňkách mapy. Ukládá, které části tilemapy jsou blokované postavenými budovami, a umožňuje rychlou kontrolu, zda je možné na dané pozici stavět. Tento systém tvoří základ pro budoucí pathfinding jednotek.

TerrainChecker.cs

TerrainChecker slouží k rozpoznávání typu terénu na základě tilemapy. Podle pozice ve světě určuje, zda se jedná o pevninu nebo vodu. Tento skript je využíván při stavění budov i při omezení pohybu jednotlivých typů jednotek.

SelectionManager.cs

SelectionManager zajišťuje výběr jednotek pomocí myši. Umožňuje výběr jedné jednotky kliknutím i hromadný výběr více jednotek pomocí taženého obdélníku. Zároveň zpracovává příkazy k pohybu jednotek a rozděluje cílové pozice do jednoduché formace.

SelectableUnit.cs

SelectableUnit reprezentuje jednotlivé jednotky ve hře. Skript přijímá cílové pozice a stará se o samotný pohyb jednotky. Obsahuje jednoduchý systém vyhýbání se překážkám a ostatním jednotkám, aby nedocházelo ke kolizím a shlukování.

BuildingData.cs

BuildingData je ScriptableObject sloužící jako datový kontejner pro budovy. Obsahuje informace o prefabu budovy a dalších parametrech. Tento přístup umožňuje přidávat nové budovy do hry bez zásahů do herní logiky.

UnitData.cs (plánováno)

UnitData je obdobný datový objekt jako BuildingData, ale určený pro jednotky. Je připraven pro budoucí rozšíření hry o kasárna, továrny a multiplayerovou synchronizaci.
