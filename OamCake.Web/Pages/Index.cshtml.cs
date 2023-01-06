using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Data;
using OamCake.Data.Dto;

namespace OamCake.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly OamCakeContext _db;
        public CatalogListDto Catalog { get; set; } = new();
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

            var result = await query.FirstOrDefaultAsync();
            if (result != null)
            {
                Catalog = new CatalogListDto
                {
                    Description = result.Description,
                    Id = result.Id,
                    CatalogDetailListDto = result.CatalogDetails.Where(x =>
                            (x.Cake.CategoryId == CategoryId || CategoryId == null) &&
                            (Search == null || x.Cake.Name.Contains(Search, StringComparison.OrdinalIgnoreCase))
                    ).Select(x => new CatalogDetailListDto
                    {
                        Id = x.Id,
                        CakeId = x.CakeId,
                        CakeName = x.Cake.Name,
                        CatalogId = x.CatalogId,
                        Photo = x.Photo,
                        RealPhoto = x.Cake.Photo,
                        Price = x.Price
                    }).ToList()
                };
            }

            Categories = await _db.Category.ToListAsync();
        }
    }
}