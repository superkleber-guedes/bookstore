using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Kleber.Bookstore.Attributes;
using Kleber.Bookstore.Models;
using bookstore.CommandHandlers;

namespace Kleber.Bookstore.Controllers
{
    /// <summary>
    /// Update an existing book
    /// </summary>
    [ApiController]
    public class UpdateBookByIdController : ControllerBase
    {
        private readonly IUpdateBookCommandHandler _commandHandler;

        /// <summary>
        /// Controller constuctor
        /// </summary>
        /// <param name="commandHandler"></param>
        public UpdateBookByIdController(IUpdateBookCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        /// <summary>
        /// Update an existing book
        /// </summary>
        /// <param name="body">A JSON object that represents a book.</param>
        /// <param name="id"></param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Book not found</response>
        [HttpPut]
        [Route("/books/{id}")]
        [ValidateModelState]
        [SwaggerOperation("UpdateBookById")]
        [SwaggerResponse(statusCode: 400, type: typeof(InlineResponse400), description: "Bad Request")]
        public virtual IActionResult UpdateBookById([FromBody] Book body, [FromRoute][Required] long? id)
        {
            return StatusCode(404);
        }
    }
}
