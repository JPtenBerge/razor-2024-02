﻿using DemoProject.Entities;
using FluentValidation;

namespace DemoProject.Validators;

public class CharacterValidator : AbstractValidator<Character>
{
    public CharacterValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Een naam graag")
            .Matches("^[a-zA-Z -]+$").WithMessage("Alleen letters, spaties en streepjes graag");
    
        RuleFor(x => x.PhotoUrl)
            .NotEmpty().WithMessage("Vul in aub");
    }
}