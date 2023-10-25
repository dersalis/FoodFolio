using FluentValidation;
using FoodFolio.WebApi.Dtos;
using FoodFolio.WebApi.Entities;

namespace FoodFolio.WebApi.Validators;

public class QueryValidator : AbstractValidator<QueryDto>
{
    private int[] allowedPageSizes = new[] { 5, 10, 15 };
    private string[] allowedSortByColumnNames = new[]
    {
        nameof(Dish.Name),
        nameof(Dish.Price),
        nameof(Dish.Description)
    };

    public QueryValidator()
    {
        RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(r => r.PageSize).Custom((value, context) =>
        {
            if (!allowedPageSizes.Contains(value))
            {
                context.AddFailure("PageSize", $"Page size must in [{string.Join(", ", allowedPageSizes)}]");
            }
        });
        RuleFor(r => r.SortBy).Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
            .WithMessage($"Sort by is optional, or must be in [{string.Join(", ", allowedSortByColumnNames)}]");
    }
}