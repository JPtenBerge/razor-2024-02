using Demo.Shared.Entities;
using DemoProject.Repositories;

namespace DemoProject.Tests;

public class NepNationRepository : INationRepository
{
    public IEnumerable<Nation> GetAll()
    {
        return new List<Nation>();
    }
}