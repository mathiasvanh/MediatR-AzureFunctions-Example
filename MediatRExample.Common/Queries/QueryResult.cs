using Newtonsoft.Json;

namespace MediatRExample.Common.Queries
{
    public abstract class QueryResult : IQueryResult
    {
        public bool IsSuccess { get; protected set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? FailureReason { get; protected set; }

        protected QueryResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public static TResult Failed<TResult>(string reason)
            where TResult : QueryResult, new()
        {
            return new TResult
            {
                IsSuccess = false,
                FailureReason = reason
            };
        }
    }
}
