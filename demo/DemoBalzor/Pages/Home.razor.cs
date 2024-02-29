using System.Collections;
using System.Net.Http.Json;
using Demo.Shared.Dtos;
using Demo.Shared.Entities;
using Flurl.Http;
using Microsoft.AspNetCore.Components;

namespace DemoBalzor.Pages;

public partial class Home : ComponentBase
{
    // [Inject] public HttpClient Http { get; set; }

    public string Name { get; set; } = "Arno 2";

    public CharacterPostRequestDto NewCharacter { get; set; } = new();

    public List<Character>? AvatarCharacters { get; set; }


    protected async override Task OnInitializedAsync()
    {
        // AvatarCharacters = (await Http.GetFromJsonAsync<IEnumerable<Character>>("api/character"))!.ToList();
        AvatarCharacters = (await "http://localhost:5074/api/character".GetJsonAsync<IEnumerable<Character>>()).ToList();

        // Http.PutAsJsonAsync<CharacterPostRequestDto>("api/product", NewCharacter);
    }


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

    void AddCharacter()
    {
        Console.WriteLine("Addding! " + NewCharacter.Name + " kan benden: " + NewCharacter.IsBender);

        AvatarCharacters.Add(NewCharacter.MapToEntity());
    }
}