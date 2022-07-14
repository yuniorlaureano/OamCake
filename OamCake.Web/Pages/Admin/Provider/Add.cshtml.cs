using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Common;
using OamCake.Data;
using OamCake.Data.Dto;

namespace OamCake.Web.Pages.Admin.Provider
{
    [Authorize]
    public class AddModel : PageModel
    {
        private readonly OamCakeContext _db;

        [BindProperty]
        public ProviderCreationDto Provider { get; set; } = new();

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
                var provider = await _db.Provider.FirstOrDefaultAsync(x => x.Id == Id);
                if (provider != null)
                {
                    Provider = new(provider);
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

            if (Provider.Id > 0)
            {
                var provider = await _db.Provider.FirstOrDefaultAsync(x => x.Id == Id);

                provider.Name = Provider.Name;
                provider.Email = Provider.Email;
                provider.Phone = Provider.Phone;
                provider.Address = Provider.Address;
                provider.UpdatedAt = DateTime.Now;
                provider.UpdatedBy = userId;

                _db.Provider.Update(provider);
            }
            else
            {
                var provider = new Entity.Provider()
                {
                    Name = Provider.Name,
                    Phone = Provider.Phone,
                    Email = Provider.Email,
                    Address = Provider.Address,
                    CreatedAt = DateTime.Now,
                    CreatedBy = userId
                };

                _db.Provider.Add(provider);
            }

            await _db.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
