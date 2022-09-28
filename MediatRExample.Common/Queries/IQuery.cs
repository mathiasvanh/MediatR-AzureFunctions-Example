using MediatR;

namespace MediatRExample.Common.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}
