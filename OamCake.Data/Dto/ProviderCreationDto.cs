using OamCake.Entity;
using System.ComponentModel.DataAnnotations;

namespace OamCake.Data.Dto
{
    public class ProviderCreationDto
    {
        public short Id { get; set; }
        [Required(ErrorMessage ="El nombre es requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "La dirección es requerida")]
        public string Address { get; set; }

        [Required(ErrorMessage = "El teléfono es requerido")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        public string Email { get; set; }

        public ProviderCreationDto()
        {

        }

        public ProviderCreationDto(Provider provider)
        {
            Id = provider.Id;
            Name = provider.Name;
            Address = provider.Address;
            Phone = provider.Phone;
            Email = provider.Email;
        }
    }
}
