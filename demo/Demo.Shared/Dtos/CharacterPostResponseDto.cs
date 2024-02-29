using Demo.Shared.Entities;

namespace Demo.Shared.Dtos;

public class CharacterPostResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsBender { get; set; }
    public string PhotoUrl { get; set; }
    public int NationId { get; set; }
    public string Nation { get; set; }
}

public static class CharacterPostResponseDtoExtensions
{
    public static CharacterPostResponseDto MapToDto(this Character entity)
    {
        return new CharacterPostResponseDto
        {
            Id = entity.Id,
            Name = entity.Name,
            IsBender = entity.IsBender,
            PhotoUrl = entity.PhotoUrl,
            NationId = entity.NationId,
            Nation = entity.Nation.Name
        };
    }
}
