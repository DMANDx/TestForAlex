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

namespace Avto1Test.Pages
{
    
    public class IndexModel : PageModel
    {
        private readonly Avto1Test.Models.ApplicationContext _context;

        public IndexModel(Avto1Test.Models.ApplicationContext context)
        {
            _context = context;
        }

        public new IList<Models.Url> Url { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Urls != null)
            {
                Url = await _context.Urls.OrderByDescending(z => z.DateCreate).ToListAsync();
            }
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

        public async Task<IActionResult> OnPostIncrem(int id)
        {          
            var ur = await _context.Urls.Where(z => z.Id == id).FirstOrDefaultAsync();

            if (ur != null)
            {
                ur.NumOfCall += 1;
                _context.Attach(ur).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }            
            
            //return RedirectToPage("");
            return Redirect(ur.MainURL);
            //return null;
        }

        public string GetUri(string str)
        {
            Uri uri = new(str);
            string lastSegment = uri.Segments[uri.Segments.Length - 1];
            return lastSegment;
        }        
    }
}