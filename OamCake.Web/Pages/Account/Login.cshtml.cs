using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Common;
using OamCake.Common.Helpers;
using OamCake.Data;
using OamCake.Data.Dto;
using System.Security.Claims;

namespace OamCake.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public LoginDto LoginDto { get; set; } = new LoginDto();
        public OamCakeContext _db;

        public LoginModel(OamCakeContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = _db.User.Include(x => x.Employee).FirstOrDefault(x => x.Email == LoginDto.Email && x.Password == LoginDto.Password.HashedPassword());

            if (user == null)
            {
                ModelState.AddModelError("Usuario", "El usuario es inválido");
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, $"{user.Employee.Name} {user.Employee.LastName}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(Constants.CLAIM_PHOTO, user.Employee.Photo ?? ""),
                new Claim(Constants.CLAIM_ID, user.Id.ToString())
            };

            if (user.IsAdmin)
            {
                claims.Add(new Claim(Constants.CLAIM_ADMIN, Constants.CLAIM_ADMIN));
            }

            var identity = new ClaimsIdentity(claims, Constants.AUTH_TYPE);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = LoginDto.RememberMe,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
            };

            await HttpContext.SignInAsync(Constants.AUTH_TYPE, claimsPrincipal, authenticationProperties);

            return RedirectToPage("/Index");
        }
    }
}
