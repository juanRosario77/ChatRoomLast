using ChatRoomApplication.Controllers;
using ChatRoomApplication.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting
{    
    public class ChatControllerTests
    {
        private readonly ChatController _controller;

        public ChatControllerTests()
        {
            // Setup the ChatService instance for testing
            var chatService = new ChatService();
            _controller = new ChatController(chatService);
        }

        [Fact]
        public void RegisterUser_WhenNameIsAvailable_ReturnsNoContent()
        {
            // Arrange
            var userDto = new UserDto { Name = "TestUser1" };

            // Act
            var result = _controller.RegisterUser(userDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void RegisterUser_WhenNameIsTaken_ReturnsBadRequest()
        {
            // Arrange
            var userDto1 = new UserDto { Name = "TestUser2" };
            var userDto2 = new UserDto { Name = "TestUser2" };

            // Add the first user with the name "TestUser2"
            _controller.RegisterUser(userDto1);

            // Act
            var result = _controller.RegisterUser(userDto2);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("This name is taken. Please choose another name", badRequestResult.Value);
        }
    }

}
