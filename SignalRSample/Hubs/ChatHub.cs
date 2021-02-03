using Microsoft.AspNetCore.SignalR;
using SignalRSample.Models.ORM.Context;
using SignalRSample.Models.ORM.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRSample.Hubs
{
    public class ChatHub : Hub
    {
        private ChatContext _chatContext;
        public override Task OnConnectedAsync()
        {
            var id = Context.ConnectionId;

            string connecteduser = Context.User.Claims.ToArray()[1].Value;

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            //string msg = message.Trim();
            await Clients.All.SendAsync("ReceiveMessage", message);

            //await Clients.Client("asdasdasd").SendAsync("ReceiveMessage", msg);
        }

        private AdminUser GetUser()
        {
            _chatContext = new ChatContext();

            var email = Context.User.Claims.ToArray()[0].Value;

            var currentUser = _chatContext.AdminUsers.FirstOrDefault(x => x.EMail == email);

            return currentUser;
        }
        public async Task ChangeStatus(string status)
        {
            string userstatus = status.Substring(status.IndexOf("-") + 1);

            GetUser().OnlineStatus = userstatus;
            _chatContext.SaveChanges();

            string connectuserid = Context.User.Claims.ToArray()[1].Value;


            await Clients.All.SendAsync("UserStatus", userstatus, connectuserid);

            //await Clients.Client("asdasdasd").SendAsync("ReceiveMessage", msg);
        }

    }
}