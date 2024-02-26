# Labs

We gaan een todo-applicatie maken.

1. Toon een lijstje van todos op de pagina. Een todo bestaat uit:

	- Id
	- Taak (wat er gedaan moet worden - string)
	- Wanneer het gedaan moet zijn
	- Of het gedaan is

2. Maak een formulier boven je tabel van todos om nieuwe todos mee toe te voegen. Met formuliervalidatie natuurlijk.
   Tijd over? Implementeer wat client-side validatie. Hint: unobtrusive

3. Maak een repository om de data access van je todo's in te vullen. In de nabije toekomst gaan we nog een repo toevoegen die daadwerkelijk naar de database gaat.

4. Sla je todo's op in een database. Ontsluit die database met Entity Framework Core.

5. Associeer je todo's met een categorie, zoals huiselijk onderhoud, werk of relatiemanagement. Dit is een aparte entiteit in je database. Gebruik een `<select>` in je form. Op zoek naar een uitdaging? Associeer je todo met één OF MEER categorieën.

6. Test één van je methoden op je Razor Page. Voel je vrij om te kiezen welke frameworks/libraries je hierbij inzet.

7. Maak een stuk middleware die alle 404-responses logt.

