using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.SignalR;

public class ChatHub : Hub
{
    private byte[] encryptionKey = new byte[16];

    public async Task JoinChat(string username)
    {
         await Groups.AddToGroupAsync(Context.ConnectionId, username);

         await Clients.Caller.SendAsync("JoinedChat", $"Você entrou na conversa como {username}");
    }
    public async Task SendMessage(string user, string message)
    {
        var encryptionService = new EncryptionService(encryptionKey);

        string encryptedMessage = encryptionService.Encrypt(message);

        await Clients.All.SendAsync("ReceiveMessage", user, encryptedMessage);
    }
    public async Task LeaveChat(string username)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, username);

        await Clients.Caller.SendAsync("LeftChat", $"Você saiu da conversa como {username}");
    }

    public async Task PrivateMessage(string fromUser, string toUser, string message)
    {
        var encryptionService = new EncryptionService(encryptionKey);

        string encryptedMessage = encryptionService.Encrypt(message);

        await Clients.Group(toUser).SendAsync("ReceivePrivateMessage", fromUser, encryptedMessage);
    }

    public async Task BroadcastTyping(string username)
    {
        await Clients.OthersInGroup(username).SendAsync("Typing", $"{username} está digitando...");
    }

    public async Task StopTyping(string username)
    {
        await Clients.OthersInGroup(username).SendAsync("StopTyping", $"{username} parou de digitar.");
    }


}


