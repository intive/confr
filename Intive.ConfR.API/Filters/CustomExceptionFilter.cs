using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Intive.ConfR.Application.Exceptions;
using Intive.ConfR.Logging;
using SixLabors.ImageSharp;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using Intive.ConfR.Infrastructure;
using Hangfire;
using Intive.ConfR.Persistence.Repositories.Exceptions;
using Intive.ConfR.Domain.Exceptions;
using Microsoft.AspNetCore.Hosting;

namespace Intive.ConfR.API.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILoggerManager _logger;
        private readonly IHostingEnvironment _environment;
        private readonly Mails _options;

        public CustomExceptionFilterAttribute(ILoggerManager logger, IOptions<Mails> options, IHostingEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
            _options = options.Value;
        }

        public override void OnException(ExceptionContext context)
        {
            HttpStatusCode code;

            switch (context.Exception)
            {
                case ValidationException _:
                    context.HttpContext.Response.ContentType = "application/json";
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Result = new JsonResult(
                        ((ValidationException)context.Exception).Failures);

                    _logger.LogError($"{context.Exception.Message}");

                    return;
                case ImageFormatException _:
                case EMailAddressInvalidException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                case ForbiddenException _:
                    code = HttpStatusCode.Forbidden;
                    break;
                case NotFoundException _:
                case DeleteFailureException _:
                case CommentNotFoundException _:
                case ContainerNotFoundException _:
                case ImageNotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
                case UnauthorizedAccessException _:
                    code = HttpStatusCode.Unauthorized;
                    break;
                case ConflictException _:
                    code = HttpStatusCode.Conflict;
                    break;
                case UriFormatException _:
                    code = HttpStatusCode.RequestUriTooLong;
                    break;
                case InvalidFormatException _:
                    code = HttpStatusCode.UnprocessableEntity;
                    break;
                case GraphApiException _:
                    var exception = context.Exception as GraphApiException;
                    code = (HttpStatusCode)exception.StatusCode;
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    SendEmail(context.Exception);
                    break;
            }
            
            _logger.LogError($"{context.Exception.Message}");

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;

            context.Result = _environment.IsProduction()
                ? new JsonResult(new
                {
                    error = new[] {context.Exception.Message}
                })
                : new JsonResult(new
                {
                    error = new[] {context.Exception.Message},
                    stackTrace = context.Exception.StackTrace
                });
        }

        private void SendEmail(Exception ex)
        {
            var mails = _options.Recipients;

            foreach (var mail in mails)
            {
                var newMail = new Application.Notifications.Models.Mail
                {
                    From = _options.Admin,
                    FromName = _options.AdminName,
                    FromPassword = _options.AdminPassword,
                    ToName = _options.RecipientName,
                    To = mail,
                    Body = $"Something bad happened: {ex.Message} \n {ex.StackTrace}",
                    Subject = "Internal Server Error"
                };

                BackgroundJob.Enqueue(() => EmailService.SendAsync(newMail));
            }
        }
    }

    public class Mails
    {
        public string RecipientName { get; set; }
        public List<string> Recipients { get; set; }
        public string AdminName { get; set; }
        public string Admin { get; set; }
        public string AdminPassword { get; set; }
    }
}
