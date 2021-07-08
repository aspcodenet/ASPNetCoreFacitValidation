using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreEFCoreFacit.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPNetCoreEFCoreFacit.Pages.Validering
{
    public class BilRegModel : PageModel
    {
        [BindProperty]
        [Required]
        [MinLength(5)]
        [MaxLength(120)]
        public string Manufacturer { get; set; }

        [BindProperty]
        [Required]
        [MinLength(5)]
        [MaxLength(120)]
        public string Model { get; set; }


        [BindProperty]
        [Required]
        [Range(1900,2022)]
        public int Year { get; set; }

        [BindProperty]
        [Required]
        [RegNo]
        public string RegNo { get; set; }



        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //Spara
                return RedirectToPage("/RegisterConfirmation");
            }

            return Page();
        }


        public void OnGet()
        {
        }
    }
}
