namespace MediatRExample.Common.Queries
{
    public abstract class QueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
        where TResult : IQueryResult
    {
        public abstract Task<TResult> Handle(TQuery request, CancellationToken cancellationToken);
    }
}