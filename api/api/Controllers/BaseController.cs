using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.Models.View;
using FluentValidation;
using FluentValidation.Results;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMapper Mapper;

        public BaseController(IMapper mapper)
        {
            Mapper = mapper;
        }
        
         protected BadRequestObjectResult BadRequest(string message)
        {
            return BadRequest(new GenericViewModel
            {
                Message = message
            });
        }

        protected BadRequestObjectResult BadRequest(IEnumerable<string> messages)
        {
            return BadRequest(new ValidationErrorsViewModel
            {
                Messages = messages
            });
        }

        protected NotFoundObjectResult NotFound(string message)
        {
            return NotFound(new GenericViewModel
            {
                Message = message
            });
        }

        protected ObjectResult Conflict(string message)
        {
            return StatusCode((int) HttpStatusCode.Conflict, new GenericViewModel
            {
                Message = message
            });
        }

        protected CreatedResult Created<T>(T data)
        {
            return Created(string.Empty, data);
        }

        protected ObjectResult Forbidden(string message)
        {
            return StatusCode((int) HttpStatusCode.Forbidden, new GenericViewModel
            {
                Message = message
            });
        }

        internal static async Task<(bool, IEnumerable<string>)> IsValid<T>(object model) where T : IValidator, new()
        {
            var results = await Validate<T>(model);
            if (results.IsValid)
            {
                return (true, new string[0]);
            }

            var errorMessages = results.Errors
                .Select(x => x.ErrorMessage);

            return (false, errorMessages);
        }

        private static async Task<ValidationResult> Validate<T>(object model) where T : IValidator, new()
        {
            var validator = new T();
            return await validator.ValidateAsync(model);
        }
    }
}