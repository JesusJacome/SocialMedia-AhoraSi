using FluentValidation;
using SocialMedia.Core.DTOs;

namespace SocialMedia.Infrastructure.Validators
{
    public class PostValidator : AbstractValidator<PostDTO>
    {
        public PostValidator()
        {
            RuleFor(post => post.Description)
            .NotNull()
            .Length(0, 500);

            RuleFor(post => post.Date)
            .NotNull();
            
            
            
        }
    }
}
