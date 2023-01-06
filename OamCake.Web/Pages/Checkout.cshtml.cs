using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Common;
using OamCake.Data;
using OamCake.Data.Dto;
using OamCake.Entity;

namespace OamCake.Web.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly OamCakeContext _db;

        [BindProperty]
        public List<CheckoutDto> Order { get; set; }

        public CheckoutModel(OamCakeContext db)
        {
            _db = db;
        }

        public async Task OnGet()
        {
           
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = Int32.Parse(User.Claims.FirstOrDefault(p => p.Type == Constants.CLAIM_ID).Value);

            var payment = Order.Select(x => x.Qty * x.Price).Sum(x => x);

            var order = new Order
            {
                CreatedAt = DateTime.Now,
                CreatedBy = userId,
                Payment = payment,
                Status = 'A',
                OrderDetails = Order.Select(x => new OrderDetail
                {
                    CakeId = x.CakeId,
                    Price = x.Price,
                    Quantity = x.Qty
                }).ToList()
            };

            _db.Order.Add(order);
            await _db.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
    }
}