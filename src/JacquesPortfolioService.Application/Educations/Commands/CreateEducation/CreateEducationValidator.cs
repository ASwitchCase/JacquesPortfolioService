using FluentValidation;

public class CreateEducationValidator : AbstractValidator<CreateEducationCommand>
{
    public CreateEducationValidator()
    {
        RuleFor(x => x.institution).NotEmpty();
        RuleFor(x => x.degree).NotEmpty();
        RuleFor(x => x.fieldOfStudy).NotEmpty();
        RuleFor(x => x.startDate).NotEmpty();
        RuleFor(x => x.description).NotEmpty();
    }
}
