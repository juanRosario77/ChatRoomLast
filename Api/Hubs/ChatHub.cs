
using ChatRoomApplication.Services;
using DataAccessLayer.Interface;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatRoomApplication.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatService _chatService;
        private readonly IGenericRepository<MessageDto> _genericRepository;
        private readonly IUnitOfWork _unitOfWork;

        private List<MessageDto> _messagesHistoy = new List<MessageDto>();
        public ChatHub(ChatService chatService)
        {
            _chatService = chatService;           
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "ChatRoom");
            await Clients.Caller.SendAsync("UserConnected");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "ChatRoom");
            var user = _chatService.GetUserByConnectionId(Context.ConnectionId);
            _chatService.RemoveUserFromList(user);
            await DisplayOnlineUsers();

            await base.OnDisconnectedAsync(exception);
        }

        public async Task AddUserConnectionId(string name)
        {
            _chatService.AddUserConnectinId(name, Context.ConnectionId);
            await DisplayOnlineUsers();
        }

        public async Task ReceiveMessage(MessageDto message)
        {
            //message.Content = "Saludos desde backend";                   
            //TODO: Guardo los mensaje/            
            await Clients.Group("ChatRoom").SendAsync("NewMessage", message);
        }      

        private async Task DisplayOnlineUsers()
        {
            var onlineUsers = _chatService.GetOnlineUsers();
            await Clients.Groups("ChatRoom").SendAsync("OnlineUsers", onlineUsers);
        }
    }
}
