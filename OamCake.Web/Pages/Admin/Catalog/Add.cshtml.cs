using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Common;
using OamCake.Data;
using OamCake.Data.Dto;

namespace OamCake.Web.Pages.Admin.Catalog
{
    [Authorize]
    public class AddModel : PageModel
    {
        private readonly OamCakeContext _db;
        public TableResponse<CatalogDetailsDto> CakeTable { get; set; } = new();
        public IEnumerable<Entity.Category> Categories { get; set; } = Enumerable.Empty<Entity.Category>();

        [BindProperty]
        public CatalogCreationDto Catalog { get; set; } = new();
                
        public AddModel(OamCakeContext db)
        {
            _db = db;
        }
                
        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }

        public string Errors { get; set; }

        public async Task OnGet()
        {
            var query = _db.Cake.Include(x => x.Category).Where(x => x.DeletedAt == null);
            var catalogDetails = new Dictionary<long, Entity.CatalogDetail>();

            if (Id != null && Id != 0)
            {
                catalogDetails = await _db.CatalogDetail
                            .Include(x => x.Cake)
                            .Where(x => x.CatalogId == Id)
                            .ToDictionaryAsync(x => x.CakeId);

                var catalog = await _db.Catalog.FirstOrDefaultAsync(x => x.Id == Id);

                Catalog = new()
                {
                    Id = catalog.Id,
                    Description = catalog.Description,
                    IsPublished = catalog.IsPublished,
                    CakesId = catalogDetails.Values.Select(x => $"{x.CakeId}|{x.Price}").ToArray()
                };
            }

            if (!String.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(x => x.Name.Contains(Search));
            }

            if (CategoryId != 0 && CategoryId != null)
            {
                query = query.Where(x => x.CategoryId == CategoryId);
            }


            var cakes = await query.ToListAsync();
            CakeTable.Data = new List<CatalogDetailsDto>();

            foreach (var item in cakes)
            {
                catalogDetails.TryGetValue(item.Id, out Entity.CatalogDetail cake);

                CakeTable.Data.Add(new CatalogDetailsDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    CategoryId = item.CategoryId,
                    CategoryName = item.Category?.Name,                   
                    IsSet = cake != null,
                    Photo = String.IsNullOrEmpty(cake?.Photo)? item.Photo : cake?.Photo,
                    Price = cake?.Price ?? 0
                });
            }

            Categories = await _db.Category.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var x = await Task.FromResult(0);

            if (String.IsNullOrWhiteSpace(Catalog.Description))
            {
                Errors = "Debe proveer la descripción";
                return RedirectToPage("/admin/catalog/add", new { Search, Id, CategoryId });
            }

            var userId = Int32.Parse(User.Claims.FirstOrDefault(p => p.Type == Constants.CLAIM_ID).Value);
            
            var cakesIds = Catalog.CakesId.Select(x => new CakesIdHolder
            {
                CakeId = long.Parse(x.Split("|")[0]),
                Price = decimal.Parse(String.IsNullOrEmpty(x.Split("|")[1]) ? "0": x.Split("|")[1])
            }).ToList();

            if (Catalog.Id > 0)
            {
                var catalog = await _db.Catalog.Where(x => x.Id == Catalog.Id).FirstOrDefaultAsync();
                var details = await _db.CatalogDetail.Where(x => x.CatalogId == Catalog.Id).ToListAsync();

                if (catalog != null)
                {
                    if (details != null)
                    {                        
                        if (details.Any())
                        {
                            _db.CatalogDetail.RemoveRange(details);
                        }

                        if (details.Any())
                        {
                            _db.CatalogDetail.AddRange(cakesIds.Select(x => new Entity.CatalogDetail
                            {
                                CakeId = x.CakeId,
                                CreatedAt = DateTime.Now,
                                CreatedBy = userId,
                                CatalogId = catalog.Id,
                                Photo = "",
                                Price = x.Price
                            }).ToList());
                        }
                    }

                    catalog.Description = Catalog.Description;
                    catalog.IsPublished = Catalog.IsPublished;
                    catalog.UpdatedAt = DateTime.Now;
                    catalog.UpdatedBy = userId;

                    _db.Catalog.Update(catalog);
                    await _db.SaveChangesAsync();
                }
            }
            else
            {
                var catalog = new Entity.Catalog
                {
                    Description = Catalog.Description,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId,
                    IsPublished = Catalog.IsPublished,
                    PublishDate = DateTime.Now,
                    CatalogDetails = cakesIds.Select(x => new Entity.CatalogDetail
                    {
                        CakeId = x.CakeId,
                        CreatedAt = DateTime.Now,
                        CreatedBy = userId,
                        Photo = "",
                        Price= x.Price
                    }).ToList(),
                };

                _db.Catalog.Add(catalog);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("/admin/catalog/add", new { Search, Id, CategoryId });

        }
    }

    class CakesIdHolder
    {
        public long CakeId { get; set; }
        public decimal Price { get; set; }
    }
}
