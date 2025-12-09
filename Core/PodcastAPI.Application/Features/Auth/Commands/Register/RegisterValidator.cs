using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastAPI.Application.Features.Auth.Commands.Register
{
    public class RegisterValidator : AbstractValidator<Register.Command>
    {
        public RegisterValidator() 
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı zorunludur")
                .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakterli olmalıdır")
                .MaximumLength(50).WithMessage("Kullanıcı adı 50 karakterden fazla olamaz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre zorunludur")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır")
                .MaximumLength(20).WithMessage("Şifre 20 karakterden fazla olamaz");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta zorunludur")
                .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi giriniz");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Şifreler eşleşmiyor");
        }
    }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            
}
