using Microsoft.AspNetCore.SignalR;
using Server.Domain.Models;
using Server.Services.message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;
        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }
        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}
        public string MethodOneSimpleParameterSimpleReturnValue(string p1)
        {
            return p1;
        }
        public async Task SendMessage(int userId,int facultyId, string content)
        {
            Message message = new Message() { UserId = userId, Content = content, CreatedAt = DateTime.Now, FacultyId = facultyId};
            await _messageService.Create(message);
            await Clients.All.SendAsync("ReceiveMessage", userId, facultyId, content);
        }
    }
}
