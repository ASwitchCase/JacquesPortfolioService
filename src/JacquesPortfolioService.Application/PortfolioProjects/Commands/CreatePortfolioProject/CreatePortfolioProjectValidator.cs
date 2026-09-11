using FluentValidation;

public class CreatePortfolioProjectValidator : AbstractValidator<CreatePortfolioProjectCommand>
{
    public CreatePortfolioProjectValidator()
    {
        RuleFor(x => x.title).NotEmpty();
        RuleFor(x => x.spec).NotEmpty();
        RuleFor(x => x.detail).NotEmpty();
    }
}
