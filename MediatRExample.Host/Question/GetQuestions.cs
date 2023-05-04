using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MediatRExample.Common.Queries;
using MediatRExample.Common.Utils;
using MediatRExample.Common.Validation;
using MediatRExample.Modules.Quiz.Queries.GetQuestion;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace MediatRExample.Host.Question;

public class GetQuestions
{
    private readonly ILogger<GetQuestions> _logger;
    private readonly IMediator _mediator;

    public GetQuestions(ILogger<GetQuestions> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [FunctionName(nameof(Question_GetQuestions))]
    public async Task<IActionResult> Question_GetQuestions(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
        HttpRequest request)
    {
        try
        {
            var query = new GetQuestionQuery()
            {
                CategoryId = request.Query["categoryId"].ToString().SplitToListOfInt().First() //todo bad
            };
            var queryResult = await _mediator.Send(query);

            if (queryResult.IsSuccess)
            {
                return new OkObjectResult(queryResult);
            }

            _logger.LogError($"[Question_GetQuestions] failed -> {queryResult.FailureReason}");
            return new BadRequestObjectResult(queryResult);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "[Weather_GetWeather] failed");
            return new BadRequestObjectResult(QueryResult.Failed<GetQuestionQueryResult>(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[Weather_GetWeather] Bad");
            // return new InternalServerErrorObjectResult(QueryResult.Failed<GetDiagnosticsQueryResult>(ex.Message));
            return new BadRequestResult();
        }
    }
}