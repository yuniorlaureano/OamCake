using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Data;
using OamCake.Data.Dto;
using OamCake.Entity;

namespace OamCake.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly OamCakeContext _db;
        public Catalog Catalog { get; set; } = new();
        public IEnumerable<Entity.Category> Categories { get; set; } = Enumerable.Empty<Entity.Category>();

        public IndexModel(OamCakeContext db)
        {
            _db = db;
        }

        [BindProperty(SupportsGet = true)]
        public int Pages { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }

        public async Task OnGet()
        {
            var query = _db.Catalog.Include(x => x.CatalogDetails)
                                   .ThenInclude(x => x.Cake).Where(x => x.DeletedAt == null && x.IsPublished);

            Catalog = await query.FirstOrDefaultAsync();
            Catalog.CatalogDetails = Catalog.CatalogDetails.Where(x => 
                (x.Cake.CategoryId == CategoryId || CategoryId == null) &&
                (Search == null || x.Cake.Name.Contains(Search, StringComparison.OrdinalIgnoreCase))
            ).ToList();
            Categories = await _db.Category.ToListAsync();
        }
    }
}