using System.ComponentModel.DataAnnotations;

namespace CharactersAPI.Dtos
{
    public class CharacterDto
    {
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
