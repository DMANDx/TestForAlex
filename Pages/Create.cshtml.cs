using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Avto1Test.Models;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Avto1Test.Pages
{
    public class CreateModel : PageModel
    {
        private readonly Avto1Test.Models.ApplicationContext _context;

        public CreateModel(Avto1Test.Models.ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Url Url { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            Url.TinyURL = "";

            if (!ModelState.IsValid || _context.Urls == null || Url == null)
            {
                return Page();
            }

            Url.TinyURL = ToTiny(Url.MainURL);
            Url.NumOfCall = 0;
            Url.DateCreate = DateTime.Now;

            _context.Urls.Add(Url);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private static string ToTiny(string url)
        {
            string ur = "av1" + "/" + GenerateRandomString();
            return ur;
        }
        
        public static string GenerateRandomString()
        {
            Random r = new Random();
            int rand = r.Next(0, 2);

            int length = 6;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[r.Next(s.Length)]).ToArray());
        }
    }
}
