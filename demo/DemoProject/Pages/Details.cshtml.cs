using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoProject.Pages;

public class Details : PageModel
{
    public void OnGet(int? id)
    {
        Console.WriteLine("Details met id " + id);
    }

    public void OnGetRegister(string hoi)
    {
        Console.WriteLine("register " + hoi);
    }

    public void OnGetSubscribe()
    {
        Console.WriteLine("subscribe");
    }
}