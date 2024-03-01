using Demo.Shared.Entities;
using Microsoft.AspNetCore.SignalR;

namespace DemoProject.Hubs;

public class PollHub : Hub
{
    public async Task InitPoll(PollModel newPoll)
    {
        await Clients.All.SendAsync("init-poll", newPoll);
    }

    public async Task Vote(int optionId)
    {
        await Clients.All.SendAsync("vote", optionId);
    }
}