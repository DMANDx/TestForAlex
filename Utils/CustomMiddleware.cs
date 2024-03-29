using Avto1Test.Migrations;
using Avto1Test.Models;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace Avto1Test.Utils
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;        
        private readonly ILogger _logger;


        public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger)
        {
            _next = next;            
            _logger = logger;

        }

        public async Task InvokeAsync(HttpContext context, ApplicationContext DBcontext)
        {            
            if (context.Request.Path.StartsWithSegments("/"))
            {
                var request = context.Request;
                var visitor = new Visit
                {
                    UserAgent = request.Headers["User-Agent"].ToString(),
                    RemoteAddr = request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    VisitDate = DateTime.Now
                };

                try
                {
                    DBcontext.Visits.Add(visitor);
                    DBcontext.SaveChanges();
                }
                catch
                {
                    _logger.LogCritical("Class<Index>:OnPostIncrem: ERROR:ur = null");
                    throw new Exception("Alert");
                }
            }

            // Передача запроса дальше по конвейеру
            await _next(context);           
        }
    }
}
