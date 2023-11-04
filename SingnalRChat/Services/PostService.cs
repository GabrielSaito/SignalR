using SingnalRChat.Models;

namespace SingnalRChat.Services
{
    public class PostService
    {
        private List<PostModel> posts = new List<PostModel>();

        public PostModel CreatePost(string title, string content)
        {
            PostModel post = new PostModel
            {
                Title = title,
                Content = content,
                PublishDate = DateTime.Now
            };

            posts.Add(post);
            return post;
        }

        public List<PostModel> GetPosts()
        {
            return posts;
        }
    }
}
