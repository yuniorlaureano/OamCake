using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Common;
using OamCake.Data;
using OamCake.Data.Dto;

namespace OamCake.Web.Pages.Admin.Projection
{
    [Authorize]
    public class ListModel : PageModel
    {
        private readonly OamCakeContext _db;
        public TableResponse<Entity.Projection> ProjectionTable { get; set; } = new();

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
            var query = _db.Projection.Where(x => x.DeletedAt == null);

            if (!String.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(x => x.Description.Contains(Search));
            }

            ProjectionTable.Data = await query
                                        .Skip((Pages) * 20)
                                        .Take(20).ToListAsync();
            ProjectionTable.Count = await query.CountAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(p => p.Type == Constants.CLAIM_ID).Value);
            var projection = await _db.Projection.FirstOrDefaultAsync(x => x.Id == Id);
            if (projection != null)
            {
                projection.DeletedBy = userId;
                projection.DeletedAt = DateTime.UtcNow;
                _db.Update(projection);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("/admin/projection/list", new { Pages, Search });
        }
    }
}
