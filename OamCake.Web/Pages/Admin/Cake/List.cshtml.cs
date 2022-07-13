using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Common;
using OamCake.Data;
using OamCake.Data.Dto;
using OamCake.Entity;

namespace OamCake.Web.Pages.Admin.Cake
{
    [Authorize]
    public class ListModel : PageModel
    {
        private readonly OamCakeContext _db;
        public TableResponse<OamCake.Entity.Cake> CakeTable { get; set; } = new();
        public IEnumerable<Entity.Category> Categories { get; set; } = Enumerable.Empty<Entity.Category>();


        [BindProperty]
        public int Id { get; set; }

        public ListModel(OamCakeContext db)
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
            var query = _db.Cake.Include(x => x.Category).Where(x => x.DeletedAt == null);

            if (!String.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(x => x.Name.Contains(Search));
            }

            if (CategoryId != 0 && CategoryId != null)
            {
                query = query.Where(x => x.CategoryId == CategoryId);
            }

            CakeTable.Data = await query
                            .Skip((Pages) * 20)
                            .Take(20).ToListAsync();
            CakeTable.Count = await query.CountAsync();

            Categories = await _db.Category.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(p => p.Type == Constants.CLAIM_ID).Value);
            var cake = await _db.Cake.FirstOrDefaultAsync(x => x.Id == Id);
            if (cake != null)
            {
                cake.DeletedBy = userId;
                cake.DeletedAt = DateTime.UtcNow;
                _db.Update(cake);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("/Cake/List", new { Pages, Search });
        }
    }
}
