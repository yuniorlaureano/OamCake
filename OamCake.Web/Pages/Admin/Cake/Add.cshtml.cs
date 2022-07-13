using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Common;
using OamCake.Common.Helpers;
using OamCake.Data;
using OamCake.Data.Dto;
using OamCake.Entity;

namespace OamCake.Web.Pages.Admin.Cake
{
    [Authorize]
    public class AddModel : PageModel
    {
        [BindProperty]
        public CakeCreationDto CakeCreationDto { get; set; } = new();
        public List<Entity.Category> Categories { get; set; } = new();


        private readonly OamCakeContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;


        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public AddModel(OamCakeContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task OnGetAsync()
        {
            Categories = await _db.Category.Where(x => x.DeletedAt == null).ToListAsync();

            if (Id > 0)
            {
                var cake = await _db.Cake.FirstOrDefaultAsync(x => x.Id == Id);
                if (cake != null)
                {
                    CakeCreationDto = new()
                    {
                         Name = cake.Name,                         
                         Id = cake.Id,
                         CategoryId = cake.CategoryId,
                    };
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

            if (CakeCreationDto.Id > 0)
            {
                var cake = await _db.Cake.FirstOrDefaultAsync(x => x.Id == Id);

                cake.Name = CakeCreationDto.Name;
                cake.CategoryId = CakeCreationDto.CategoryId;
                CakeCreationDto.Photo?.UploadPhoto(cake.Photo, _webHostEnvironment.ContentRootPath, (photo) => cake.Photo ??= photo);

                _db.Cake.Update(cake);
            }
            else
            {
                var cake = new Entity.Cake
                {
                    Name = CakeCreationDto.Name,
                    CategoryId = CakeCreationDto.CategoryId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId,
                };

                CakeCreationDto.Photo?.UploadPhoto(null, _webHostEnvironment.ContentRootPath, (photo) => cake.Photo ??= photo);
                _db.Cake.Add(cake);
            }

            await _db.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
