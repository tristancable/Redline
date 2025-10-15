namespace RedlineLib.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public UserModel User { get; set; }
        public Guid AuthorId { get; set; }
        public UserModel Author { get; set; }
    }
}