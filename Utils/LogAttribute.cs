using Avto1Test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Text.RegularExpressions;
using ZstdSharp.Unsafe;

namespace Avto1Test.Utils
{
    public class LogXActionFilter : ResultFilterAttribute
    {
               
        public LogXActionFilter()
        {
                        
        }

        public void OnActionExecuting(ActionExecutingContext context, ILogger _logger, ApplicationContext _context)
        {
            

            var request = context.HttpContext.Request;
            var visitor = new Visit
            {
                UserAgent = request.Headers["User-Agent"].ToString(),
                RemoteAddr = request.HttpContext.Connection.RemoteIpAddress.ToString(),
                VisitDate = DateTime.Now
            };

            try
            {
                _context.Visits.Add(visitor);
                _context.SaveChanges();
            }
            catch
            {
                _logger.LogCritical("Class<Index>:OnPostIncrem: ERROR:ur = null");
                throw new Exception("Alert");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Метод, который будет вызван после выполнения действия
        }
    }
}