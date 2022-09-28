using MediatR;

namespace MediatRExample.Common.Queries
{
    public interface IQueryHandler<in TQuery, TResult> :
        IRequestHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
        where TResult : IQueryResult
    {
    }
}
