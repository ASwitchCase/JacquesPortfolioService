using FluentValidation;

public class UpdateSkillValidator : AbstractValidator<UpdateSkillCommand>
{
    public UpdateSkillValidator()
    {
        RuleFor(x => x.skillId).NotEmpty();
        RuleFor(x => x.skillName).NotEmpty();
    }
}
