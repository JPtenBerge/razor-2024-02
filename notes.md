# Notes

## Paradigmas der web development

- SPA - Single Page Application
  - alles wordt client-side gerenderd
  - server stuurt HTML-templates op
  - server stuurt los data op (JSON)
  - Vue.js React Angular Svelte Fresh Solid Qwik Blazor
  - hip
- SSR - server-side rendering
  - hier rendert de server jouw initiele pagina
  - daarna gaat het weer over in een SPA
  - complementair aan de SPA
  - Next.js/Remix (React) Nuxt.js (Vue) @angular/ssr Deno SolidStart QwikCity ASP.NET Core (Blazor)
  - hip
  - het op de achtergrond opsturen van interactieve dingen (.js) - HYDRATION
    - Qwik - resumability
- SSG - static site generation
  - productcatalogi
  - /product-details/14
  - genereert heeeeel veel .html-bestanden
  - buildstep
  - bol.com amazon.com documentatiewebsites
  - Astro 11ty HUGO  @angular/ssr Next.js
  - hip
- MPA - Multi Page Application
  - ASP.NET Core MVC
  - ASP.NET Core Razor Pages
  - PHP
  - Java Spring
  - niet hip.

## HTTP-berichten en -statuscodessen

* 2xx - SUCCESS
  - 200 OK
  - 201 Created
  - 204 No Content  (DELETE)
* 3xx - REDIRECTS
  - 301/302  temporary/permanent
* 4xx - CLIENT ERROR
  - 400 Bad Request
  - 401 Unauthorized
  - 403 Forbidden
  - 404 Not Found
  - 405 Method Not Allowed  (POST => ...geen POST ondersteunt)
  - 415 Mediatype not supported  (XML => ...die geen XML kan parsen)
  - 418 I'm a teapot
* 5xx - SERVER ERROR
  - 500 Internal Server Error
  - 502 Bad Gateway

anti forgery token?
CSRF/XSRF 
Cross Site Request Forgery

```html
<form action="https://goeddomein.nl/process">
```

`IActionResult`
- redirect to page result
- redirect result
- json result
- xml result
- content result
- file result
- error result

Qua HTTP-berichten:
GET:
- heeft GEEN body
- DELETE ook niet
- alles in url, maximaal 2048 karakters

POST:
- heeft een body
  - PUT en PATCH ook

Voorbeeld: loginformulier via GET of POST versturen?
- informatie staat in de body, niet in url
  - wachtwoord in url is niet tof. iedereen die meekijkt op je scherm kan wachtwoord zien
  - proxy logs

## Formuliervalidatie

validatie implementeren:
- out-of-the-box: data annotation validator 
- lib: FluentValidation. voordelen:
  - rijker: conditionele validatie
  - meer validaties out-of-the-box
  - unittesten

## EF Core

- ORM: Object-Relational Mapper
- onderdeel van ADO.NET: data access in .NET
  - ActiveX Data Objects

Geschiedenis van manieren om naar je db te gaan:

1. rechtstreeks hardcoded SQL - 2001
  ```cs
  using(var connection = new SqlConnection("...")) 
  {
    connection.Open();
    using(var command = new SqlCommand())
    {
      command.CommandType = CommandType.Text;
      command.CommandText = "SELECT * FROM klant;"; // SQL injection
      using(var reader = command.ExecuteReader())
      {
        while(reader.Next())
        {
          reader["name"]
          int.Parse(reader["age"])
        }
      }
    }
  }
  ```
2. Datasets/datatables - 2005
  - visueel designertje voor je datamodel
  - queries instellen bij iedere table
  - niet heel flexibel
  - voelde heel database-ig aan:
    ```cs
    foreach (var customer of CustomerTable.Fill())
    {
      customer.AddressRow
    }
    ```
3. LINQ to SQL - 2008
  - lijkt heel erg op wat EF Core vandaag de dag
  - werkte enkel op SQL Server
  - geen ondersteuning voor n:m-relaties
4. LINQ to Entities (Entity Framework 6.4 momenteel) - 2009
  - database first
  - model first
  - code first
5. Entity Framework Core - .NET Core - 2016
  - code first

Packages:
* `Microsoft.EntityFrameworkCore` - abstractie  DbContext
* `Microsoft.EntityFrameworkCore.SqlServer` - queries at runtime vertalen naar SQL Server
* `Microsoft.EntityFrameworkCore.Tools` - Visual Studio package manager console commando's: `Update-Database` `Add-Migration`
  - heeft een dependency op die hieronder
* `Microsoft.EntityFrameworkCore.Design` - als je `dotnet ef migrations` wil kunnen doen

```sh
dotnet ef migrations add Naam
dotnet ef migrations script
dotnet ef database update
dotnet ef database revert
```

## Repositories

- meeeeeeeeeeestal DB
- kan ook voor frontend-DAL: frontend => backend
- vooral fijn als ze waarde toevoegen, bijv. queries die *altijd* bepaalde clausules in zich moeten hebben:
  ```cs
  context.Customers.Where(x => !x.IsInactive)
  ```

Unit of work gaat vaak hand-in-hand met repositories en bevatten vaak repository-overstijgende persistentielogica.

## Dependency injection

Geen `new`, maar dat een ander stuk code laten doen: dependency injection.

- 1 keer een instantie en die injecteer je wanneer je 'm nodig hebt
- het is een vorm van Inversion of Control

3 lifetimes in ASP.NET Core:

```cs
builder.Service.AddTransient<..., ...>() // altijd een nieuwe
builder.Service.AddScoped<..., ...>() // per request een nieuwe
builder.Service.AddSingleton<..., ...>() // 1 instance to rule them all
```

JP prefereert `.AddTransient()` ivm side effects.


## Unittesten

Testsoorten (zie ook testpiramide):

* unittesten
  - heel snel
  - heel klein stukje code
* integration testing
  - niet zo heel snel
  - integratie met database
  - integratie met API
  - integratie tussen componenten
  - HTML renderen
  - code aan het aanspreken
* UI testing / end-to-end testing
  - heel langzaam
  - browser aansturen
  - Playwright (Microsoft)
* manual testing
- zeer langzaam
- zeer duur

Wat is een unit?
* `methodA()` roept `methodB()` in zelfde class aan?
* `methodA()` roept `Math.random()` uit het .NET Framework aan?
* `methodA()` roept een repository aan?
* `methodA()` gaat interactie aan met een database?
* `methodA()` doet een API-call naar Azure?

zou je private moeten testen?
- ja, maar niet rechtstreeks. via de public API
- soms wel rechtstreeks als de situatie zich daarvoor leent. het is niet verboden.
  - met reflection
  - maar liever niet
- zie ook black box vs white box testing

```cs
var methods = sut.GetType().GetMethods(BindingFlags.Instance & BindingFlags.NonPublic);
var method = methods.Single(x => x.Name == "GetData");
method.Invoke(obj);
```

testframeworks:
- MSTest `[TestMethod]`  `Assert.AreEqual()`   <=== guidance framework
- NUnit `[Test]`
- xUnit `[Fact]`/`[Theory]`  `Assert.Equals()`

Vroeger wel relevante verschillen, maar zijn behoorlijk naar elkaar toe gegroeid. [Data-driven tests](https://learn.microsoft.com/en-us/visualstudio/test/how-to-create-a-data-driven-unit-test?view=vs-2022) waren vroeger vooral een dingetje bij MSTest:

```cs
[DataSource(@"Provider=Microsoft.SqlServerCe.Client.4.0; Data Source=C:\Data\MathsData.sdf;", "Numbers")]
```
Maar tegenwoordig hebben ze gewoon `[DataRow()]`

```cs
// data-driven/parameterized test: verschillende inputs
// 4 8
// -4 8
// -4 -8
public int Add(int x, int y)
{

}
```

Mockframeworks
- Moq <== guidance framework
- NSubstitute
- FakeItEasy <== guidance framework
- EasyMock

