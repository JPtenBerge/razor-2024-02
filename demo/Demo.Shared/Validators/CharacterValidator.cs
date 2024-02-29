using Demo.Shared.Dtos;
using FluentValidation;

namespace Demo.Shared.Validators;

public class CharacterValidator : AbstractValidator<CharacterPostRequestDto>
{
    public CharacterValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Een naam graag")
            .Matches("^[a-zA-Z -]+$").WithMessage("Alleen letters, spaties en streepjes graag");
    
        RuleFor(x => x.PhotoUrl)
            .NotEmpty().WithMessage("Vul in aub");

        RuleFor(x => x.NationId)
            .NotEmpty().WithMessage("Nation graag");
    }
}