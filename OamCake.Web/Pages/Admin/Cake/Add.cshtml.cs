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
        public List<Entity.Product> Products { get; set; } = new();


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
            Products = await _db.Product.Where(x => x.DeletedAt == null).ToListAsync();

            if (Id > 0)
            {
                var cake = await _db.Cake
                            .Include(x => x.Ingredients)
                            .ThenInclude(x => x.Product)
                            .FirstOrDefaultAsync(x => x.Id == Id);

                if (cake != null)
                {
                    CakeCreationDto = new()
                    {
                        Name = cake.Name,
                        Id = cake.Id,
                        CategoryId = cake.CategoryId,
                        StrPhoto = cake.Photo,
                        Ingredients = cake.Ingredients.Select(x => new IngredientDto
                        {
                            ProductId = x.ProductId.Value,
                            ProductName = x.Product.Name + " " + x.Product.Description,
                            Quantity = x.Quantity,
                            Unit = x.Unid
                        }).ToList()
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
                var cake = await _db.Cake.Include(x => x.Ingredients).FirstOrDefaultAsync(x => x.Id == Id);

                cake.Name = CakeCreationDto.Name;
                cake.CategoryId = CakeCreationDto.CategoryId;
                CakeCreationDto.Photo?.UploadPhoto(cake.Photo, _webHostEnvironment.ContentRootPath, (photo) =>
                {
                    if (photo != null)
                    {
                        cake.Photo = photo;
                    }
                });

                if (CakeCreationDto.Ingredients?.Any() == true || cake.Ingredients.Any())
                {
                    if (cake.Ingredients.Any())
                    {
                        _db.Ingredient.RemoveRange(cake.Ingredients);
                    }

                    cake.Ingredients = CakeCreationDto.Ingredients.Select(x => new Ingredient()
                    {
                        ProductId = x.ProductId.Value,
                        Quantity = x.Quantity.Value,
                        Unid = x.Unit.Value,
                        CreatedAt = DateTime.Now,
                        CreatedBy = userId,
                    }).ToList();
                }
                
                _db.Cake.Update(cake);
            }
            else
            {
                var cake = new Entity.Cake
                {
                    Name = CakeCreationDto.Name,
                    CategoryId = CakeCreationDto.CategoryId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId
                };

                if (CakeCreationDto.Ingredients?.Any() == true)
                {
                    cake.Ingredients = CakeCreationDto.Ingredients.Select(x => new Ingredient()
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity.Value,
                        Unid = x.Unit.Value,
                        CreatedAt = DateTime.Now,
                        CreatedBy = userId,
                    }).ToList();
                }

                CakeCreationDto.Photo?.UploadPhoto(null, _webHostEnvironment.ContentRootPath, (photo) => cake.Photo ??= photo);
                _db.Cake.Add(cake);
            }

            await _db.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
