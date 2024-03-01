using Demo.Shared.Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace DemoBalzor.Pages;

public partial class Poll : ComponentBase
{
    private HubConnection _connection;
    
    public PollModel NewPoll { get; set; } = new();
    public PollModel? ActivePoll { get; set; }
    
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
            .ConfigureLogging(logging =>
            {
                logging.SetMinimumLevel(LogLevel.Debug);
            })
            .Build();
        _connection.On("init-poll", (PollModel poll) =>
        {
            ActivePoll = poll;
            StateHasChanged();
        });
        _connection.On("vote", (int optionId) =>
        {
            ActivePoll.TotalNrOfVotes++;
            ActivePoll.Options.Single(x => x.Id == optionId).NrOfVotes++;
            StateHasChanged();
        });
        await _connection.StartAsync();
        
    }


    async Task InitPoll()
    {
        await _connection.SendAsync("InitPoll", NewPoll);
    }

    async Task Vote(OptionModel option)
    {
        await _connection.SendAsync("vote", option.Id);
    }
}