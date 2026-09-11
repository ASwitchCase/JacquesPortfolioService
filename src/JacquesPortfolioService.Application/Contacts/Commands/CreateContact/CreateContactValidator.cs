using FluentValidation;

public class CreateContactValidator : AbstractValidator<CreateContactCommand>
{
    public CreateContactValidator()
    {
        RuleFor(x => x.name).NotEmpty();
        RuleFor(x => x.email).NotEmpty().EmailAddress();
        RuleFor(x => x.company).NotEmpty();
        RuleFor(x => x.message).NotEmpty();
    }
}
