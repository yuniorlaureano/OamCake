using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Common;
using OamCake.Data;
using OamCake.Data.Dto;

namespace OamCake.Web.Pages.Admin.Category
{
    [Authorize]
    public class ListModel : PageModel
    {
        private readonly OamCakeContext _db;
        public TableResponse<Entity.Category> CategoryTable { get; set; } = new TableResponse<Entity.Category>();

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
            var query = _db.Category.Where(x => x.DeletedAt == null);

            if (!String.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(x => x.Name.Contains(Search) || x.Name.Contains(Search));
            }

            CategoryTable.Data = await query
                                        .Skip((Pages) * 20)
                                        .Take(20).ToListAsync();
            CategoryTable.Count = await query.CountAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(p => p.Type == Constants.CLAIM_ID).Value);
            var category = await _db.Category.FirstOrDefaultAsync(x => x.Id == Id);
            if (category != null)
            {
                category.DeletedBy = userId;
                category.DeletedAt = DateTime.UtcNow;
                _db.Update(category);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("/Category/List", new { Pages, Search });
        }
    }
}
