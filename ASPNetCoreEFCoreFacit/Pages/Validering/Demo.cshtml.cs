using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ASPNetCoreEFCoreFacit.Pages.Validering
{
    public class DemoModel : PageModel
    {
        [BindProperty]
        [MaxLength(10)]
        public string Namn { get; set; }

        [BindProperty]
        [MaxLength(512)]
        public string Beskrivning { get; set; }


        [BindProperty]
        //[Required]
        public string SelectDayOfWeek { get; set; }
        public List<SelectListItem> AllDayOfWeeks { get; set; }

        [BindProperty]
        //[DataType(DataType.Date)]
        public DateTime StartDay { get; set; } // Date


        [BindProperty]
        [Range(1,10,ErrorMessage = "Du måste skriva ett tal mellan 1 och 10")]
        public int Age { get; set; }


        [BindProperty]
        [Range(1900, 2025)]
        public int Year { get; set; }

        [BindProperty]
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        
        [BindProperty]
        [Required]
        [MinLength(6)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string PasswordAgain { get; set; }


        [BindProperty]
        [Range(1900,2025)]
        [GoodHockeyYear]   // [SvensktPersonNummer]   [RegNo]
        public int Year2 { get; set; }


        public void OnGet()
        {
            SetAllDayOfWeeks();
        }

        public IActionResult OnPost()
        {
            var day = Enum.Parse<DayOfWeek>(SelectDayOfWeek);
            if (StartDay.Year >= 2022 && day == DayOfWeek.Thursday)
            {
                ModelState.AddModelError("SelectDayOfWeek", "Torsdagar inte möjliga från 2022 och framåt");
            }

            if (ModelState.IsValid)
            {
                
                return RedirectToPage("/RegisterConfirmation");
            }

            SetAllDayOfWeeks();
            return Page();
        }

        private void SetAllDayOfWeeks()
        {
            AllDayOfWeeks = Enum.GetValues<DayOfWeek>().Cast<DayOfWeek>()
                .Select(e => new SelectListItem(e.ToString(), ((int)e).ToString())).ToList();

            AllDayOfWeeks.Insert(0, new SelectListItem("...välj dag...", "-1"));
        }

    }


    public class GoodHockeyYearAttribute : ValidationAttribute
    {
        public GoodHockeyYearAttribute()
        {
            ErrorMessage = "Det var INTE ett bra hockeyår. Sverige vann inte VM då";
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            int year = int.Parse(value.ToString());
            var goodyears = new []{ 1953, 1957, 1962, 1987, 1991, 1992, 1998, 2006, 2013 };
            if (goodyears.Contains(year)) return ValidationResult.Success;
            return new ValidationResult(ErrorMessage);
        }
    }
}
