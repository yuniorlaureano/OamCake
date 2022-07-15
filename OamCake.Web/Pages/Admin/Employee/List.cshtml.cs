using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Common;
using OamCake.Data;
using OamCake.Data.Dto;

namespace OamCake.Web.Pages.Admin.Employee
{
    [Authorize]
    public class ListModel : PageModel
    {
        private readonly OamCakeContext _db;
        public TableResponse<Entity.Employee> EmployeeTable { get; set; } = new();

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
            var query = _db.Employee.Where(x => x.DeletedAt == null);

            if (!String.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(x => x.Name.Contains(Search) || x.LastName.Contains(Search));
            }

            EmployeeTable.Data = await query
                                        .Skip((Pages) * 20)
                                        .Take(20).ToListAsync();
            EmployeeTable.Count = await query.CountAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var userId = Int32.Parse(User.Claims.FirstOrDefault(p => p.Type == Constants.CLAIM_ID).Value);
            var employee = await _db.Employee.FirstOrDefaultAsync(x => x.Id == Id);
            if (employee != null)
            {
                employee.DeletedBy = userId;
                employee.DeletedAt = DateTime.UtcNow;
                _db.Update(employee);
                await _db.SaveChangesAsync();
            }

            return RedirectToPage("/admin/employee/list", new { Pages, Search });
        }
    }
}
