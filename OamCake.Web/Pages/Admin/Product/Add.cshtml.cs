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
    public class AddModel : PageModel
    {
        private readonly OamCakeContext _db;

        [BindProperty]
        public ProductDto Product { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public AddModel(OamCakeContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
            if (Id > 0)
            {
                var product = await _db.Product.FirstOrDefaultAsync(x => x.Id == Id);
                if (product != null)
                {
                    Product = new(product);
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = Int32.Parse(User.Claims.FirstOrDefault(p => p.Type == Constants.CLAIM_ID).Value);

            if (Product.Id > 0)
            {
                var product = await _db.Product.FirstOrDefaultAsync(x => x.Id == Id);

                product.Name = Product.Name;
                product.Description = Product.Description;
                product.Code = Product.Code;
                product.UpdatedAt = DateTime.Now;
                product.UpdatedBy = userId;

                _db.Product.Update(product);
            }
            else
            {
                var product = new Entity.Product()
                {
                    Name = Product.Name,
                    Description = Product.Description,
                    Code = Product.Code,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId
                };

                _db.Product.Add(product);
            }

            await _db.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
