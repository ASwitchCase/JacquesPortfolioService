using FluentValidation;

public class UpdatePortfolioProjectValidator : AbstractValidator<UpdatePortfolioProjectCommand>
{
    public UpdatePortfolioProjectValidator()
    {
        RuleFor(x => x.projectId).NotEmpty();
        RuleFor(x => x.title).NotEmpty();
        RuleFor(x => x.spec).NotEmpty();
        RuleFor(x => x.detail).NotEmpty();
    }
}
