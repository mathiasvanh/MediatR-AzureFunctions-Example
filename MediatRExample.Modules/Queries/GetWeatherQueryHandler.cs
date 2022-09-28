using MediatRExample.Common.Queries;

namespace MediatRExample.Modules.Weather.Queries;

public class GetWeatherQueryHandler : QueryHandler<GetWeatherQuery, GetWeatherQueryResult>
{
    public override async Task<GetWeatherQueryResult> Handle(GetWeatherQuery request, CancellationToken cancellationToken)
    {
        return await Task.Run(() => new GetWeatherQueryResult("5"));
    }
}