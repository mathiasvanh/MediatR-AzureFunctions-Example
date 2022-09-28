namespace MediatRExample.Common.Commands
{
    public interface ICommandResult
    {
        bool IsSuccess { get; }

        string? FailureReason { get; }
    }
}
