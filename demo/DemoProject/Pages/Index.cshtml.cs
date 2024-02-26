using DemoProject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoProject.Pages;

public class IndexModel : PageModel // code-behind
{
    public static List<Character> AvatarCharacters { get; set; } = new List<Character>()
    {
        new()
        {
            Id = 4,
            Name = "Aang",
            IsBender = true,
            Elements = { "Earth", "Air", "Fire", "Water" },
            PhotoUrl =
                "https://oyster.ignimgs.com/mediawiki/apis.ign.com/avatar-the-last-airbender/b/b0/Aang_img.jpg?width=325"
        },
        new()
        {
            Id = 8,
            Name = "Sokka",
            IsBender = false,
            Elements = null,
            PhotoUrl =
                "https://oyster.ignimgs.com/mediawiki/apis.ign.com/avatar-the-last-airbender/a/ad/Sokka_img.jpg?width=325"
        },
        new()
        {
            Id = 15,
            Name = "Katara",
            IsBender = true,
            Elements = { "Water" },
            PhotoUrl =
                "https://pics.craiyon.com/2023-10-02/bb59b64d2b7b4262bba96d775dcc2e3a.webp"
        }
    };

    [BindProperty]
    public Character NewCharacter { get; set; }
    
    public void OnGet()
    {
    }

    
    
    public IActionResult OnPost() // model binding
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        Console.WriteLine($"posting 2! {NewCharacter.Name} kan benden: {NewCharacter.IsBender}");
        NewCharacter.Id = AvatarCharacters.Max(x => x.Id) + 1;
        AvatarCharacters.Add(NewCharacter);
        return RedirectToPage(); // GET
    }
}