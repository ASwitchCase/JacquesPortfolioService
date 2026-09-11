using FluentValidation;

public class UpdateEducationValidator : AbstractValidator<UpdateEducationCommand>
{
    public UpdateEducationValidator()
    {
        RuleFor(x => x.educationId).NotEmpty();
        RuleFor(x => x.institution).NotEmpty();
        RuleFor(x => x.degree).NotEmpty();
        RuleFor(x => x.fieldOfStudy).NotEmpty();
        RuleFor(x => x.startDate).NotEmpty();
        RuleFor(x => x.description).NotEmpty();
    }
}
