using System.ComponentModel.DataAnnotations;

namespace TekkenAppAssembly.Dtos
{
    public class CharacterCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
