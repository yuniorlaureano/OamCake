using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Data;
using OamCake.Data.Dto;

namespace OamCake.Web.Pages.Admin.Catalog
{
    [Authorize]
    public class AddModel : PageModel
    {
        private readonly OamCakeContext _db;
        public TableResponse<OamCake.Entity.Cake> CakeTable { get; set; } = new();
        public IEnumerable<Entity.Category> Categories { get; set; } = Enumerable.Empty<Entity.Category>();

        [BindProperty]
        public CatalogCreationDto Catalog { get; set; } = new();
                
        public AddModel(OamCakeContext db)
        {
            _db = db;
        }
                
        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CatalogId { get; set; }

        public string Errors { get; set; }

        public async Task OnGet()
        {
            IQueryable<Entity.Cake> query = null;

            if (CatalogId == null || CatalogId == 0)
            {
                query = _db.Cake.Include(x => x.Category).Where(x => x.DeletedAt == null);
            }
            else
            {
                query = _db.CatalogDetail
                            .Include(x => x.Cake)
                            .ThenInclude(x => x.Category)
                            .Where(x => x.Cake.DeletedAt == null)
                            .Select(x => x.Cake);
            }

            if (!String.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(x => x.Name.Contains(Search));
            }

            if (CategoryId != 0 && CategoryId != null)
            {
                query = query.Where(x => x.CategoryId == CategoryId);
            }

            CakeTable.Data = await query.ToListAsync();
            Categories = await _db.Category.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var x = await Task.FromResult(0);

            if (String.IsNullOrWhiteSpace(Catalog.Description))
            {
                Errors = "Debe proveer la descripción";
                return Page();
            }

            if (Catalog.Id > 0)
            {

            }
            else
            {

            }

            return Page();

        }
    }
}
