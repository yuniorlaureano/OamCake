using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Common;
using OamCake.Data;
using OamCake.Data.Dto;

namespace OamCake.Web.Pages.Admin.Catalog
{
    [Authorize]
    public class ListModel : PageModel
    {
        private readonly OamCakeContext _db;
        public TableResponse<Entity.Catalog> CatalogTable { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int Pages { get; set; }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public ListModel(OamCakeContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
            var query = _db.Catalog.Where(x => x.DeletedAt == null);

            if (!String.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(x => x.Description.Contains(Search));
            }

            CatalogTable.Data = await query
                                        .Skip((Pages) * 20)
                                        .Take(20).ToListAsync();
            CatalogTable.Count = await query.CountAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(p => p.Type == Constants.CLAIM_ID).Value);
            var catalog = await _db.Catalog.FirstOrDefaultAsync(x => x.Id == Id);
            if (catalog != null)
            {
                catalog.DeletedBy = userId;
                catalog.DeletedAt = DateTime.UtcNow;
                _db.Update(catalog);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("/admin/catalog/list", new { Pages, Search });
        }
    }
}
