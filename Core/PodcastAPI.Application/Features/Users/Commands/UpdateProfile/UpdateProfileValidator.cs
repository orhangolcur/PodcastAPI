using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastAPI.Application.Features.Users.Commands.UpdateProfile
{
    public class UpdateProfileValidator : AbstractValidator<UpdateProfile.Command>
    {
        public UpdateProfileValidator() 
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz");
            RuleFor(x => x.Bio)
                .MaximumLength(200).WithMessage("Biyografi 500 karakterden fazla olamaz");
        }
    }
}
