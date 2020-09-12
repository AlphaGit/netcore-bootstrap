using System.Net;
using Alpha.Bootstrap.Logic.Models.Errors;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Alpha.Bootstrap.WebApi
{
    public class WebApiResponses
    {
        public static ActionResult GetErrorResponse(Result result)
        {
            if (result.HasError<ResourceNotFoundError>())
                return new NotFoundResult();

            if (result.HasError<ConcurrencyError>())
                return new ConflictResult();

            return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
        }
    }
}
