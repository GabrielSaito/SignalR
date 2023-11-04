using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using SingnalRChat.Models;
using SingnalRChat.Models.Post;
using SingnalRChat.Services;

[ApiController]
[Route("api/posts")]
public class PostController : ControllerBase
{
    private readonly PostService _postService;

    public PostController(PostService postService)
    {
        _postService = postService;
    }

    [HttpPost]
    public IActionResult CreatePost([FromBody] PostRequest request)
    {
        if (request == null)
        {
            return BadRequest("Dados de postagem inválidos.");  
        }

        if (string.IsNullOrEmpty(request.Title) || string.IsNullOrEmpty(request.Content))
        {
            return BadRequest("Título e conteúdo da postagem são obrigatórios.");  
        }

         PostModel createdPost = _postService.CreatePost(request.Title, request.Content);

         return Ok(new { Id = createdPost.Id, Title = createdPost.Title, Content = createdPost.Content, PublishDate = createdPost.PublishDate });
    }

}
