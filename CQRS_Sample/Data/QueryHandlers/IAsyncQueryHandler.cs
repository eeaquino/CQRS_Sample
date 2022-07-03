using CQRS_Sample.Queries;

namespace CQRS_Sample.Data.QueryHandlers
{
    public interface IAsyncQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> Handle(TQuery query,CancellationToken ct);
    }
}
