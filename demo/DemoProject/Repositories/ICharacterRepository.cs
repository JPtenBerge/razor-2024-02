using Demo.Shared.Entities;

namespace DemoProject.Repositories;

public interface ICharacterRepository
{
    IEnumerable<Character> GetAll();
    void Add(Character newCharacter);
}