using MediatRExample.Common.Queries;

namespace MediatRExample.Modules.Quiz.Queries.GetQuestion;

public class GetQuestionQuery : IQuery<GetQuestionQueryResult>
{
    public int CategoryId { get; set; }
}