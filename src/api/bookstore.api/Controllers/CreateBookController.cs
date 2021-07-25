using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Kleber.Bookstore.Attributes;
using Kleber.Bookstore.Models;
using bookstore.CommandHandlers;
using bookstore.CommandHandlers.Commands;
using System.Threading.Tasks;

namespace Kleber.Bookstore.API.Controllers
{
    /// <summary>
    /// Creates a new book
    /// </summary>
    [ApiController]
    public class CreateBookController : ControllerBase
    {
        private readonly ICreateBookCommandHandler _commandHandler;

        /// <summary>
        /// Controller constuctor
        /// </summary>
        /// <param name="commandHandler"></param>
        public CreateBookController(ICreateBookCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        /// <summary>
        /// Creates a new book
        /// </summary>
        /// <param name="body">A JSON object that represents a book.</param>
        /// <response code="201">Created</response>
        /// <response code="400">Bad Request</response>
        [HttpPost]
        [Route("/books")]
        [ValidateModelState]
        [SwaggerOperation("CreateBook")]
        [SwaggerResponse(statusCode: 201, type: typeof(CreatedResponse), description: "Created")]
        [SwaggerResponse(statusCode: 400, type: typeof(BadRequestResponse), description: "Bad Request")]
        public async Task<IActionResult> CreateBook([FromBody] Book body)
        {
            if (body.Id is null) return StatusCode(400, new BadRequestResponse("'Id' is required."));
            if (body.Price is null) return StatusCode(400, new BadRequestResponse("'Price' is required."));

            CreateBookCommand command = new CreateBookCommand(
                body.Id.Value,
                body.Title,
                body.Author,
                body.Price.Value);

            var idCreated = await _commandHandler.HandleAsync(command);

            return StatusCode(201, new CreatedResponse(idCreated));
        }
    }
}
