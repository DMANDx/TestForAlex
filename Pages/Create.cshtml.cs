using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Avto1Test.Models;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;
using Avto1Test.Utils;

namespace Avto1Test.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Avto1Test.Models.ApplicationContext _context;
        readonly ILogger logger = Log.loggerFactory.CreateLogger<CreateModel>();

        public CreateModel(Avto1Test.Models.ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public new Url Url { get; set; } = default!;

        public Url GetUrl()
        {
            return Url;
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(Url url)
        {

            Url.TinyURL = "";

            if (!ModelState.IsValid || _context.Urls == null || Url == null)
            {
                logger.LogCritical("Class<CreateModel>OnPostAsync: ModelState.IsValid || _context.Urls == null || Url == null");
                return Page();
            }

#pragma warning disable CS8604 // Possible null reference argument.
            Url.TinyURL = ToTiny();
            Url.NumOfCall = 0;
            Url.DateCreate = DateTime.Now;

            _context.Urls.Add(Url);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        
        /// <summary>
        /// Formatting Tiny URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string ToTiny()
        {
            string ur = "av1" + "/" + GenerateRandomString();
            return ur;
        }

        /// <summary>
        /// Generation Random for URL/[]
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomString()
        {
            Random r = new();
            int rand = r.Next(0, 2);

            int length = 6;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[r.Next(s.Length)]).ToArray());
        }
    }
}
