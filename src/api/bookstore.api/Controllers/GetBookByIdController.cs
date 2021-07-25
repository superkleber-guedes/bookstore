using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Kleber.Bookstore.Attributes;

using Microsoft.AspNetCore.Authorization;
using Kleber.Bookstore.Models;

namespace Kleber.Bookstore.Controllers
{
    /// <summary>
    /// Gets a book by id
    /// </summary>
    [ApiController]
    public class GetBookByIdController : ControllerBase
    {
        /// <summary>
        /// Gets a book by id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Success</response>
        /// <response code="404">Book not found</response>
        [HttpGet]
        [Route("//books/{id}")]
        [ValidateModelState]
        [SwaggerOperation("GetBookById")]
        [SwaggerResponse(statusCode: 200, type: typeof(Book), description: "Success")]
        public virtual IActionResult GetBookById([FromRoute][Required] long? id)
        {
            return StatusCode(404);
        }
    }
}
