using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Common;
using OamCake.Data;
using OamCake.Data.Dto;

namespace OamCake.Web.Pages.Admin.Client
{
    [Authorize]
    public class ListModel : PageModel
    {
        private readonly OamCakeContext _db;
        public TableResponse<Entity.Client> ClientTable { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int Pages { get; set; }

        [BindProperty]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Search { get; set; }

        public ListModel(OamCakeContext db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
            var query = _db.Client.Where(x => x.DeletedAt == null);

            if (!String.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(x => x.Name.Contains(Search) || x.LastName.Contains(Search));
            }

            ClientTable.Data = await query
                                        .Skip((Pages) * 20)
                                        .Take(20).ToListAsync();
            ClientTable.Count = await query.CountAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(p => p.Type == Constants.CLAIM_ID).Value);
            var client = await _db.Client.FirstOrDefaultAsync(x => x.Id == Id);
            if (client != null)
            {
                client.DeletedBy = userId;
                client.DeletedAt = DateTime.UtcNow;
                _db.Update(client);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("/admin/client/list", new { Pages, Search });
        }
    }
}
