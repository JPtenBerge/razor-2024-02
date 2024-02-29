using Demo.Shared.Entities;
using DemoProject.Repositories;

namespace DemoProject.Tests;

public class NepCharacterRepository : ICharacterRepository
{
    public bool HasGetAllBeenCalled { get; set; }
    
    public IEnumerable<Character> GetAll()
    {
        HasGetAllBeenCalled = true;
        return new List<Character>
        {
            new() { Name = "Frank" },
            new() { Name = "Henk" },
            new() { Name = "Piet" }
        };
    }

    public void Add(Character newCharacter)
    {
    }
}