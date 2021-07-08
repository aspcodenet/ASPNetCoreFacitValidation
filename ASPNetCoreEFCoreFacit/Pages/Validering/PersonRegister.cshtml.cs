using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPNetCoreEFCoreFacit.Pages.Validering
{
    public class PersonRegisterModel : PageModel
    {
        [BindProperty]
        [Required]
        [MinLength(5)]
        [MaxLength(12)]
        public string Username { get; set; }
        
        [BindProperty]
        [Required]
        public string Password { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string PasswordAgain { get; set; }

        public void OnGet()
        {
        }


        public IActionResult OnPost()
        {
            if (IsValidPassword(Password) == false)
            {
                ModelState.AddModelError(nameof(Password), "Lösenordet måste innehålla både siffror och bokstäver");
            }
                

            if (ModelState.IsValid)
            {
                //Spara
                return RedirectToPage("/RegistrationConfirmation");
            }

            return Page();
        }

        private bool IsValidPassword(string password)
        {
            bool foundDigit = false;
            bool foundLetter = false;

            foreach (char ch in password)
            {
                if (Char.IsDigit(ch)) foundDigit = true;
                if (Char.IsLetter(ch)) foundLetter = true;
            }

            return foundDigit && foundLetter;
        }
    }
}
