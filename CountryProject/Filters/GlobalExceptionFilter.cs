using CountryProject.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualBasic;
using System.Net;
using System;
using CountryProject.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using CountryProject.Services;
using CountryProject.Models;
using Newtonsoft.Json;

namespace CountryProject.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        private readonly IEmailServices _emailService;
        public GlobalExceptionFilter(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var logger = provider.GetService<ILogger<GlobalExceptionFilter>>();
            var emailService = provider.GetService<IEmailServices>();

            _logger = logger;
            _emailService = emailService;
        }
        public void OnException(ExceptionContext context)
        {
            

            var (responseModel, statusCode) = GetStatusCode<object>(context.Exception, _logger);
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)statusCode;
            response.ContentType = "application/json";
            //send mail if critical error
            if (statusCode == HttpStatusCode.InternalServerError)
            {
                _emailService.SendMail(new EmailModel
                {
                    Subject = "Critical Error Occured",
                    Receiver = "admin@countryproject.app",
                    HtmlBody = $"<div>\r\n    <p>Hello Admin</p>\r\n    <br />\r\n    <p>An Error Occured : {JsonConvert.SerializeObject(responseModel)}</p>\r\n</div>"
                });
            }


            context.Result = new JsonResult(responseModel);
        }

        public static (object responseModel, HttpStatusCode statusCode) GetStatusCode<T>(Exception exception, ILogger logger)
        {
            switch (exception)
            {
                case BaseException bex:
                    {
                        logger.LogError(bex,bex.Message);
                        return (new
                        {
                            Message = bex.Message,
                            Status = false,
                        }, bex.HttpStatusCode);
                    }
                default:
                    {
                        logger.LogCritical(exception, exception.Message);
                        return (new
                        {
                            Message = exception.Message,
                            Status = false
                        }, HttpStatusCode.InternalServerError);
                    }


            }
        }
    }
}
