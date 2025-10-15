using Microsoft.EntityFrameworkCore;
using RedlineLib.Entites;
using RedlineLib.Models;

namespace Redline.Services
{
    public class MessageService
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl = "https://localhost:7297";

        public MessageService()
        {
            _http = new HttpClient();
        }

        // get all AspNetUsers
        public async Task<List<User>> GetAllUsersAsync()
        {
            var url = $"{_baseUrl}/Message/users";
            var result = await _http.GetFromJsonAsync<List<User>>(url);
            return result ?? new List<User>();
        }

        public async Task<Message?> GetMessageAsync(Guid id)
        {
            var url = $"{_baseUrl}/Message/{id}";
            return await _http.GetFromJsonAsync<Message>(url);
        }

        // GET - /Message/user/{userId}
        public async Task<List<Message>> GetMessagesForUserAsync(Guid userId)
        {
            var url = $"{_baseUrl}/Message/user/{userId}";
            var result = await _http.GetFromJsonAsync<List<Message>>(url);
            return result ?? new List<Message>();
        }

        // ✅ GET - /Message/user/byname/{displayName}
        public async Task<User?> GetUserByDisplayNameAsync(string displayName)
        {
            var encodedDisplayName = Uri.EscapeDataString(displayName);
            var url = $"{_baseUrl}/Message/user/byname/{encodedDisplayName}";
            return await _http.GetFromJsonAsync<User>(url);
        }

        // GET user by email
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var encodedEmail = Uri.EscapeDataString(email);
            var url = $"{_baseUrl}/Message/user/byemail/{encodedEmail}";
            return await _http.GetFromJsonAsync<User>(url);
        }

        // POST - /Message
        public async Task<bool> PostMessageAsync(Guid senderId, Guid receiverId, string content)
        {
            var url = $"{_baseUrl}/Message";

            var newMessage = new
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content
            };

            var result = await _http.PostAsJsonAsync(url, newMessage);

            return result.IsSuccessStatusCode;
        }

        // PUT - /Message/{id}
        public async Task<bool> EditMessageAsync(Guid id, string newContent)
        {
            var url = $"{_baseUrl}/Message/{id}";

            var model = new { NewContent = newContent };

            var result = await _http.PutAsJsonAsync(url, model);

            return result.IsSuccessStatusCode;
        }

        // DELETE - /Message/{id}
        public async Task<bool> DeleteMessageAsync(int id)
        {
            var url = $"{_baseUrl}/Message/{id}";

            var result = await _http.DeleteAsync(url);

            return result.IsSuccessStatusCode;
        }

        // GET user by ApplicationUserId
        public async Task<User?> GetUserByApplicationUserIdAsync(string appUserId)
        {
            var url = $"{_baseUrl}/Message/user/byappuser/{appUserId}";
            return await _http.GetFromJsonAsync<User>(url);
        }

        // PUT - update user
        public async Task<bool> UpdateUserAsync(User user)
        {
            var url = $"{_baseUrl}/Message/user/{user.Id}";
            var result = await _http.PutAsJsonAsync(url, user);
            return result.IsSuccessStatusCode;
        }
    }
}