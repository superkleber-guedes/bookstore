using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Kleber.Bookstore.Attributes;
using Kleber.Bookstore.Models;
using bookstore.CommandHandlers;

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
        [SwaggerResponse(statusCode: 201, type: typeof(InlineResponse201), description: "Created")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400), description: "Bad Request")]
        public virtual IActionResult CreateBook([FromBody] Book body)
        {
            return StatusCode(201, default(InlineResponse201));

        }
    }
}
