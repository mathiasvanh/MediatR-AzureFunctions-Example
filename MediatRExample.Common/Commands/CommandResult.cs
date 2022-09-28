using Newtonsoft.Json;

namespace MediatRExample.Common.Commands
{
    public abstract class CommandResult : ICommandResult
    {
        public bool IsSuccess { get; protected set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? FailureReason { get; protected set; }

        protected CommandResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public static TResult Failed<TResult>(string reason)
            where TResult : CommandResult, new()
        {
            return new TResult
            {
                IsSuccess = false,
                FailureReason = reason
            };
        }
    }
}
