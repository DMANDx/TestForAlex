using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Avto1Test.Models;
using Avto1Test.Utils;

namespace Avto1Test.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly Avto1Test.Models.ApplicationContext _context;

        readonly ILogger logger = Log.loggerFactory.CreateLogger<DeleteModel>();

        public DeleteModel(Avto1Test.Models.ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
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
                logger.LogCritical("Class<Delete>:OnGetAsync: url == null");
                return NotFound();
            }
            else 
            {
                Url = url;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Urls == null)
            {
                return NotFound();
            }
            var url = await _context.Urls.FindAsync(id);

            if (url != null)
            {
                Url = url;
                _context.Urls.Remove(Url);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex) 
                {
                    logger.LogCritical($"Class<Delete>:OnPostAsync: ERROR:{ex.Message}");
                }
                

                
            }

            return RedirectToPage("./Index");
        }
    }
}
