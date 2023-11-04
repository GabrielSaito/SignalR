namespace SingnalRChat.Models.Post
{
    public class Like
    {
        public int LikeId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }

        public User User { get; set; }
        public PostModel Post { get; set; }
    }


}
