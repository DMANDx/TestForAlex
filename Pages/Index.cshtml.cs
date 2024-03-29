using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Avto1Test.Models;
using System.Security.Policy;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Logging;
using Avto1Test.Utils;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Avto1Test.Pages
{
    //[LogXActionFilter]
    public class IndexModel : PageModel
    {
        private readonly Avto1Test.Models.ApplicationContext _context;

        readonly ILogger logger = Log.loggerFactory.CreateLogger<IndexModel>();


        public IndexModel(Avto1Test.Models.ApplicationContext context)
        {
            _context = context;
        }

        public new IList<Models.Url> Url { get;set; } = default!;
        
        public async Task OnGetAsync()
        {            
            try
            {
                if (_context.Urls != null)
                {
                    Url = await _context.Urls.OrderByDescending(z => z.DateCreate).ToListAsync();
                }
            }
            catch (Exception ex) 
            {
                logger.LogCritical($"Getting all:ERROR {ex}");
            }

            logger.LogInformation("Getting all:OK");
        }

        /// <summary>
        /// Encrement for link
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        public async Task<IActionResult> OnPostIncremAsync(int id)
        {
            var ur = _context.Urls.Where(z => z.Id == id).FirstOrDefault();

            if (ur != null)
            {
                ur.NumOfCall += 1;
                _context.Attach(ur).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            else
            {
                logger.LogCritical("Class<Index>:OnPostIncrem: ERROR:ur = null");
            }

            return new EmptyResult();
        }
        /// <summary>
        /// Function for getting Tiny link
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string GetUri(string str)
        {
            Uri uri = new(str);
            string lastSegment = uri.Segments[uri.Segments.Length - 1];
            return lastSegment;
        }        
    }
}