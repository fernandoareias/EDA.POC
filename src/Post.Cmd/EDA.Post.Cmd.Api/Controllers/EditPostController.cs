using EDA.Core.Infraestructure;
using EDA.Post.Cmd.Api.Commands;
using EDA.Post.Cmd.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EDA.Post.Cmd.Api.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class EditPostController : ControllerBase
    {
        private readonly ILogger<EditPostController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public EditPostController(ILogger<EditPostController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMessageAsync(Guid id, EditPostCommand command)
        {
            try
            {
                command.Id = id;
                await _commandDispatcher.SendAsync(command);

                return StatusCode(StatusCodes.Status200OK, new NewPostResponse
                {
                    Id = command.Id,
                    Message = "New post creation request completed successfully!"
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new NewPostResponse
                {
                    Id = Guid.NewGuid(),
                    Message = "Client made a bad request!"
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to edit a new post!";

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
