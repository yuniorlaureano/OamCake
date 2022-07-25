using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OamCake.Data.Dto
{
    public class CakeCreationDto
    {
        public long? Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; }
        public IFormFile? Photo { get; set; }
        public long? CategoryId { get; set; }

        public string? StrPhoto { get; set; }
        public List<IngredientDto> Ingredients { get; set; } = new();
    }
}
