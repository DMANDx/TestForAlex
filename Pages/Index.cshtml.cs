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

namespace Avto1Test.Pages
{
    
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

        //CORSES!!!
        //public async Task<RedirectResult> OnPostIncrem(int id)
        //{
        //    var ur = await _context.Urls.Where(z => z.Id == id).FirstOrDefaultAsync();

        //    if (ur != null)
        //    {
        //        ur.NumOfCall += 1;
        //        _context.Attach(ur).State = EntityState.Modified;
        //        await _context.SaveChangesAsync();
        //    }

        //    RedirectResult redirectResult = new RedirectResult(ur.MainURL, true,true);
        //    return redirectResult;
        //    //return Redirect(ur.MainURL);
        //}       



        /// <summary>
        /// Encrement for link
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostIncrem(int id)
        {          
            var ur = await _context.Urls.Where(z => z.Id == id).FirstOrDefaultAsync();

            if (ur != null)
            {
                ur.NumOfCall += 1;
                _context.Attach(ur).State = EntityState.Modified;
                await _context.SaveChangesAsync();                
            }
            else
            {
                logger.LogCritical("OnPostIncrem :ERROR ur = null");
            }

            //return RedirectToPage("");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return Redirect(ur.MainURL);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
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