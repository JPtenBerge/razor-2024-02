using Demo.Shared.Entities;
using Microsoft.AspNetCore.Components;

namespace DemoBalzor.Pages;

public partial class Home : ComponentBase
{
    public string Name { get; set; } = "Arno 2";

    public List<Character>? AvatarCharacters { get; set; } = new List<Character>()
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
    
    void ChangeName()
    {
        Name += "Jurre";

        // var t = new Thread(() =>
        // {
        //     Console.WriteLine("hallo vanaf thread");
        // });
        // t.Start();
        
        new Timer(new TimerCallback(_ => // vertaald naar setInterval()
        {
            Console.WriteLine("hoi!");
            Name += "q";
            StateHasChanged();
        }), null, 1000, 1000);
    }
}