using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Kleber.Bookstore.Attributes;
using Kleber.Bookstore.Models;
using bookstore.QueryHandlers;
using bookstore.QueryHandlers.Queries;
using System;
using System.Threading.Tasks;

namespace Kleber.Bookstore.Controllers
{
    /// <summary>
    /// Returns a list of books
    /// </summary>
    [ApiController]
    public class GetBookController : ControllerBase
    {
        private readonly IGetBooksQueryHandler _queryHandler;

        /// <summary>
        /// Controller constuctor
        /// </summary>
        /// <param name="queryHandler"></param>
        public GetBookController(IGetBooksQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
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
        [SwaggerResponse(statusCode: 400, type: typeof(BadRequestResponse), description: "Bad Request")]
        public async Task<IActionResult> GetBook([FromQuery] string sortby)
        {
            try
            {
                GetBooksQuery query = new GetBooksQuery(sortby);

                var response = await _queryHandler.HandleAsync(query);

                return new ObjectResult(response);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return StatusCode(400, new BadRequestResponse(ex.Message));
            }
        }
    }
}
