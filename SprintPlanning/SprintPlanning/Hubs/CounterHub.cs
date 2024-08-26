using Microsoft.AspNetCore.SignalR;

namespace SprintPlanning.Hubs;

public class CounterHub : Hub
{
    public async Task UpdateCounter(int currentCount)
    {
        // Broadcast the updated count to all connected clients
        await Clients.All.SendAsync("ReceiveCountUpdate", currentCount);
    }
}
