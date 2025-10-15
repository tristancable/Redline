using RedlineLib.Entites;
using RedlineLib.Models;

namespace RedlineLib.Data
{
    public interface IMessageDAL
    {
        List<User> GetAllUsers();
        List<Message> GetMessagesForUser(Guid userId);
        Message GetMessage(int messageId);
        DalResponse<Message> PostMessage(Guid senderId, Guid receiverId, string content);
        DalResponse<int> EditMessage(int messageId, string newContent);
        DalResponse<int> RemoveMessage(int messageId);
        User? GetUserByDisplayName(string displayName);
        User? GetUserByApplicationUserId(string applicationUserId);
        User? GetUserByEmail(string email);
        public DalResponse<int> UpdateUser(User user);
    }
}