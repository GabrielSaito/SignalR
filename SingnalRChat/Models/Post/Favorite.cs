namespace SingnalRChat.Models.Post
{
    public class Favorite
    {
        public int FavoriteId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }

        public User User { get; set; }
        public PostModel Post { get; set; }
    }

}
