using Microsoft.AspNetCore.Components;
using TekkenAppAssembly.Dtos;
using TekkenAppAssembly.Services;

namespace TekkenAppAssembly.Pages
{
    public partial class Characters
    {
        [Inject]
        private ICharactersService CharactersService { get; set; } = default!;

        private IEnumerable<CharacterReadDto> characters = Enumerable.Empty<CharacterReadDto>();

        protected override async Task OnInitializedAsync()
        {
            characters = await CharactersService.GetAllCharacters();
        }
    }
}
