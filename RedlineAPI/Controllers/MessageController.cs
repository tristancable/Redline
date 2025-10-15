using Microsoft.AspNetCore.Mvc;
using RedlineLib.Data;
using RedlineLib.Entites;
using RedlineLib.Models;
using System.Collections.Generic;

namespace RedlineAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IMessageDAL dal;

        public MessageController(ILogger<MessageController> logger, IMessageDAL dal)
        {
            _logger = logger;
            this.dal = dal;
        }

        // GET all AspNetUsers
        [HttpGet("users")]
        public IEnumerable<User> GetAllUsers()
        {
            return dal.GetAllUsers();
        }


        // GET - /Message/{id}
        [HttpGet("{id}")]
        public Message Get(int id)
        {
            return dal.GetMessage(id);
        }

        // ✅ NEW: GET - /Message/user/{userId}
        [HttpGet("user/{userId}")]
        public IEnumerable<Message> GetMessagesForUser(Guid userId)
        {
            return dal.GetMessagesForUser(userId);
        }

        // ✅ NEW: GET - /Message/user/byname/{displayName}
        [HttpGet("user/byname/{displayName}")]
        public User? GetUserByDisplayName(string displayName)
        {
            return dal.GetUserByDisplayName(displayName);
        }

        // ✅ NEW: GET - /Message/user/byemail/{email}
        [HttpGet("user/byemail/{email}")]
        public User? GetUserByEmail(string email)
        {
            return dal.GetUserByEmail(email);
        }

        // POST - /Message
        [HttpPost]
        public IActionResult Post([FromBody] MessagePostModel model)
        {
            if (model == null)
                return BadRequest("Invalid message data.");

            var response = dal.PostMessage(model.SenderId, model.ReceiverId, model.Content);

            if (response.Code == ResponseCodes.SUCCESS)
                return Ok(response.Data);
            else
                return BadRequest(response.Message);
        }

        // PUT - /Message/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MessageEditModel model)
        {
            if (model == null)
                return BadRequest("Invalid data.");

            var response = dal.EditMessage(id, model.NewContent);

            if (response.Code == ResponseCodes.SUCCESS)
                return Ok(response.Message);
            else
                return BadRequest(response.Message);
        }

        // DELETE - /Message/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var response = dal.RemoveMessage(id);

            if (response.Code == ResponseCodes.SUCCESS)
                return Ok(response.Message);
            else
                return NotFound(response.Message);
        }

        // ✅ NEW: GET - /Message/user/byappuser/{appUserId}
        [HttpGet("user/byappuser/{appUserId}")]
        public ActionResult<User?> GetUserByApplicationUserId(string appUserId)
        {
            var user = dal.GetUserByApplicationUserId(appUserId);

            if (user == null)
                return NotFound($"No user found with ApplicationUserId {appUserId}");

            return Ok(user);
        }

        // ✅ NEW: PUT - /Message/user/{id}
        [HttpPut("user/{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody] User updatedUser)
        {
            if (updatedUser == null || updatedUser.Id != id)
                return BadRequest("Invalid user data.");

            dal.UpdateUser(updatedUser);

            return Ok("User updated successfully.");
        }
    }

    // Helper models for POST and PUT requests
    public class MessagePostModel
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; } = "";
    }

    public class MessageEditModel
    {
        public string NewContent { get; set; } = "";
    }
}