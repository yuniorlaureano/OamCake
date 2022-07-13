using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OamCake.Common.Helpers;
using OamCake.Data;
using OamCake.Data.Dto;
using OamCake.Entity;

namespace OamCake.Web.Pages.Account
{
    public class SignUpModel : PageModel
    {
        [BindProperty]
        public UserCreationDto UserCreationDto { get; set; } = new UserCreationDto();
        public OamCakeContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public SignUpModel(OamCakeContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task OnGetAsync()
        {
            if (Id > 0)
            {
                var user = await _db.User.Include(x => x.Employee).FirstOrDefaultAsync(x => x.Id == Id);
                if (user != null)
                {
                    UserCreationDto.Id = user.Id;
                    UserCreationDto.EmployeeId = user.EmployeeId;
                    UserCreationDto.Email = user.Email;
                    UserCreationDto.Name = user.Employee.Name;
                    UserCreationDto.Address = user.Employee.Address;
                    UserCreationDto.LastName = user.Employee.LastName;
                    UserCreationDto.Phone = user.Employee.Phone;
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (UserCreationDto.Id > 0)
            {
                var user = await _db.User.Include(x => x.Employee).FirstOrDefaultAsync(x => x.Id == Id);

                user.Email = UserCreationDto.Email;
                user.Employee.Name = UserCreationDto.Name;
                user.Employee.Address = UserCreationDto.Address;
                user.Employee.Phone = UserCreationDto.Phone;
                user.Employee.LastName = UserCreationDto.LastName;

                UserCreationDto.Photo.UploadPhoto(user.Employee.Photo, _webHostEnvironment.ContentRootPath, (photo) =>
                {
                    user.Employee.Photo ??= photo;
                });

                _db.User.Add(user);
            }
            else
            {
                var user = new User
                {
                    Email = UserCreationDto.Email,
                    Password = UserCreationDto.Password.HashedPassword(),
                    Employee = new Employee()
                    {
                        Name = UserCreationDto.Name,
                        Address = UserCreationDto.Address,
                        Phone = UserCreationDto.Phone,
                        LastName = UserCreationDto.LastName
                    }
                };

                UserCreationDto.Photo.UploadPhoto(null, _webHostEnvironment.ContentRootPath, (photo) =>
                {
                    user.Employee.Photo ??= photo;
                });

                _db.User.Add(user);
            }

            await _db.SaveChangesAsync();
            return RedirectToPage("/Account/Login");
        }
    }
}
