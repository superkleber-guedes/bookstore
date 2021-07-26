using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Kleber.Bookstore.Attributes;
using Kleber.Bookstore.Models;
using bookstore.QueryHandlers;
using bookstore.Infrastructure.Exceptions;
using System.Threading.Tasks;

namespace Kleber.Bookstore.Controllers
{
    /// <summary>
    /// Gets a book by id
    /// </summary>
    [ApiController]
    public class GetBookByIdController : ControllerBase
    {
        private readonly IGetBookByIdQueryHandler _queryHandler;

        /// <summary>
        /// Controller constuctor
        /// </summary>
        /// <param name="queryHandler"></param>
        public GetBookByIdController(IGetBookByIdQueryHandler queryHandler)
        {
            _queryHandler = queryHandler;
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
        public async Task<IActionResult> GetBookById([FromRoute][Required] long id)
        {
            try
            {
                var result = await _queryHandler.HandleAsync(id);
                return new ObjectResult(result);
            }
            catch (ResourceNotFoundException)
            {
                return StatusCode(404);
            }
        }
    }
}
