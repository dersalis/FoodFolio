using FluentValidation;
using FoodFolio.WebApi.Dtos;

namespace FoodFolio.WebApi.Validators
{
    public class CreateDishTypeValidator : AbstractValidator<CreateDishTypeDto>
    {
		public CreateDishTypeValidator()
		{
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nazwa is required")
                .MinimumLength(1).WithMessage("Name minimum lenght is 1")
                .MaximumLength(50).WithMessage("Name maximum lenght is 50");

            RuleFor(x => x.Description)
                .MaximumLength(50).WithMessage("Description maximum lenght is 50");
        }
	}
}

