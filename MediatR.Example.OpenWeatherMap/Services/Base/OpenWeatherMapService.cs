using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace MediatR.Example.OpenWeatherMap.Services.Base;

public class OpenWeatherMapService
{
    // private readonly HttpClient _httpClient;
    //
    // private readonly OpenWeatherMapOptions _options;
    //
    // public OpenWeatherMapService(HttpClient httpClient, OpenWeatherMapOptions options)
    // {
    //     _httpClient = httpClient;
    //     _options = options;
    //     _options = options ?? throw new ArgumentNullException(nameof(options));
    //     _options.Validate();
    // }
    //
    // public virtual void Dispose()
    // {
    //     _httpClient.Dispose();
    // }
    //
    // public Task<Weather?> GetCurrentWeatherAsync(string city, string? countryCode = null, RequestOptions? requestOptions = default)
    // {
    //     requestOptions ??= RequestOptions.Default;
    //     requestOptions.CancellationToken.ThrowIfCancellationRequested();
    //
    //     // send request
    //     var parameters = new NameValueCollection { { "q", BuildCityName(city, countryCode) } };
    //     return RequestAsync<Weather>("weather", parameters, requestOptions);
    // }
    //
    // private Uri BuildRequestUri(string uri, NameValueCollection? queryParameters = null, RequestOptions? requestOptions = default, string version = "2.5")
    // {
    //     requestOptions ??= RequestOptions.Default;
    //
    //     // create a new query string collection
    //     var parameters = HttpUtility.ParseQueryString(string.Empty);
    //
    //     // add query parameters if specified
    //     if (queryParameters != null)
    //     {
    //         parameters.Add(queryParameters);
    //     }
    //
    //     // add the api key to the query parameter list
    //     parameters.Add("APPID", _options.ApiKey);
    //
    //     // add the specific unit if it is not the default (Kelvin)
    //     if (requestOptions.Unit != UnitType.Default)
    //     {
    //         parameters.Add("units", requestOptions.Unit == UnitType.Imperial
    //             ? "imperial" : "metric");
    //     }
    //
    //     // add the specific language when specified
    //     if (!string.IsNullOrWhiteSpace(requestOptions.Language))
    //     {
    //         parameters.Add("lang", requestOptions.Language);
    //     }
    //
    //     // build request uri
    //     var query = parameters.ToString();
    //     var requestUri = new Uri(_options.BaseAddress, version + "/" + uri);
    //     var builder = new UriBuilder(requestUri) { Query = query };
    //     return builder.Uri;
    // }
    //
    // private async Task<TEntity?> RequestAsync<TEntity>(
    //         string uri, NameValueCollection? queryParameters = null, RequestOptions? requestOptions = default,
    //         string version = "2.5", bool doCache = true, HttpMethod? method = null, HttpContent? httpContent = null)
    //         where TEntity : class
    //     {
    //         method ??= HttpMethod.Get;
    //         requestOptions ??= RequestOptions.Default;
    //         requestOptions.CancellationToken.ThrowIfCancellationRequested();
    //
    //         // send request
    //         var requestUri = BuildRequestUri(uri, queryParameters, requestOptions, version);
    //         var result = await SendAsync<TEntity>(method, requestUri, httpContent);
    //
    //         // resource not found
    //         if (result == null)
    //             return null;
    //
    //         return result;
    //     }
    //
    // private async Task<TEntity?> SendAsync<TEntity>(HttpMethod httpMethod, Uri uri, HttpContent? httpContent = null) where TEntity : class
    // {
    //     // send request to the api
    //     using var request = new HttpRequestMessage(httpMethod, uri) { Content = httpContent };
    //     using var response = await _httpClient.SendAsync(request);
    //
    //     // read the result as a string
    //     var content = await response.Content.ReadAsStringAsync();
    //
    //     // the resource was not found
    //     if (response.StatusCode == HttpStatusCode.NotFound)
    //         return default;
    //
    //
    //     if (response.IsSuccessStatusCode) return JsonConvert.DeserializeObject<TEntity>(content);
    //
    //     var messageBuilder = new StringBuilder();
    //     messageBuilder.Append("OpenWeatherMap Request Error:");
    //     messageBuilder.Append($"\n- Got status {response.StatusCode} ({(int)response.StatusCode}), expected: 2xx.");
    //     messageBuilder.Append($"\n- Request Status Line: {httpMethod.Method} {uri}.");
    //     messageBuilder.Append("\n- Response Headers:");
    //
    //     foreach (var header in response.Headers)
    //     {
    //         messageBuilder.Append($"\n    - {header.Key}:\t{string.Join(", ", header.Value)}");
    //     }
    //
    //     messageBuilder.Append($"\n- Response Content: {content}");
    //     throw new InvalidOperationException(messageBuilder.ToString());
    // }
    //
    // private string BuildCityName(string city, string? countryCode = null)
    //     => string.IsNullOrWhiteSpace(countryCode) ? city : city + "-" + countryCode;
}