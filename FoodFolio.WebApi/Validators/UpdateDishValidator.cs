using FluentValidation;
using FoodFolio.WebApi.Dtos;

namespace FoodFolio.WebApi.Validators
{
    public class UpdateDishValidator : AbstractValidator<UpdateDishDto>
    {
		public UpdateDishValidator()
		{
            RuleFor(x => x.Name)
                .MaximumLength(50).WithMessage("Name maximum lenght is 50");

            RuleFor(x => x.Description)
                .MaximumLength(50).WithMessage("Description maximum lenght is 50");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.ServingDate)
                .GreaterThan(DateTime.Now.Date).WithMessage($"ServingDate must by greater than {DateTime.Now.Date.ToString("yyyy-MM-dd HH-mm")}");

            RuleFor(x => x.DishTypeId)
                .NotEmpty().WithMessage("DishType is required");
        }
	}
}

