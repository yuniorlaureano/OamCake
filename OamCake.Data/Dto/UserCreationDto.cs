using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OamCake.Data.Dto
{
    public class UserCreationDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "La dirección es requerido")]
        public string Address { get; set; }

        [Required(ErrorMessage = "El teléfono es requerido")]
        public string Phone { get; set; }

        public IFormFile? Photo { get; set; }

        [Required(ErrorMessage = "El correo es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerido")]
        public string Password { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }

        public bool IsActive { get; set; }
        public int EmployeeId { get; set; }
    }
}
