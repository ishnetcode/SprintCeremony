using Microsoft.AspNetCore.SignalR;

namespace SprintPlanning.Hubs;

public class RoomHub : Hub
{
    private static Dictionary<string, List<string>> rooms = new Dictionary<string, List<string>>();

    public async Task JoinRoom(string roomId, string userName)
    {
        if (!rooms.ContainsKey(roomId))
        {
            rooms[roomId] = new List<string>();
        }

        if (!rooms[roomId].Contains(userName))
        {
            rooms[roomId].Add(userName);
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        await Clients.Group(roomId).SendAsync("UserJoined", userName, rooms[roomId]);
    }

    public async Task CastVote(string roomId, string userName, int points)
    {
        await Clients.Group(roomId).SendAsync("VoteCast", userName, points);
    }

    public async Task ShowVotes(string roomId)
    {
        await Clients.Group(roomId).SendAsync("ShowVotes");
    }

    public async Task AddStory(string roomId, string storyTitle)
    {
        // Broadcast the new story to everyone in the room
        await Clients.Group(roomId).SendAsync("ReceiveStory", storyTitle);
    }
}
