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
