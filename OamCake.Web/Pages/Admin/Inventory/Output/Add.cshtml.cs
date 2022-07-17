using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OamCake.Common;
using OamCake.Data;
using OamCake.Data.Dto;
using OamCake.Repository;

namespace OamCake.Web.Pages.Admin.Inventory.Output
{
    [Authorize]
    public class Add : PageModel
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly OamCakeContext _db;

        [BindProperty]
        public StockCreationDto[] CreateStock { get; set; }
        public InventoryFlowDto InventoryFlowDto { get; set; } = new InventoryFlowDto();

        public Add(IInventoryRepository inventoryRepository, OamCakeContext db)
        {
            _inventoryRepository = inventoryRepository;
            _db = db;
        }

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public async Task OnGet()
        {
            InventoryFlowDto.Flow = Flow.Output;
            InventoryFlowDto.Stock = await _inventoryRepository.GetStockAsync(Search);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (CreateStock == null)
            {
                return Page();
            }

            var userId = Int32.Parse(User.Claims.FirstOrDefault(p => p.Type == Constants.CLAIM_ID).Value);

            var validStock = CreateStock
                    .Where(p => p.IsValid())
                    .Select(p => new Entity.Inventory
                    {
                        CreatedAt = DateTime.Now,
                        Quantity = p.Quantity > 0 ? p.Quantity * -1 : p.Quantity,
                        ProductId = p.ProductId,
                        CreatedBy = userId,
                    }); ;

            if (validStock.Any())
            {
                _db.Inventory.AddRange(validStock);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage(new { Search });
        }
    }
}
