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
    /// Returns a list of books
    /// </summary>
    [ApiController]
    public class GetBookController : ControllerBase
    {
        /// <summary>
        /// Returns a list of books. Sorted by title by default.
        /// </summary>
        /// <param name="sortby"></param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("//books")]
        [ValidateModelState]
        [SwaggerOperation("GetBook")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Book>), description: "Success")]
        public virtual IActionResult GetBook([FromQuery] string sortby)
        {
            return new ObjectResult(new List<Book>());
        }
    }
}
