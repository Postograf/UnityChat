using Microsoft.AspNetCore.SignalR;

using Server.Models;
using Server.Data;

namespace Server.Hubs
{
    public class ChatHub : Hub
    {
        public ChatContext _db;

        public ChatHub(ChatContext context)
        {
            _db = context;
        }

        public async void SendMessage(string name, string color, string message)
        {
            var sender = GetUser(name, color);

            if (sender is null) return;

            _db.Messages.Add(new Message { Sender = sender, Text = message });

            await _db.SaveChangesAsync();

            await Clients.All.SendAsync("ReceiveMessage", name, color, message);
        }

        public async void ChangeStatus(string name, string color, bool status)
        {
            var sender = GetUser(name, color);

            if (sender is null)
                _db.Users.Add(new User { Color = color, Status = status, Name = name });
            else
                sender.Status = status;
            await _db.SaveChangesAsync();
            
            await Clients.All.SendAsync("ReceiveStatus", name, color, status);
        }

        private User? GetUser(string name, string color)
        {
            return _db
                .Users
                .Where(x => x.Name == name && x.Color == color)
                .FirstOrDefault();
        }
    }
}
