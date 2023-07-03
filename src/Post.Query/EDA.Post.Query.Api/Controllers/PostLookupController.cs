using EDA.Core.Infraestructure;
using EDA.Post.Query.Api.DTOs;
using EDA.Post.Query.Api.Queries;
using EDA.Post.Query.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EDA.Post.Query.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PostLookupController : ControllerBase
    {
        private readonly ILogger<PostLookupController> logger;
        private readonly IQueryDispatcher<PostEntity> _queryDispatcher;

        public PostLookupController(ILogger<PostLookupController> logger, IQueryDispatcher<PostEntity> queryDispatcher)
        {
            this.logger = logger;
            _queryDispatcher = queryDispatcher;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            var posts = await _queryDispatcher.SendAsync(new FindAllPostsQuery());

            if (posts == null || !posts.Any())
                return NoContent();

            return Ok(new PostLookupResponse
            {
                Posts = posts,
            });
        }
    }
}
