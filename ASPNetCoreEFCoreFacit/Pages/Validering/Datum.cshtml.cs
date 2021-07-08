using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASPNetCoreEFCoreFacit.Pages.Validering
{
    public class DatumModel : PageModel
    {
        [BindProperty]
        [Range(1900,2025)]
        public int Year { get; set; }

        [BindProperty]
        [Range(1, 12)]
        public int Month { get; set; }

        [BindProperty]
        [Range(1, 31, ErrorMessage = "Dagen måste vara mellan 1 och 31")]
        public int Day { get; set; }


        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

            }

            return Page();
        }

        public void OnGet()
        {
        }
    }
}
