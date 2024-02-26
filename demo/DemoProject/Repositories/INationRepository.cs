using DemoProject.Entities;

namespace DemoProject.Repositories;

public interface INationRepository
{
    IEnumerable<Nation> GetAll();
}