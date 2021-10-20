using Logeecom.DemoProjekat.Exceptions;
using Logeecom.DemoProjekat.PL.ViewModels.RepsonseModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace Logeecom.DemoProjekat.PL.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        [Route("error")]
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            var code = HttpStatusCode.InternalServerError;
            string title = "Internal error";

            if (exception is ValidationException)
            {
                code = HttpStatusCode.BadRequest;
                title = "Validation error occured.";
            }
            else if (exception is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
                title = "Validation error occured.";
            }
            else if (exception is UnauthenticatedException)
            {
                code = HttpStatusCode.Unauthorized;
                title = "Unauthorized.";
            }
            else if (exception is UnauthorizedAccessException)
            {
                code = HttpStatusCode.Forbidden;
                title = "Forbidden.";
            }
            else
            {
                this.logger.LogError(exception.Message);
            }

            Response.StatusCode = (int)code;

            return new ErrorResponse() { Title = title, Status = code, Message = exception.Message };
        }
    }
}
