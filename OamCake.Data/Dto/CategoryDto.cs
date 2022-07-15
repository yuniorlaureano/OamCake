using OamCake.Entity;
using System.ComponentModel.DataAnnotations;

namespace OamCake.Data.Dto
{
    public class CategoryDto
    {
        public long Id { get; set; }

        [Display(Name = "Nombre")]

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; }

        public CategoryDto() { }

        public CategoryDto(Category client)
        {
            Id = client.Id;
            Name = client.Name;
        }
    }
}
