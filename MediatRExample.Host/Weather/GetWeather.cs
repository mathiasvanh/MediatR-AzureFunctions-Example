using System;
using System.Threading.Tasks;
using MediatR;
using MediatRExample.Common.Queries;
using MediatRExample.Common.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MediatRExample.Modules.Weather.Queries;

namespace MediatRExample.Host.Weather;

public class GetWeather
{
    private readonly IMediator _mediator;
    private readonly ILogger<GetWeather> _logger;

    public GetWeather(IMediator mediator, ILogger<GetWeather> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [FunctionName(nameof(Weather_GetWeather))]
    public async Task<IActionResult> Weather_GetWeather(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)]
        HttpRequest request)
    {
        try
        {
            var query = new GetWeatherQuery();
            var queryResult = await _mediator.Send(query);

            if (queryResult.IsSuccess)
            {
                return new OkObjectResult(queryResult);
            }

            return new BadRequestObjectResult(queryResult);

        }
        catch (ValidationException ex)
        {
            _logger.LogWarning(ex, "[Weather_GetWeather] failed");
            return new BadRequestObjectResult(QueryResult.Failed<GetWeatherQueryResult>(ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "[Weather_GetWeather] Bad");
            // return new InternalServerErrorObjectResult(QueryResult.Failed<GetDiagnosticsQueryResult>(ex.Message));
            return new BadRequestResult();
        }
    }
}