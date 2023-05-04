using FluentValidation;

namespace MediatRExample.Modules.Quiz.Queries.GetQuestion;

public class GetQuestionQueryValidator : AbstractValidator<GetQuestionQuery>
{
    public GetQuestionQueryValidator()
    {
        RuleFor(q => q.CategoryId).NotEmpty().WithMessage("CountryCode should not be null or empty");
    }
}