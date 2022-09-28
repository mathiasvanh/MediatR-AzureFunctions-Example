using MediatRExample.Common.Queries;

namespace MediatRExample.Modules.Weather.Queries;

public class GetWeatherQueryResult : QueryResult
{
    public string Temp { get; set; }

    public GetWeatherQueryResult() : base(false)
    {
    }

    public GetWeatherQueryResult(string temp) : base(true)
    {
        Temp = temp;
    }
}