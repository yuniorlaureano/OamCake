using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Common;
using OamCake.Data;
using OamCake.Data.Dto;

namespace OamCake.Web.Pages.Admin.Product
{
    [Authorize]
    public class ListModel : PageModel
    {
        private readonly OamCakeContext _db;
        public TableResponse<Entity.Product> ProductTable { get; set; } = new TableResponse<Entity.Product>();

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
            var query = _db.Product.Where(x => x.DeletedAt == null);

            if (!String.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(x => x.Name.Contains(Search) || x.Description.Contains(Search));
            }

            ProductTable.Data = await query
                                        .Skip((Pages) * 20)
                                        .Take(20).ToListAsync();
            ProductTable.Count = await query.CountAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(p => p.Type == Constants.CLAIM_ID).Value);
            var product = await _db.Category.FirstOrDefaultAsync(x => x.Id == Id);
            if (product != null)
            {
                product.DeletedBy = userId;
                product.DeletedAt = DateTime.UtcNow;
                _db.Update(product);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("/admin/product/list", new { Pages, Search });
        }
    }
}
