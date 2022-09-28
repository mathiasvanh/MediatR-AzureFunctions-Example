namespace MediatRExample.Common.Validation
{
    public class ValidationException : Exception
    {
        public ValidationException(string message, string? details = null) : base(message)
        {
            this.Details = details;
        }

        public string? Details { get; }
    }
}
