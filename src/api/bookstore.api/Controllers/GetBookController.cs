using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Kleber.Bookstore.Attributes;
using Kleber.Bookstore.Models;
using bookstore.QueryHandlers;

namespace Kleber.Bookstore.Controllers
{
    /// <summary>
    /// Returns a list of books
    /// </summary>
    [ApiController]
    public class GetBookController : ControllerBase
    {
        private readonly IGetBooksQueryHandler _commandHandler;

        /// <summary>
        /// Controller constuctor
        /// </summary>
        /// <param name="commandHandler"></param>
        public GetBookController(IGetBooksQueryHandler commandHandler)
        {
            _commandHandler = commandHandler;
        }

        /// <summary>
        /// Returns a list of books. Sorted by title by default.
        /// </summary>
        /// <param name="sortby"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/books")]
        [ValidateModelState]
        [SwaggerOperation("GetBook")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Book>), description: "Success")]
        public virtual IActionResult GetBook([FromQuery] string sortby)
        {
            return new ObjectResult(new List<Book>());
        }
    }
}
