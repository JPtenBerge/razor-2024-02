using DemoProject.Entities;
using DemoProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoProject.Pages;

public class IndexModel : PageModel // code-behind
{
    private readonly ICharacterRepository _characterRepository;
    public IEnumerable<Character> AvatarCharacters { get; set; }

    [BindProperty]
    public Character NewCharacter { get; set; }

    public IndexModel(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
    }
    
    public void OnGet()
    {
        AvatarCharacters = _characterRepository.GetAll();
    }

    public IActionResult OnPost() // model binding
    {
        if (!ModelState.IsValid)
        {
            AvatarCharacters = _characterRepository.GetAll();
            return Page();
        }
        
        Console.WriteLine($"posting 2! {NewCharacter.Name} kan benden: {NewCharacter.IsBender}");
        _characterRepository.Add(NewCharacter);
        return RedirectToPage(); // GET
    }
}