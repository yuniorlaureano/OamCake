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
    public class OrderDeliveryListModel : PageModel
    {
        private readonly OamCakeContext _db;
        public TableResponse<Entity.OrderDelivery> OrderDeliveryTable { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int Pages { get; set; }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public OrderDeliveryListModel(OamCakeContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
            //var query = _db.OrderDelivery
            //               .Include(x => x.Order)
            //               .ThenInclude(x => x.User)
            //               .ThenInclude(x => x.Employee)
            //               .Include(x => x.Delivery)
            //               .ThenInclude(x => x.Employee)
            //               .Where(x => x.DeletedAt == null);

            //if (!String.IsNullOrWhiteSpace(Search))
            //{
            //    query = query.Where(x => x.Order.User.Employee.Name.Contains(Search) || x.Order.User.Employee.LastName.Contains(Search));
            //}

            //OrderDeliveryTable.Data = await query
            //                            .Skip((Pages) * 20)
            //                            .Take(20).ToListAsync();
            //OrderDeliveryTable.Count = await query.CountAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(p => p.Type == Constants.CLAIM_ID).Value);
            var orderDelivery = await _db.OrderDelivery.FirstOrDefaultAsync(x => x.Id == Id);
            if (orderDelivery != null)
            {
                orderDelivery.DeletedBy = userId;
                orderDelivery.DeletedAt = DateTime.UtcNow;
                _db.Update(orderDelivery);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("/admin/delivery/orderdeliverylist", new { Pages, Search });
        }
    }
}
