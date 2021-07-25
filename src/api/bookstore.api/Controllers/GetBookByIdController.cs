using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Kleber.Bookstore.Attributes;
using Kleber.Bookstore.Models;
using bookstore.QueryHandlers;

namespace Kleber.Bookstore.Controllers
{
    /// <summary>
    /// Gets a book by id
    /// </summary>
    [ApiController]
    public class GetBookByIdController : ControllerBase
    {
        private readonly IGetBookByIdQueryHandler _commandHandler;

        /// <summary>
        /// Controller constuctor
        /// </summary>
        /// <param name="commandHandler"></param>
        public GetBookByIdController(IGetBookByIdQueryHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }
        
        /// <summary>
         /// Gets a book by id
         /// </summary>
         /// <param name="id"></param>
         /// <response code="200">Success</response>
         /// <response code="404">Book not found</response>
        [HttpGet]
        [Route("/books/{id}")]
        [ValidateModelState]
        [SwaggerOperation("GetBookById")]
        [SwaggerResponse(statusCode: 200, type: typeof(Book), description: "Success")]
        public virtual IActionResult GetBookById([FromRoute][Required] long? id)
        {
            return StatusCode(404);
        }
    }
}
