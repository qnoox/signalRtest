using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.SignalR;

using System.Linq;
using System.Threading.Tasks;

namespace Grimshot.Hubs
{
    public class GameHub : Hub
    {
		public async Task SendMessage(string user, string message)
		{
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}

		public override async Task OnConnectedAsync()
		{
			//var id = Context.ConnectionId;
			await Clients.All.SendAsync("Send", $"{Context.ConnectionId} joined", Context.ConnectionId);
		}

		public override async Task OnDisconnectedAsync(Exception ex)
		{
			await Clients.Others.SendAsync("Send", $"{Context.ConnectionId} left", Context.ConnectionId);
		}

		//used for the shape and chat example.
		public async Task Send(string message, string id)
		{
			await Clients.Client(Context.ConnectionId).SendAsync("Send", message, id);
		}

		//used for the shape example
		public async Task MoveShape(int x, int y, string id)
		{
			await Clients.Others.SendAsync("shapeMoved", x, y, Context.ConnectionId);
			//await Clients.All.SendAsync("shapeMoved", x, y);
		}

	}
}
