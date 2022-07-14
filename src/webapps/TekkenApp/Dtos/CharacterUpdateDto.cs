using System.ComponentModel.DataAnnotations;

namespace TekkenApp.Dtos
{
    public class CharacterUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
