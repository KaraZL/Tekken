using AutoMapper;
using CharactersAPI.Dtos;
using CharactersAPI.Models;

namespace CharactersAPI.Profiles
{
    public class CharactersProfiles : Profile
    {
        public CharactersProfiles()
        {
            CreateMap<CharacterDto, Character>();
        }
    }
}
