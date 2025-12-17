using FluentValidation;
using System;

namespace PodcastAPI.Application.Features.Podcasts.Commands.CreatePodcast
{
    public class CreatePodcastValidator : AbstractValidator<CreatePodcast.Command>
    {
        public CreatePodcastValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Podcast title cannot be empty.")
                .NotNull()
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description field is required.");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("You must select a category.")
                .NotEqual("General").WithMessage("Please select a valid category other than 'General'.");

            RuleFor(x => x.RssUrl)
                .NotEmpty().WithMessage("RSS link is required.")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("Please enter a valid URL address.");
        }
    }
}