using DemoProject.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Caching.Memory;

namespace DemoProject.Controllers;

[Route("api/state")]
public class StateController : ControllerBase
{
    private readonly IMemoryCache _memCache;

    public StateController(IMemoryCache memCache)
    {
        _memCache = memCache;
    }
    
    // [OutputCache(Duration = 10)]
    [HttpGet("caching")]
    public string Caching()
    {
        var nu = _memCache.GetOrCreate("nu", entry =>
        {
            // entry.tok
            entry.SlidingExpiration = TimeSpan.FromSeconds(10);
            return DateTime.Now;
        });
        return nu.ToLongTimeString();
    }

    [HttpGet("session")]
    public string Session()
    {
        var nu = HttpContext.Session.Get<DateTime?>("nu");
        if (nu == null)
        {
            nu = DateTime.Now;
            HttpContext.Session.Set("nu", nu);    
        }
        
        return nu.Value.ToLongTimeString();
    }
}