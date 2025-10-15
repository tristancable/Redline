using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedlineLib.Entites
{
    [Table("User")]
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Link to ASP.NET Identity user
        [Required]
        public string ApplicationUserId { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string Email { get; set; } = string.Empty;

        // Default display name will be the email when creating a new user
        [Required, MaxLength(100)]
        public string DisplayName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string ProfilePictureUrl { get; set; } = string.Empty;

        public int? Age { get; set; }

        [MaxLength(200)]
        public string Location { get; set; } = string.Empty;

        [MaxLength(100)]
        public string CurrentCar { get; set; } = string.Empty;

        [MaxLength(100)]
        public string FavoriteCar { get; set; } = string.Empty;

        public ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();

        // Optional: constructor to initialize DisplayName with email
        public User(string applicationUserId, string email)
        {
            ApplicationUserId = applicationUserId;
            DisplayName = email;
        }

        // Parameterless constructor needed by EF
        public User() { }
    }
}