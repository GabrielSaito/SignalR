
namespace SingnalRChat.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; } 
        public int PostId { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
    }
}
