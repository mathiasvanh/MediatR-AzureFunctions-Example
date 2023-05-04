using AutoMapper;
using MediatRExample.Common.Queries;
using MediatRExample.Databases.Quiz.Repositories;
using MediatRExample.Modules.Quiz.Dto;
using Microsoft.EntityFrameworkCore;

namespace MediatRExample.Modules.Quiz.Queries.GetQuestion;

public class GetQuestionQueryHandler : QueryHandler<GetQuestionQuery, GetQuestionQueryResult>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IMapper _mapper;

    public GetQuestionQueryHandler(IQuestionRepository questionRepository, IMapper mapper)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
    }

    public override async Task<GetQuestionQueryResult> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
    {
        var questions = _questionRepository.AllAsNoTracking().Include(x => x.Category);
        var results = await questions.Select(o => _mapper.Map<QuestionDto>(o)).ToListAsync(cancellationToken);

        return new GetQuestionQueryResult(results);
    }
}