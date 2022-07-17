using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OamCake.Data.Dto;
using OamCake.Repository;

namespace OamCake.Web.Pages.Admin
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public TableResponse<MovementSummaryDto> InputTable { get; set; } = new();

        private readonly IInventoryRepository _inventoryRepository;

        [BindProperty(SupportsGet = true)]
        public int Pages { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public string FIlter { get; set; }
        public int Inputs { get; set; }
        public int Outputs { get; set; }

        public IndexModel(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task OnGet()
        {
            InputTable = await _inventoryRepository.GetMovementSummaryAsync(Search, Pages, 20);
            Inputs = await _inventoryRepository.GetInputMovementDashboardCountAsync();
            Outputs = await _inventoryRepository.GetOutputMovementDashboardCountAsync();
        }
    }
}
