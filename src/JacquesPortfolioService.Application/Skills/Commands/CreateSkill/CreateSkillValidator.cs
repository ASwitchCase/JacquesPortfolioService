using FluentValidation;

public class CreateSkillValidator : AbstractValidator<CreateSkillCommand>
{
    public CreateSkillValidator()
    {
        RuleFor(x => x.skillName).NotEmpty();
    }
}