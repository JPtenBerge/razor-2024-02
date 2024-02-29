using DemoProject.DataAccess;
using Demo.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoProject.Repositories;

public class CharacterDbRepository : ICharacterRepository
{
    private readonly AvatarContext _context;
    public CharacterDbRepository(AvatarContext context)
    {
        _context = context;
    }
    
    public IEnumerable<Character> GetAll()
    {
        return _context.Characters.Include(x => x.Nation).ToArray();
    }

    public void Add(Character newCharacter)
    {
        _context.Characters.Add(newCharacter);
        _context.SaveChanges();
    }

    public void Update(Character character)
    {
        var charEntity = _context.Characters.Single(x => x.Id == character.Id);
        charEntity.IsBender = character.IsBender;
        
        // _context.Entry<Character>(character).State = EntityState.Modified;
        _context.SaveChanges();
    }
}