using Demo.Shared.Entities;

namespace Demo.Shared.Dtos;

public record CharacterPostRequestDto
{
    public string Name { get; set; }
    public bool IsBender { get; set; }
    public string PhotoUrl { get; set; }
    public int NationId { get; set; }
}

public static class CharacterPostRequestDtoExtensions
{
    public static Character MapToEntity(this CharacterPostRequestDto dto)
    {
        return new Character
        {
            Name = dto.Name,
            IsBender = dto.IsBender,
            PhotoUrl = dto.PhotoUrl,
            NationId = dto.NationId
        };
    }
}