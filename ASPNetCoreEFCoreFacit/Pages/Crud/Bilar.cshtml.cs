using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreEFCoreFacit.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASPNetCoreEFCoreFacit.Pages.Crud
{
    public class BilarModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<BilListViewModel> Bilar { get; set; }

        [BindProperty(SupportsGet = true)]
        public string q { get; set; }

        [BindProperty(SupportsGet = true)]
        public string sortcolumn { get; set; }

        [BindProperty(SupportsGet = true)]
        public string sortorder { get; set; }

        public enum SortOrder
        {
            asc,
            desc
        }

        public class BilListViewModel
        {
            public int Id { get; set; }
            public string Regno { get; set; }
            public string Manufacturer { get; set; }
            public string Model { get; set; }
            public decimal Price { get; set; }
        }

        public BilarModel(ApplicationDbContext context)
        {
            _context = context;
        }


        public void OnGet()
        {
            if (string.IsNullOrEmpty(sortcolumn))
                sortcolumn = nameof(BilListViewModel.Regno);

            if (string.IsNullOrEmpty(sortorder))
                sortorder = nameof(SortOrder.asc);

            var query = _context.Bilar.Select(r => new BilListViewModel
            {
                Id = r.Id,
                Manufacturer = r.Manufacturer.Namn,
                Model = r.Model,
                Price = r.Price,
                Regno = r.RegNo
            }).Where(e => string.IsNullOrEmpty(q) || e.Regno.Contains(q));

            if (sortcolumn == nameof(BilListViewModel.Regno))
            {
                if(sortorder == nameof(SortOrder.asc))
                    query = query.OrderBy(e => e.Regno);
                else
                    query = query.OrderByDescending(e => e.Regno);
            }
            else if (sortcolumn == nameof(BilListViewModel.Manufacturer))
            {
                if (sortorder == nameof(SortOrder.asc))
                    query = query.OrderBy(e => e.Manufacturer);
                else
                    query = query.OrderByDescending(e => e.Manufacturer);
            }
            else if (sortcolumn == nameof(BilListViewModel.Model))
            {
                if (sortorder == nameof(SortOrder.asc))
                    query = query.OrderBy(e => e.Model);
                else
                    query = query.OrderByDescending(e => e.Model);
            }
            else if (sortcolumn == nameof(BilListViewModel.Price))
            {
                if (sortorder == nameof(SortOrder.asc))
                    query = query.OrderBy(e => e.Price);
                else
                    query = query.OrderByDescending(e => e.Price);
            }

            Bilar = query.ToList();

        }
    }
}
