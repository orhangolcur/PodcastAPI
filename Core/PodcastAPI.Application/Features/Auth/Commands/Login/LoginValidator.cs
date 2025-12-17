using FluentValidation;

namespace PodcastAPI.Application.Features.Auth.Commands.Login
{
    public class LoginValidator : AbstractValidator<Login.Command>
    {
        public LoginValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
