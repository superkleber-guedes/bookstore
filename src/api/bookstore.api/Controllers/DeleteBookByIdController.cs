using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Kleber.Bookstore.Attributes;
using bookstore.CommandHandlers;

namespace Kleber.Bookstore.Controllers
{
    /// <summary>
    /// Deletes a book by id
    /// </summary>
    [ApiController]
    public class DeleteBookByIdController : ControllerBase
    {
        private readonly IDeleteBookCommandHandler _commandHandler;

        /// <summary>
        /// Controller constuctor
        /// </summary>
        /// <param name="commandHandler"></param>
        public DeleteBookByIdController(IDeleteBookCommandHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        /// <summary>
        /// Deletes a book by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Success</response>
        /// <response code="404">Book not found</response>
        [HttpDelete]
        [Route("/books/{id}")]
        [ValidateModelState]
        [SwaggerOperation("DeleteBookById")]
        public virtual IActionResult DeleteBookById([FromRoute][Required] long? id)
        {
            return StatusCode(404);
        }
    }
}
