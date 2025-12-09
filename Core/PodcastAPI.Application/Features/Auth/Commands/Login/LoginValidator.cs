using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastAPI.Application.Features.Auth.Commands.Login
{
    public class LoginValidator : AbstractValidator<Login.Command>
    {
        public LoginValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta boş olamaz")
                .EmailAddress().WithMessage("Geçerli bir e-posta formatı değil");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre zorunludur.");
        }
    }
}
