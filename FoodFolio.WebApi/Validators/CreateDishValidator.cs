using FluentValidation;
using FoodFolio.WebApi.Dtos;

namespace FoodFolio.WebApi.Validators
{
    public class CreateDishValidator : AbstractValidator<CreateDishDto>
	{
		public CreateDishValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Nazwa is required")
				.MinimumLength(1).WithMessage("Name minimum lenght is 1")
				.MaximumLength(50).WithMessage("Name maximum lenght is 50");

            RuleFor(x => x.Description)
                .MaximumLength(50).WithMessage("Description maximum lenght is 50");

            RuleFor(x => x.Price)
				.NotEmpty().WithMessage("Price is required")
				.GreaterThan(0).WithMessage("Price must be greater than 0");

			RuleFor(x => x.ServingDate)
				.NotEmpty().WithMessage("ServingDate is required")
				.GreaterThan(DateTime.Now.Date).WithMessage($"ServingDate must by greater than {DateTime.Now.Date.ToString("yyyy-MM-dd HH-mm")}");

			RuleFor(x => x.DishTypeId)
				.NotEmpty().WithMessage("DishType is required");
		}
	}
}

