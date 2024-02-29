using System.Linq.Expressions;
using Demo.Shared.Entities;

namespace DemoProject.Repositories;

public class CharacterRepository : ICharacterRepository
{
    private List<Character> _avatarCharacters { get; set; } = new List<Character>()
    {
        new()
        {
            Id = 4,
            Name = "Aang",
            IsBender = true,
            Elements = { "Earth", "Air", "Fire", "Water" },
            PhotoUrl =
                "https://oyster.ignimgs.com/mediawiki/apis.ign.com/avatar-the-last-airbender/b/b0/Aang_img.jpg?width=325"
        },
        new()
        {
            Id = 8,
            Name = "Sokka",
            IsBender = false,
            Elements = null,
            PhotoUrl =
                "https://oyster.ignimgs.com/mediawiki/apis.ign.com/avatar-the-last-airbender/a/ad/Sokka_img.jpg?width=325"
        },
        new()
        {
            Id = 15,
            Name = "Katara",
            IsBender = true,
            Elements = { "Water" },
            PhotoUrl =
                "https://pics.craiyon.com/2023-10-02/bb59b64d2b7b4262bba96d775dcc2e3a.webp"
        }
    };
    
    public IEnumerable<Character> GetAll()
    {
        return _avatarCharacters;
    }

    public void Add(Character newCharacter)
    {
        newCharacter.Id = _avatarCharacters.Max(x => x.Id) + 1;
        _avatarCharacters.Add(newCharacter);
    }

    // public IEnumerable<Character> Get(int pageNr, int nrPerPage)
    // {
    //     
    // }
    // public IQueryable<Character> GetFiltered()
    // {
    //     
    // }
    //
    // public IEnumerable<Character> GetFiltered(Expression<Func<Character, bool>> where)
    // {
    //     list.Where(where);
    // }
    // public IEnumerable<Character> GetAllUglyCharacters()
    // {
    //     
    // }
    // filteren
    // sortering
    // skip
    // max zoveel
    
    // heel veel methoden
    // heel veel parameters voor alle bewerksoperaties
    // IQueryable
}