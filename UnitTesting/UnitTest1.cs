using ChatRoomApplication.Controllers;
using ChatRoomApplication.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using NUnit.Framework;

namespace UnitTesting
{

    public class UnitTest1
    {

        private ChatService _chatService;
        private ChatController _controller;

        public UnitTest1()
       {
            _chatService = new ChatService();
            _controller = new ChatController(_chatService);
        }    

        [Fact]
        public void RegisterUser_NewUser_ReturnsNoContent()
            {
                // Arrange
                var newUser = new UserDto { Name = "John" };

                // Act
                var result = _controller.RegisterUser(newUser);

                // Assert
                NUnit.Framework.Assert.That(result, Is.TypeOf<NoContentResult>());
            }

        [Fact]
        public void RegisterUser_ExistingUser_ReturnsBadRequest()
            {
                // Arrange
                var existingUser = new UserDto { Name = "Jane" };
                _chatService.AddUserToList(existingUser.Name);

                // Act
                var result = _controller.RegisterUser(existingUser);

                // Assert
                NUnit.Framework.Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
            }
        
    }
}

