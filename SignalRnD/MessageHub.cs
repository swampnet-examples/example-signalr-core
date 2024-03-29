﻿using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;

namespace SignalRnD;

/// <summary>
/// MessageHub is where we broadcast messages to clients, and clients call methods on the hub.
/// </summary>
public class MessageHub : Hub
{
    private readonly IConfiguration _cfg;

    public MessageHub(IConfiguration cfg)
    {
        _cfg = cfg;
    }

    /// <summary>
    /// Called when a client connects to the hub.
    /// </summary>
    public async override Task OnConnectedAsync()
    {
        Console.WriteLine($"OnConnectedAsync: {Context.ConnectionId}");

        // Just broadcast an 'msg' back to the caller
        await Clients.Caller.SendAsync("msg", "connected");

        // And send a message to all the other clients informing them of the noobie
        await Clients.Others.SendAsync("msg", $"Welcome '{Context.ConnectionId}' to the hub!");

        await base.OnConnectedAsync();
    }


    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine($"OnDisconnectedAsync: {exception?.Message}");

        return base.OnDisconnectedAsync(exception);
    }


    public async Task SendMessage(string message)
    {
        Console.WriteLine("SendMessage: " + message);

        // Broadcast an 'msg' to all clients *except* the caller
        await Clients.Others.SendAsync("msg", message);
    }


    public async Task Heartbeat(string message)
    {
        // Broadcase an 'hb' message to all clients
        await Clients.All.SendAsync("hb", message);
    }
}
