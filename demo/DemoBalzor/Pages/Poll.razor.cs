using Demo.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace DemoBalzor.Pages;

public partial class Poll : ComponentBase
{
    private HubConnection _connection;
        
    protected override async Task OnInitializedAsync()
    {
        // init form stuff
        var options = new List<OptionModel>();
        for (int i = 0; i < 4; i++)
        {
            options.Add(new() { Id = i });
        }
        NewPoll.Options = options;
        
        // setup signalr connection
        _connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5074/pollHub")
            .Build();
        await _connection.StartAsync();
        // .ConfigureLogging(Logging)
    }

    public PollModel NewPoll { get; set; } = new();

    async Task InitPoll()
    {
        await _connection.SendAsync("initPoll", NewPoll);
    }
}