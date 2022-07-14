using Microsoft.AspNetCore.Components;
using TekkenApp.Dtos;
using TekkenApp.Services;

namespace TekkenApp.Pages
{
    public partial class Characters
    {
        [Inject]
        private ICharactersService CharactersService { get; set; } = default!;

        private IEnumerable<CharacterReadDto> characters;

        protected override async Task OnInitializedAsync()
        {
            characters = await CharactersService.GetAllCharacters();
        }

    }

}
