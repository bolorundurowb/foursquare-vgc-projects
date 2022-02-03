using System.Linq;
using Microsoft.AspNetCore.Mvc;
using neophyte.api.Models.View;

namespace neophyte.api.Configuration;

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