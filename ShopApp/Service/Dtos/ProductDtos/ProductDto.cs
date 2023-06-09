﻿using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Service.Dtos.ProductDtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CostPrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public IFormFile File { get; set; }
    }

    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(30).MinimumLength(2);
            RuleFor(x => x.SalePrice).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(x => x.DiscountPercent).NotNull().GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
            RuleFor(x => x.CostPrice).NotNull().LessThanOrEqualTo(x => x.SalePrice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.BrandId).NotEmpty();


            RuleFor(x => x).Custom((x, context) =>
            {
                if ((x.SalePrice * (100 - x.DiscountPercent) / 100) < x.CostPrice)
                {
                    context.AddFailure(nameof(ProductDto.DiscountPercent), "Discount percent must be less");
                }
            });
        }
    }
}
