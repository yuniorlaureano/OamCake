using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Common;
using OamCake.Data;
using OamCake.Data.Dto;

namespace OamCake.Web.Pages.Admin.Order
{
    [Authorize]
    public class ListModel : PageModel
    {
        private readonly OamCakeContext _db;
        public TableResponse<Entity.Order> OrderTable { get; set; } = new();

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
            var query = _db.Order
                           .Include(x => x.Client)
                           .Where(x => x.DeletedAt == null);

            if (!String.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(x => x.Client.Name.Contains(Search) || x.Client.LastName.Contains(Search));
            }

            OrderTable.Data = await query
                                        .Skip((Pages) * 20)
                                        .Take(20).ToListAsync();
            OrderTable.Count = await query.CountAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(p => p.Type == Constants.CLAIM_ID).Value);
            var order = await _db.Order.FirstOrDefaultAsync(x => x.Id == Id);
            if (order != null)
            {
                order.DeletedBy = userId;
                order.DeletedAt = DateTime.UtcNow;
                _db.Update(order);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("/admin/order/list", new { Pages, Search });
        }
    }
}
