using System.Collections.Generic;
using System.Linq;
using System.Net;
using System;
using System.Threading.Tasks;
using api.Models.View;
using FluentValidation;
using FluentValidation.Results;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Authorize]
[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public class ApiController : ControllerBase
{
    protected readonly IMapper Mapper;

    public ApiController(IMapper mapper)
    {
        Mapper = mapper;
    }
        
    protected BadRequestObjectResult BadRequest(string message) =>
        BadRequest(new GenericViewModel
        {
            Message = message
        });

    protected NotFoundObjectResult NotFound(string message) =>
        NotFound(new GenericViewModel
        {
            Message = message
        });

    protected ObjectResult Conflict(string message) =>
        StatusCode((int) HttpStatusCode.Conflict, new GenericViewModel
        {
            Message = message
        });

    protected CreatedResult Created<T>(T data) => Created(string.Empty, data);

    protected ObjectResult Forbidden(string message) =>
        StatusCode((int) HttpStatusCode.Forbidden, new GenericViewModel
        {
            Message = message
        });

    internal static async Task<(bool, IEnumerable<string>)> IsValid<TValidator, TModel>(TModel model) where TValidator : IValidator<TModel>, new()
    {
        var results = await Validate<TValidator, TModel>(model);
        if (results.IsValid) return (true, Array.Empty<string>());

        var errorMessages = results.Errors
            .Select(x => x.ErrorMessage);

        return (false, errorMessages);
    }

    private static async Task<ValidationResult> Validate<TValidator, TModel>(TModel model) where TValidator : IValidator<TModel>, new()
    {
        var validator = new TValidator();
        return await validator.ValidateAsync(model);
    }
}