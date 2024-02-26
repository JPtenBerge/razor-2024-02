using DemoProject.Entities;
using DemoProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoProject.Pages;

public class IndexModel : PageModel // code-behind
{
    private readonly ICharacterRepository _characterRepository;
    private readonly INationRepository _nationRepository;
    public IEnumerable<Character> AvatarCharacters { get; set; }
    public IEnumerable<SelectListItem> NationOptions { get; set; }
    
    [BindProperty]
    public Character NewCharacter { get; set; }

    public IndexModel(ICharacterRepository characterRepository, INationRepository nationRepository)
    {
        _characterRepository = characterRepository;
        _nationRepository = nationRepository;
    }
    
    public void OnGet()
    {
        GetData();
    }

    public IActionResult OnPost() // model binding
    {
        if (!ModelState.IsValid)
        {
            GetData();
            return Page();
        }
        
        Console.WriteLine($"posting 2! {NewCharacter.Name} kan benden: {NewCharacter.IsBender}");
        _characterRepository.Add(NewCharacter);
        return RedirectToPage(); // GET
    }

    private void GetData()
    {
        AvatarCharacters = _characterRepository.GetAll();
        NationOptions = _nationRepository.GetAll().Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        });
    }
}