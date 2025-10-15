using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RedlineLib.Entites;
using RedlineLib.Models;

namespace RedlineLib.Data
{
    public class EFMessageDAL : IMessageDAL
    {
        private readonly MessageContext context;

        public EFMessageDAL(string connString)
        {
            var builder = new DbContextOptionsBuilder<MessageContext>();
            builder.UseSqlServer(connString);

            this.context = new MessageContext(builder.Options);

            this.context.Database.EnsureCreated();
        }

        public List<User> GetAllUsers()
        {
            return context.Users.ToList();
        }

        public List<Message> GetMessagesForUser(Guid userId)
        {
            return context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Where(m => m.ReceiverId == userId)
                .OrderByDescending(m => m.Timestamp)
                .ToList();
        }

        public Message GetMessage(int messageId)
        {
            var result = context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .FirstOrDefault(m => m.Id == messageId);

            return result;
        }

        public DalResponse<Message> PostMessage(Guid senderId, Guid receiverId, string content)
        {
            // Use the ssender Id to lookup the correct User id

            var senderUser = context.Users.FirstOrDefault(u => u.Id == senderId);

            var message = new Message
            {
                SenderId = senderUser.Id,
                ReceiverId = receiverId,
                Content = content,
                Timestamp = DateTime.UtcNow
            };

            try
            {
                context.Messages.Add(message);
                context.SaveChanges();

                return new DalResponse<Message>
                {
                    Code = ResponseCodes.SUCCESS,
                    Message = "Message sent successfully",
                    Data = message
                };
            }
            catch
            {
                return new DalResponse<Message>
                {
                    Code = ResponseCodes.FAILURE,
                    Message = "Failed to send message"
                };
            }
        }

        public DalResponse<int> EditMessage(int messageId, string newContent)
        {
            var message = context.Messages.Find(messageId);
            if (message != null)
            {
                message.Content = newContent;
                context.SaveChanges();

                return new DalResponse<int>
                {
                    Code = ResponseCodes.SUCCESS,
                    Message = "Message updated"
                };
            }

            return new DalResponse<int>
            {
                Code = ResponseCodes.FAILURE,
                Message = "Message not found"
            };
        }

        public DalResponse<int> RemoveMessage(int messageId)
        {
            var message = context.Messages.Find(messageId);
            if (message != null)
            {
                context.Messages.Remove(message);
                context.SaveChanges();

                return new DalResponse<int>
                {
                    Code = ResponseCodes.SUCCESS,
                    Message = "Message deleted"
                };
            }

            return new DalResponse<int>
            {
                Code = ResponseCodes.FAILURE,
                Message = "Message not found"
            };
        }

        public User? GetUserByDisplayName(string displayName)
        {
            try
            {
                return context.Users.FirstOrDefault(u => u.DisplayName == displayName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); // Or use ILogger
                throw;
            }
        }

        public User? GetUserByApplicationUserId(string applicationUserId)
        {
            return context.Users.FirstOrDefault(u => u.ApplicationUserId == applicationUserId);
        }

        public User? GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.Email == email);
        }

        public DalResponse<int> UpdateUser(User user)
        {
            var existingUser = context.Users.FirstOrDefault(u => u.Id == user.Id);

            if (existingUser == null)
            {
                return new DalResponse<int>
                {
                    Code = ResponseCodes.FAILURE,
                    Message = "User not found"
                };
            }

            // Update only the fields you want to allow changes for
            existingUser.DisplayName = user.DisplayName;
            existingUser.ProfilePictureUrl = user.ProfilePictureUrl;
            existingUser.Age = user.Age;
            existingUser.Location = user.Location;
            existingUser.CurrentCar = user.CurrentCar;
            existingUser.FavoriteCar = user.FavoriteCar;

            try
            {
                context.SaveChanges();
                return new DalResponse<int>
                {
                    Code = ResponseCodes.SUCCESS,
                    Message = "User updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new DalResponse<int>
                {
                    Code = ResponseCodes.FAILURE,
                    Message = $"Error updating user: {ex.Message}"
                };
            }
        }

    }
}
