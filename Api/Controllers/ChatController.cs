using ChatRoomApplication.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;


namespace ChatRoomApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Chat API")]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;
        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("register-user")]
        [SwaggerOperation("Register a new user")]
        //[SwaggerResponse(StatusCodes.Status204NoContent, "User successfully registered")]
        public IActionResult RegisterUser(UserDto model)
        {
            if (_chatService.AddUserToList(model.Name))
            {
                // 204 status code
                return NoContent();
            }

            return BadRequest("This name is taken. Please choose another name");
        }
    }
}
