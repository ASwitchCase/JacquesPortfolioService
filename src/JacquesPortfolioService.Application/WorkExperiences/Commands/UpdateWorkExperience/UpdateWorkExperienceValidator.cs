using FluentValidation;

public class UpdateWorkExperienceValidator : AbstractValidator<UpdateWorkExperienceCommand>
{
    public UpdateWorkExperienceValidator()
    {
        RuleFor(x => x.experienceId).NotEmpty();
        RuleFor(x => x.company).NotEmpty();
        RuleFor(x => x.title).NotEmpty();
        RuleFor(x => x.startDate).NotEmpty();
        RuleFor(x => x.description).NotEmpty();
        RuleFor(x => x.location).NotEmpty();
    }
}
