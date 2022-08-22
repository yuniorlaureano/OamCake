using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Common;
using OamCake.Data;
using OamCake.Data.Dto;

namespace OamCake.Web.Pages.Admin.Projection
{
    [Authorize]
    public class AddModel : PageModel
    {
        private readonly OamCakeContext _db;
        public TableResponse<ProjectionDetailDto> CakeTable { get; set; } = new();
        public IEnumerable<Entity.Category> Categories { get; set; } = Enumerable.Empty<Entity.Category>();

        [BindProperty]
        public ProjectionCreationDto Projection { get; set; } = new();
                
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
            var query = _db.Cake.Include(x => x.Category)
                           .Include(x => x.Ingredients)
                           .ThenInclude(x => x.Product).Where(x => x.DeletedAt == null);

            var projectionDetails = new Dictionary<long, Entity.ProjectionDetail>();

            if (Id != null && Id != 0)
            {
                projectionDetails = await _db.ProjectionDetail
                            .Include(x => x.Cake)
                            .Where(x => x.ProjectionId == Id)
                            .ToDictionaryAsync(x => x.CakeId);

                var projection = await _db.Projection.FirstOrDefaultAsync(x => x.Id == Id);

                Projection = new()
                {
                    Id = projection.Id,
                    Description = projection.Description,
                    IsOpen = projection.IsOpen,
                    CakesId = projectionDetails.Values.Select(x => x.CakeId).ToArray()
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
            CakeTable.Data = new List<ProjectionDetailDto>();

            foreach (var item in cakes)
            {
                projectionDetails.TryGetValue(item.Id, out Entity.ProjectionDetail cake);

                CakeTable.Data.Add(new ProjectionDetailDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    CategoryId = item.CategoryId,
                    CategoryName = item.Category?.Name,                   
                    IsSet = cake != null,
                    Photo = item.Photo,
                    Quantity = cake?.Quantity ?? 0
                });
            }

            Categories = await _db.Category.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var x = await Task.FromResult(0);

            if (String.IsNullOrWhiteSpace(Projection.Description))
            {
                Errors = "Debe proveer la descripción";
                return RedirectToPage("/admin/projection/add", new { Search, Id, CategoryId });
            }

            var userId = Int32.Parse(User.Claims.FirstOrDefault(p => p.Type == Constants.CLAIM_ID).Value);

            if (Projection.Id > 0)
            {
                var projection = await _db.Projection.Where(x => x.Id == Projection.Id).FirstOrDefaultAsync();
                var details = await _db.ProjectionDetail.Where(x => x.ProjectionId == Projection.Id).ToListAsync();

                if (projection != null)
                {
                    if (details != null)
                    {
                        
                        var existing = details.Where(x => Projection.CakesId.Contains(x.CakeId)).ToList();
                        var removed = details.Where(x => !Projection.CakesId.Contains(x.CakeId)).ToList();
                        var news = Projection.CakesId.Where(x => !existing.Any(j => j.CakeId == x)).ToList();

                        if (removed.Any())
                        {
                            _db.ProjectionDetail.RemoveRange(removed);
                        }

                        if (news.Any())
                        {
                            _db.ProjectionDetail.AddRange(news.Select(x => new Entity.ProjectionDetail
                            {
                                CakeId = x,
                                CreatedAt = DateTime.Now,
                                CreatedBy = userId,
                                ProjectionId = projection.Id,
                            }).ToList());
                        }
                    }

                    projection.Description = Projection.Description;
                    projection.IsOpen = Projection.IsOpen;
                    projection.UpdatedAt = DateTime.Now;
                    projection.UpdatedBy = userId;

                    _db.Projection.Update(projection);
                    await _db.SaveChangesAsync();
                }
            }
            else
            {
                var projection = new Entity.Projection
                {
                    Description = Projection.Description,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId,
                    IsOpen = Projection.IsOpen,
                    ProjectionDetails = Projection.CakesId.Select(x => new Entity.ProjectionDetail
                    {
                        CakeId = x,
                        CreatedAt = DateTime.Now,
                        CreatedBy = userId
                    }).ToList(),
                };

                _db.Projection.Add(projection);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("/admin/projection/add", new { Search, Id, CategoryId });

        }
    }
}
