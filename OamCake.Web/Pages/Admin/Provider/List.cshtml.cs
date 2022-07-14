using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Common;
using OamCake.Data;
using OamCake.Data.Dto;

namespace OamCake.Web.Pages.Admin.Provider
{
    [Authorize]
    public class ListModel : PageModel
    {
        private readonly OamCakeContext _db;
        public TableResponse<Entity.Provider> ProviderTable { get; set; } = new();

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
            var query = _db.Provider.Where(x => x.DeletedAt == null);

            if (!String.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(x => x.Name.Contains(Search));
            }

            ProviderTable.Data = await query
                                        .Skip((Pages) * 20)
                                        .Take(20).ToListAsync();
            ProviderTable.Count = await query.CountAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(p => p.Type == Constants.CLAIM_ID).Value);
            var provider = await _db.Provider.FirstOrDefaultAsync(x => x.Id == Id);
            if (provider != null)
            {
                provider.DeletedBy = userId;
                provider.DeletedAt = DateTime.UtcNow;
                _db.Update(provider);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("/admin/provider/list", new { Pages, Search });
        }
    }
}
