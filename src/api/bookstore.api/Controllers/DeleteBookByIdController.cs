using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Kleber.Bookstore.Attributes;

namespace Kleber.Bookstore.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class DeleteBookByIdController : ControllerBase
    {
        /// <summary>
        /// Deletes a book by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Success</response>
        /// <response code="404">Book not found</response>
        [HttpDelete]
        [Route("//books/{id}")]
        [ValidateModelState]
        [SwaggerOperation("DeleteBookById")]
        public virtual IActionResult DeleteBookById([FromRoute][Required] long? id)
        {
            return StatusCode(404);
        }
    }
}
