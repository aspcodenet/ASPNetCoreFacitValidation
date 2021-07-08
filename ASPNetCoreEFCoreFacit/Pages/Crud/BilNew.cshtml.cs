using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreEFCoreFacit.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASPNetCoreEFCoreFacit.Pages.Crud
{
    public class BilNewModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public BilNewModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        [MaxLength(6)]
        public string Regno { get; set; }

        [BindProperty]
        public int ManufacturerId { get; set; }
        [BindProperty]
        public string Modell { get; set; }

        [BindProperty]
        [Range(1,1999999)]
        public decimal Price { get; set; }
        [BindProperty]
        public bool HasRadio { get; set; }

        public List<SelectListItem> AllManufacturers { get; set; }


        [BindProperty]
        [Range(1950, 2025)]
        public int Year { get; set; }


        [BindProperty]
        public DateTime FirstSalesDate { get; set; } //Dag och tid!
        
        [BindProperty]
        //[DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        
        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime BirthDateForOwner { get; set; } //Dag


        public string Bild { get; set; }

        public IActionResult OnPost(int id)
        {
            if (ModelState.IsValid)
            {
                var b = new Bil();
                _context.Bilar.Add(b);
                b.Model = Modell;
                b.HasRadio = HasRadio;
                b.Manufacturer = _context.Manufacturers.First(r=>r.Id == ManufacturerId);
                b.Price = Price;
                b.RegNo = Regno;
                b.Year = Year;
                _context.SaveChanges();
                return RedirectToPage("Bilar");
            }

            AllManufacturers = _context.Manufacturers.Select(e => new SelectListItem
            {
                Text = e.Namn,
                Value = e.Id.ToString(),
            }).ToList();
            return Page();
        }

        public void OnGet(int id)
        {

            FirstSalesDate = DateTime.Now;


            AllManufacturers = _context.Manufacturers.Select(e => new SelectListItem
            {
                Text = e.Namn,
                Value = e.Id.ToString(),
            }).ToList();
        }
    }
}
