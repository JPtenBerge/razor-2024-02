using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoProject.Pages;

public class Error : PageModel
{
    public void OnGet()
    {
        throw new NotImplementedException("Ga weg");
    }
}