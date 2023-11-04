using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SingnalRChat.Models;
using SingnalRChat.Services;

namespace SingnalRChat.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly PostService _postService;

        private readonly ILogger<PrivacyModel> _logger;

        [BindProperty]
        public PostModel Post { get; set; }
        public List<PostModel> Posts { get; set; }

        public PrivacyModel(PostService postService)
        {
            _postService = postService;
        }

        public void OnGet()
        {
            Posts = _postService.GetPosts();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _postService.CreatePost(Post.Title, Post.Content);
                return RedirectToPage();
            }
            return Page();
        }
    }
}