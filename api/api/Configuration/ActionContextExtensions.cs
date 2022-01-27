using System.Linq;
using api.Models.View;
using Microsoft.AspNetCore.Mvc;

namespace api.Configuration;

public static class ActionContextExtensions
{
    public static BadRequestObjectResult Format(this ActionContext actionContext)
    {
        var firstMessage = actionContext.ModelState
            .Where(x => x.Value.Errors.Any())
            .Take(1)
            .Select(x => x.Value.Errors.First())
            .Select(x => x.ErrorMessage)
            .First();

        return new BadRequestObjectResult(new GenericViewModel
        {
            Message = firstMessage.Replace("'", string.Empty)
        });
    }
}