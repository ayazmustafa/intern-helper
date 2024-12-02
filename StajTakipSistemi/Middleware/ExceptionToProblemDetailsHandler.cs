using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StajTakipSistemi.Exceptions.Base;

namespace StajTakipSistemi.Middleware;

public class ExceptionToProblemDetailsHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    private readonly ILogger<ExceptionToProblemDetailsHandler> _logger;

    public ExceptionToProblemDetailsHandler(IProblemDetailsService problemDetailsService, ILogger<ExceptionToProblemDetailsHandler> logger)
    {
        _problemDetailsService = problemDetailsService;
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {

        var problemDetails = new ProblemDetails();

        if (exception is ValidationException validationException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            problemDetails.Title = "One or more validation errors occurred.";
            problemDetails.Extensions.Add("errors", validationException.Errors.Select(x => x.ErrorMessage));
            _logger.LogInformation("selam");
        }
        else if (exception is ApplicationBusinessException applicationBusinessException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            problemDetails.Title = "Business rule violation.";
            problemDetails.Detail = applicationBusinessException.Message;
        }
        else if (exception is ApplicationTechnicalException )
        {
            // critical log
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            problemDetails.Title = "An error occurred while processing your request.";
            problemDetails.Detail = "Oops! Something went wrong on our end. Please try again later. If the issue persists, contact our support team for assistance.";
        }
        else
        {
            // critical log
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            problemDetails.Title = "An error occurred while processing your request.";
            problemDetails.Detail ="Oops! Something went wrong on our end. Please try again later. If the issue persists, contact our support team for assistance.";
        }

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext()
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails,
            Exception = exception
        });
    }
}