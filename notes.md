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
  - 422 Unprocessable entity - de syntax van het request was prima, maar er is een logisch probleem opgetreden waardoor het niet verder verwerkt kan worden
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

Kijk eens naar de [SQL Server Profiler](https://learn.microsoft.com/en-us/sql/tools/sql-server-profiler/sql-server-profiler?view=sql-server-ver16):
- Hoe complexer de query, hoe lastiger het vertalen wordt en soms onjuist gaat
  - Dit kan ook per EF Core-adapter verschillen, dus als je morgen met een EF Core MySQL-adapter te maken hebt in plaats van eentje voor SQL Server
- Om de performance te verbeteren door bijv. de query te gebruiken om een query execution plan naast te houden

Profiler komt standaard mee met SQL Server Developer Edition.

Wat queries betreft, positie van `.ToList()`/`.ToArray()`/`.ForEach()` maakt een hoop uit:

```cs
context.Customers.ToList().Where(x => x.Name.StartsWith("J"));
context.Customers.Where(x => x.Name.StartsWith("J")).ToList();
```

Bij de een doet de database het filteren, bij de ander worden er mogelijk miljarden records in-memory geladen en doet de .NET runtime het filteren.

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

Laatste dingetje: [FluentAssertions](https://fluentassertions.com/) is erg tof zodra je complexere assertions krijgt (inhoudelijke objecten of collecties bijv.)

## Middleware

- zit een beetje tussen client en server
- verwerkingsstap bij het verwerken van een request
- Pipeline pattern

```cs
// meeste middlewares beginnen met .Use
app.UseLogger();
app.UseStaticFiles();
app.UseDeveloperExceptionPage();
app.UseAuthentication(); // bearer token   cookie
app.UseAuthorization();
app.UseHsts();
app.UseHttpsRedirection();
app.UseAntiForgeryToken();
app.UseStatusCodePages();

// sommige beginnen met .Map
app.MapRazorPages();
app.MapControllers();
app.MapHub<>();
```

Is meestal chainable:

```cs
app.UseLogger()
  .UseStaticFiles()
  .UseDeveloperExceptionPage()
  .UseAuthentication() // bearer token   cookie
  .UseAuthorization()
  .UseHsts()
  .UseHttpsRedirection()
  .UseAntiForgeryToken()
  .UseStatusCodePages();
```

## Data in view

Arno wilde het lijstje van categorieen uit zijn database in zijn _Layout gebruiken. Mogelijkheden?

- de data doorgeven via `ViewData`
- een custom view component maken (denk partial view, maar dan met logica)
- `@RenderSection()`, maar dan moeten alle pagina's die section providen. Niet heel chic.
- eigen tag helper `<div todo-categories></div>`
- Met `@inject` de repo injecteren en gewoon de data ophalen. Is niet megachic om dat vanaf de view allerlei database-operaties uit te zetten, maar het werkt wel.

## REST: REpresentational State Transfer

- JSON/XML/...
- HTTP-header: Accept: application/json, application/xml, text/html, outlook/v-card
  - JSON
- bedacht in 2000 door Roy Fielding
- in theorie los van HTTP, maar praktisch gaat er nagenoeg altijd mee hand in hand

REST draait om resources en HTTP verbs gebruiken om die resources te benaderen. En niet meer het web misbruiken met dit soort url's:

```sh
GET index.php?action=delete_customer&id=14
```

verbs:
- GET ophalen
- POST aanmaken         /wijzigen
- PUT wijzigen          /toevoegen
- DELETE verwijderen
- PATCH deel wijzigen

```sh
# 3x POST = 3 nieuwe auto's in het systeem
POST api/car     { make: '...', model: '...' }
POST api/car     { make: '...', model: '...' }
POST api/car     { make: '...', model: '...' }

# 3x PUT  = 1 nieuwe auto in het systeem
PUT  api/car/15  { make: '...', model: '...' }
PUT  api/car/15  { make: '...', model: '...' }
PUT  api/car/15  { make: '...', model: '...' }
```

Ook wel de idempotency van een request

API testing tools:
- Postman  druk duurde lang voor dark mode er was
- Insomnia  dark mode++  
- Bruno
- curl
- Hoppscotch
- VS Code:
  - Thunder Client
  - REST Client - .http .rest
- Swagger UI

### Object cycles fixen

- DTOs
- dingen op null zetten/laten
- configuratie `ReferenceHandler.IgnoreCycles`
- configuratie `ReferenceHandler.Preserve`
- `[JsonIgnore]`

### REST maturity levels - RESTful

Zie ook [hier](https://martinfowler.com/articles/richardsonMaturityModel.html)

Meeste APIs doen level 2. Level 3 is Hypermedia As The Engine Of Application State

```json
// /api/product
[
  {
    "description": "...",
    "price": 8.99,
    "links": [
      { "details": "https://mijndomein.nl/api/product/19" },
      { "buy": "https://mijndomein.nl/api/product/19/buy" },
      { "inventory": "https://mijndomein.nl/api/product/19/inventory" },
      { "related": "https://mijndomein.nl/api/product/19/related" },
    ]
  }
]
```

Vooral waardevol voor public-facing APIs, om je gebruikers te begeleiden bij het gebruiken van je API.

Wat gebruiksvriendelijkheid van de API betreft, context aanbrengen met je URLs is ook vaak fijn:

* `api/product/18/orders/8985874`
* `api/customer/JP/aankopen`

### System.Text.Json

De default serializer sinds ASP.NET Core 3.0. 

* ASP.NET Core 1.0  Newtonsoft.Json
* ASP.NET Core 2.0  Newtonsoft.Json
* ASP.NET Core 3.0  System.Text.Json
* ASP.NET Core 5.0  System.Text.Json
* ASP.NET Core 6.0  System.Text.Json
* ASP.NET Core 7.0  System.Text.Json
* ASP.NET Core 8.0  System.Text.Json

Newtonsoft.Json wordt nog steeds wel ondersteund:

```cs
builder.Services.AddControllers().AddNewtonsoftJson();
```

System.Text.Json
- minder scenarios ondersteuning
- performance daarmee wel beter

### DTOs - Data Transfer Objects

Wat ze oplossen:

- recursie in data
- geen overbodige data oversturen
- overPOSTing/underPOSTing
- specifieke validatie

`BlablaPutRequestMessage` en `BlablaPutResponseMessage`

```sh
client <========> controller/page => service => service => repo => db
                            => validators
    <--- DTO --------------------------->
```

Vaak moet je DTOs mappen naar entities die in de rest van je architectuur gebruikt worden.

- AutoMapper (gebruikt reflection, hernoemen van properties leidt tot `null` in JSON-output)
- Mapster (gebruikt reflection, hernoemen van properties leidt tot `null` in JSON-output)
- Mapperly "creates the mapping code at build time", dat lijkt een prima optie te zijn!
- Handmatig implementeren. ChatGPT/Copilot maken dit werk minder saai.

### Overige REST-zaken

**Swagger/Open API**
- Swagger doc - documentatie van je API
- Swagger UI - API testing tool
- OpenAPi - de open standaard van API-documentatie zodat er eventueel wat concurrentie in dit landschap kan plaatsvinden.

**Versionering van REST APIs**
- api/v1/products
- api/products?v=1
- custom HTTP header `X-API-VERSION: 1`

Versionering is vooral lastig in verband met alle achterliggende code. Je models, je services, je repositories, je validators, je database. Je wil de APIs zo gescheiden mogelijk van elkaar hebben, maar bij de achterliggende verwerking wil je zo min mogelijke code-duplicatie i.v.m. bugfixing, herbruikbaarheid, etc.

**Alternatieven REST**

* GraphQL: denk aan 100 verschillende soorten clients die allemaal verschillende behoeften hebben wat betreft welke/hoeveel data er wordt teruggestuurd. GraphQL helpt dan dat de client kan aangeven wat hij wil hebben en dat stuurt GraphQL dan op.
* gRPC - gRPC Remote Procedure Call
  - protobuf: binair formaat
  - nuttig bij heule grote hoeveelheden data: begint vanaf 10MB
  - ook nuttig als serializatie van je data heel lang duurt
  - veel meer uitdrukkingsvrijheid t.o.v. REST
  - bidirectioneel streamen mogelijk, REST is request-response
  - Netflix gebruikt het veel
  - gebaseerd op HTTP/2, maar wel door dat te cherrypicken - het zijn geen standaard browserberichtjes
  - complexer om op te zetten vergeleken met 
  - lastiger te debuggen, want je kan niet makkelijk zien wat voor data er over het lijntje gaat
  - niet iedere API testing tool heeft ondersteuning voor gRPC-endpoints te testen en meestal moet je nog twee stapjes extra ondernemen om het uberhaupt te kunnen testen

## State

### Caching

- in de cache houden van data
- database ontlasten
- resultaat query > cache
- applicatiebreed, niet gebruikersgebonden

Vormen van cache:
- in-memory
- distributed
- semi-commerciele oplossing  Redis

### Sessions

- server plaatst cookie met daarin een token, het sessietoken
- client (browser) stuurt bij opvolgende requests iedere keer die cookie met token op
- server kan dan gebruikersspecifieke info ophalen uit geheugen/database/...waar hij ook maar sessiedata opslaat
