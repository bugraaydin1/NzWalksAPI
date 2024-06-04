using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NzWalksAPI.CustomActionFilters
{
    // experimantal Attribute purposes only:
    // not actually needed for validation handling
    // even without this, .Net sends BadRequest()
    // automatically if validation error occurs.
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                context.Result = new BadRequestResult();
            }
        }
    }
}