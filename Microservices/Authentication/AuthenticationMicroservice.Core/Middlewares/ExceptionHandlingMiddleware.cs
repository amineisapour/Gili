using System;
using System.Collections.Generic;
using System.Net;
using AuthenticationMicroservice.Core.Interfaces;
using GiliX.Common;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuthenticationMicroservice.Core.Middlewares
{
    public class ExceptionHandlingMiddleware : object
    {
        protected RequestDelegate Next { get; }

        public ExceptionHandlingMiddleware(RequestDelegate next) : base()
        {
            Next = next;
        }

        public async System.Threading.Tasks.Task InvokeAsync(HttpContext context)
        {
            try
            {
                //var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                //var userId = accountService.ValidateJwtToken(token);
                //if (userId != null)
                //{
                //    // attach user to context on successful jwt validation
                //    context.Items["User"] = await QueryUnitOfWork.Users.GetUserByUserIdAsync(userId.Value);
                //}

                var next = Next(context);

                switch (context.Response.StatusCode)
                {
                    case (int)HttpStatusCode.NotFound:
                        throw new KeyNotFoundException();
                    case (int)HttpStatusCode.Unauthorized:
                        throw new UnauthorizedAccessException();
                    case (int)HttpStatusCode.Forbidden:
                        throw new Exceptions.ForbiddenException("Attempted to perform a forbidden operation.");
                    case (int)HttpStatusCode.RequestTimeout:
                        throw new TimeoutException();
                    case (int)HttpStatusCode.UnsupportedMediaType:
                        throw new UnsupportedContentTypeException("model binder for the body of the request is unable to understand the request content-type header!");
                    case (int)HttpStatusCode.BadRequest:
                        throw new BadHttpRequestException("Bad Request");
                }
                await next;
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static System.Threading.Tasks.Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var result = new FluentResults.Result();

            /*
            FluentValidation.ValidationException validationException = exception as FluentValidation.ValidationException;
            if (validationException != null)
            {
                var code = System.Net.HttpStatusCode.BadRequest;
                context.Response.StatusCode = (int)code;
                foreach (var error in validationException.Errors)
                {
                    result.WithError(error.ErrorMessage);
                }
            }
            */
            // Log Error!

            switch (exception)
            {
                case Exceptions.ApplicationException:
                    result.WithError($"Status Code: {(int)HttpStatusCode.BadRequest} ({nameof(HttpStatusCode.BadRequest)})");
                    break;
                case FluentValidation.ValidationException e:
                    result.WithError($"Status Code: {(int)HttpStatusCode.BadRequest} ({nameof(HttpStatusCode.BadRequest)})");
                    foreach (var error in e.Errors)
                    {
                        result.WithError(error.ErrorMessage);
                    }
                    break;
                case KeyNotFoundException:
                    result.WithError($"Status Code: {(int)HttpStatusCode.NotFound} ({nameof(HttpStatusCode.NotFound)})");
                    break;
                case UnauthorizedAccessException:
                    result.WithError($"Status Code: {(int)HttpStatusCode.Unauthorized} ({nameof(HttpStatusCode.Unauthorized)})");
                    break;
                case Exceptions.ForbiddenException:
                    result.WithError($"Status Code: {(int)HttpStatusCode.Forbidden} ({nameof(HttpStatusCode.Forbidden)})");
                    break;
                case TimeoutException:
                    result.WithError($"Status Code: {(int)HttpStatusCode.RequestTimeout} ({nameof(HttpStatusCode.RequestTimeout)})");
                    break;
                case UnsupportedContentTypeException:
                    result.WithError($"Status Code: {(int)HttpStatusCode.UnsupportedMediaType} ({nameof(HttpStatusCode.UnsupportedMediaType)})");
                    break;
                case BadHttpRequestException:
                    result.WithError($"Status Code: {(int)HttpStatusCode.BadRequest} ({nameof(HttpStatusCode.BadRequest)})");
                    break;
                default:
                    result.WithError($"Status Code: {(int)HttpStatusCode.InternalServerError} ({nameof(HttpStatusCode.InternalServerError)})");
                    result.WithError("Internal Server Error!");
                    break;
            }

            if (exception != null && !string.IsNullOrEmpty(exception.Message))
            {
                result.WithError(exception.Message);
                if (exception.InnerException != null && !string.IsNullOrEmpty(exception.InnerException.Message))
                {
                    result.WithError(exception.InnerException.Message);
                }
            }

            var options = new System.Text.Json.JsonSerializerOptions
            {
                IncludeFields = true,
                PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase,
            };

            string resultString = System.Text.Json.JsonSerializer.Serialize(value: result.ConvertToDtxResult(), options: options);

            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(resultString);
        }
    }
}
