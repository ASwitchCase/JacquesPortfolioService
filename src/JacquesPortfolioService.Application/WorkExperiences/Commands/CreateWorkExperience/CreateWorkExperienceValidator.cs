using FluentValidation;

public class CreateWorkExperienceValidator : AbstractValidator<CreateWorkExperienceCommand>
{
    public CreateWorkExperienceValidator()
    {
        RuleFor(x => x.company).NotEmpty();
        RuleFor(x => x.title).NotEmpty();
        RuleFor(x => x.startDate).NotEmpty();
        RuleFor(x => x.description).NotEmpty();
        RuleFor(x => x.location).NotEmpty();
    }
}
