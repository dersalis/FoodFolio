using FluentValidation;
using FoodFolio.WebApi.Dtos;
using FoodFolio.WebApi.Entities;

namespace FoodFolio.WebApi.Validators;

public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
{
	public RegisterUserValidator(FoodFolioDbContext dbContext)
	{
		RuleFor(x => x.Email)
			.NotEmpty().WithMessage("Email is required")
			.EmailAddress().WithMessage("User email format")
			.Custom((value, context) => {
                var emailInUse = dbContext.Users.Any(u => u.Email == value);
                if (emailInUse) context.AddFailure("Email", "Ten email jest w użyciu!");
            });

        RuleFor(x => x.Password)
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");

        RuleFor(x => x.ConfirmPassword)
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).")
            .Equal(p => p.Password).WithMessage("Password and ConfirmPassword must be equal");
    }
}

