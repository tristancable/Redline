namespace RedlineLib.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public int Age { get; set; }
        public string Location { get; set; }
        public string CurrentCar { get; set; }
        public string FavoriteCar { get; set; }

        public ICollection<MessageModel> Messages { get; set; }
    }
}