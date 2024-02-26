using DemoProject.DataAccess;
using DemoProject.Entities;

namespace DemoProject.Repositories;

public class NationDbRepository : INationRepository
{
    private readonly AvatarContext _context;

    public NationDbRepository(AvatarContext context)
    {
        _context = context;
    }

    public IEnumerable<Nation> GetAll()
    {
        return _context.Nations.ToArray();
    }
}