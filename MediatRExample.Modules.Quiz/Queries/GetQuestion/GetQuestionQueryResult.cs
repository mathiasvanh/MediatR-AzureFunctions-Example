using MediatRExample.Common.Queries;
using MediatRExample.Modules.Quiz.Dto;

namespace MediatRExample.Modules.Quiz.Queries.GetQuestion;

public class GetQuestionQueryResult : QueryResult
{
    public List<QuestionDto> Results { get; }

    public GetQuestionQueryResult() : base(false)
    {
        Results = new List<QuestionDto>();
    }

    public GetQuestionQueryResult(List<QuestionDto> results) : base(true)
    {
        Results = results;
    }
}