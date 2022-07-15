using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Common;
using OamCake.Data;
using OamCake.Data.Dto;

namespace OamCake.Web.Pages.Admin.Delivery
{
    [Authorize]
    public class ListModel : PageModel
    {
        private readonly OamCakeContext _db;
        public TableResponse<Entity.Delivery> DeliveryTable { get; set; } = new();

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
            var query = _db.Delivery
                           .Include(x => x.Employee)
                           .Where(x => x.DeletedAt == null);

            if (!String.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(x => x.Employee.Name.Contains(Search) || x.Employee.LastName.Contains(Search));
            }

            DeliveryTable.Data = await query
                                        .Skip((Pages) * 20)
                                        .Take(20).ToListAsync();
            DeliveryTable.Count = await query.CountAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(p => p.Type == Constants.CLAIM_ID).Value);
            var delivery = await _db.Delivery.FirstOrDefaultAsync(x => x.Id == Id);
            if (delivery != null)
            {
                delivery.DeletedBy = userId;
                delivery.DeletedAt = DateTime.UtcNow;
                _db.Update(delivery);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("/admin/delivery/list", new { Pages, Search });
        }
    }
}
