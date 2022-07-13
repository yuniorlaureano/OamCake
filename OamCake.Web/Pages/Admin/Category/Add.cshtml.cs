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
    public class AddModel : PageModel
    {
        private readonly OamCakeContext _db;

        [BindProperty]
        public CategoryDto Category { get; set; } = new CategoryDto();

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
                var category = await _db.Category.FirstOrDefaultAsync(x => x.Id == Id);
                if (category != null)
                {
                    Category = new CategoryDto(category);
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

            if (Category.Id > 0)
            {
                var savedCategory = await _db.Category.FirstOrDefaultAsync(x => x.Id == Id);

                savedCategory.Name = Category.Name;
                savedCategory.UpdatedAt = DateTime.Now;
                savedCategory.UpdatedBy = userId;

                _db.Category.Update(savedCategory);
            }
            else
            {
                var category = new Entity.Category()
                {
                    Name = Category.Name,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId
                };

                _db.Category.Add(category);
            }

            await _db.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
