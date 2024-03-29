﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Avto1Test.Models;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.Extensions.Logging;
using Avto1Test.Utils;

namespace Avto1Test.Pages
{
    public class EditModel : PageModel
    {
        private readonly Avto1Test.Models.ApplicationContext _context;
        readonly ILogger logger = Log.loggerFactory.CreateLogger<EditModel>();

        public EditModel(Avto1Test.Models.ApplicationContext context)
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

            var url =  await _context.Urls.FirstOrDefaultAsync(m => m.Id == id);
            
            if (url == null)
            {
                logger.LogCritical("Class<EditModel>OnGetAsync: url == null");
                return NotFound();
            }

            Url = url;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Url.DateCreate = DateTime.Now;
            _context.Attach(Url).State = EntityState.Modified;

            try
            {
                
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!UrlExists(Url.Id))
                {
                    return NotFound();
                }
                else
                {
                    logger.LogCritical($"OnPostAsync: {ex.Message}");
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UrlExists(int id)
        {
          return (_context.Urls?.Any(e => e.Id == id)).GetValueOrDefault();
        }        
    }
}
