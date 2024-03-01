using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DemoProject.Pages;

public class Auth : PageModel
{
    private readonly UserManager<AvatarUser> _userManager;
    private readonly SignInManager<AvatarUser> _signInManager;
    public string Status { get; set; }

    public Auth(UserManager<AvatarUser> userManager, SignInManager<AvatarUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task OnGetRegister()
    {
        var user = new AvatarUser
        {
            UserName = "JP",
            Email = "jp@jp.nl",
            KanBenden = true
        };
        var result = await _userManager.CreateAsync(user, "Top$ecret1234!");
        if (result.Succeeded)
        {
            Status = "Geregistreerd!";
        }
        else
        {
            Status = string.Join(", ", result.Errors.Select(X => X.Description));
        }
    }

    public async Task OnGetLogin()
    {
        var result = await _signInManager.PasswordSignInAsync("JP", "Top$ecret1234!", false, false);
        if (result.Succeeded)
        {
            Status = "Ingelogd!";
        }
        else
        {
            Status = $"Kon niet inloggen: {result.IsLockedOut} | {result.IsNotAllowed} | {result.RequiresTwoFactor}";
        }
    }

    public async Task OnGetLogout()
    {
        await _signInManager.SignOutAsync();
        Status = "Uitgelogd!";
    }

    public async Task OnGetDoNothing()
    {
    }
}