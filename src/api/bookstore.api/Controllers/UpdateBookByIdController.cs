using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Kleber.Bookstore.Attributes;
using Kleber.Bookstore.Models;
using bookstore.CommandHandlers;
using bookstore.CommandHandlers.Commands;
using bookstore.Infrastructure.Exceptions;
using System.Threading.Tasks;

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
        [SwaggerResponse(statusCode: 400, type: typeof(BadRequestResponse), description: "Bad Request")]
        public async Task<IActionResult> UpdateBookById([FromBody] Book body, [FromRoute][Required] long? id)
        {
            if (id is null) return StatusCode(400, new BadRequestResponse("'Id' is required."));
            if (body.Price is null) return StatusCode(400, new BadRequestResponse("'Price' is required."));

            try
            {
                UpdateBookCommand command = new UpdateBookCommand(
                    id.Value,
                    body.Title,
                    body.Author,
                    body.Price.Value);

                await _commandHandler.HandleAsync(command);

                return StatusCode(200);
            }
            catch (ResourceNotFoundException)
            {
                return StatusCode(404);
            }
        }
    }
}
