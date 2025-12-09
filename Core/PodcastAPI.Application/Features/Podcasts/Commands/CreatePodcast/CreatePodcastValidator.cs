using FluentValidation;

namespace PodcastAPI.Application.Features.Podcasts.Commands.CreatePodcast
{
    public class CreatePodcastValidator : AbstractValidator<CreatePodcast.Command>
    {
        public CreatePodcastValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Podcast başlığı boş olamaz")
                .NotNull()
                .MaximumLength(100).WithMessage("Başlık 100 karakterden uzun olamaz");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Açıklama alanı zorunludur");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Kategori seçmelisiniz")
                .NotEqual("Genel").WithMessage("Lütfen geçerli bir kategori seçin");

            RuleFor(x => x.RssUrl)
                .NotEmpty().WithMessage("RSS linki zorunludur")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("Lütfen geçerli bir URL adresi giriniz");
        }
    }
}
