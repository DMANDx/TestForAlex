using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Avto1Test.Models;

namespace Avto1Test.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly Avto1Test.Models.ApplicationContext _context;

        public DetailsModel(Avto1Test.Models.ApplicationContext context)
        {
            _context = context;
        }

      public new Url Url { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Urls == null)
            {
                return NotFound();
            }

            var url = await _context.Urls.FirstOrDefaultAsync(m => m.Id == id);
            if (url == null)
            {
                return NotFound();
            }
            else 
            {
                Url = url;
            }
            return Page();
        }
    }
}
