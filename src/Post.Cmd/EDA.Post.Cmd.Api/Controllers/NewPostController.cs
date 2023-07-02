using EDA.Core.Infraestructure;
using EDA.Post.Cmd.Api.Commands;
using EDA.Post.Cmd.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EDA.Post.Cmd.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class NewPostController : ControllerBase
    {
        private readonly ILogger<NewPostController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public NewPostController(ILogger<NewPostController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> NewPostAsync(NewPostCommand command)
        {
            try
            {
                command.Id = Guid.NewGuid();
                await _commandDispatcher.SendAsync(command);

                return StatusCode(StatusCodes.Status201Created, new NewPostResponse
                {
                    Id = command.Id,
                    Message = "New post creation request completed successfully!"
                });
            }
            catch(InvalidOperationException ex)
            {

                return BadRequest(new NewPostResponse
                {
                    Id = Guid.NewGuid(),
                    Message = "Client made a bad request!"
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to create a new post!";

                _logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);
                
                return StatusCode(StatusCodes.Status500InternalServerError, new NewPostResponse
                {
                    Id = command.Id,
                    Message = SAFE_ERROR_MESSAGE
                });

            }
        }
    }
}
