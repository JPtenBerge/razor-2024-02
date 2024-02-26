using DemoProject.DataAccess;
using DemoProject.Entities;
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
}