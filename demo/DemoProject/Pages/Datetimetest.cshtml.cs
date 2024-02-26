using DemoProject.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoProject.Pages;

public class Datetimetest : PageModel
{
    public Todo NewTodo { get; set; } = new()
    {
        When = DateTime.Now.AddMinutes(-10)
    };
    
    public void OnGet()
    {
        
    }
}