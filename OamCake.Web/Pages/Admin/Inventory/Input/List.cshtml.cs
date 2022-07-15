using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Common;
using OamCake.Data;
using OamCake.Data.Dto;
using OamCake.Repository;

namespace OamCake.Web.Pages.Admin.Inventory.Input
{
    [Authorize]
    public class ListModel : PageModel
    {
        public TableResponse<MovementDto> InputTable { get; set; } = new();
        private readonly IInventoryRepository _inventoryRepository;
        private readonly OamCakeContext _db;

        [BindProperty(SupportsGet = true)]
        public int Pages { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FIlter { get; set; }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public ListModel(IInventoryRepository inventoryRepository, OamCakeContext db)
        {
            _inventoryRepository = inventoryRepository;
            _db = db;
        }

        public async Task OnGet()
        {
            InputTable = await _inventoryRepository.GetInputMovementAsync(Search, Pages, 20);
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(p => p.Type == Constants.CLAIM_ID).Value);
            var inventory = await _db.Inventory.FirstOrDefaultAsync(x => x.Id == Id);
            if (inventory != null)
            {
                inventory.DeletedBy = userId;
                inventory.DeletedAt = DateTime.UtcNow;
                _db.Update(inventory);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("/admin/inventory/input/list", new { Pages, Search });
        }
    }
}
