﻿using FluentValidation;
using System.Data;

namespace Service.Dtos.BrandDtos
{
    public class BrandDto
    {
        public string Name { get; set; }
    }

    public class BrandDtoValidator : AbstractValidator<BrandDto>
    {
        public BrandDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("mecburidir!")
                .MaximumLength(20);
        }
    }



}
