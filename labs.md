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

8. Ik wil graag verschillende views voor het tonen van de todo's:
   * Een view die alle todo's van een bepaalde categorie toont
   * Een view die de 3 meest urgente todo's toont
   * Een view die alle afgeronde todo's toont

   Hergebruik zoveel mogelijk HTML. En maak het ook gerust een beetje mooier met een navigatiegebied zodat men makkelijker kan schakelen tussen de verschillende pagina's.
   
9. Maak een REST API voor het ophalen en toevoegen van todo's. Maak ook een endpoint voor het ophalen van de categorieën. En eentje voor het afvinken van todo's.

   Deze APIs worden in de volgende oefening aangesproken door onze Blazorfrontend.

10. Gebruik DTOs binnen je applicatie om over-POSTing te voorkomen en mapping in het algemeen.

11. Onze todo-app wordt gebruikt door hele families! Sla bij elke todo op welk familielid de todo heeft toegevoegd. Men mag dus enkel todo's toevoegen als men is ingelogd. In de lijst van todo's, toon de naam van het familielid die de todo heeft toegevoegd.

12. Maak een Blazor WebAssembly-project die de todo's ophaalt en weergeeft van de server.

13. Als een ander familielid todo's toevoegt, zouden deze instant zichtbaar moeten zijn voor alle andere familieleden. Gebruik SignalR om je app realtime te maken.
   
   Dit mag je zowel doorvoeren bij je Blazor-app met C# OF bij je Razor Page middels JavaScript.
