using CQRS_Sample.Queries;

namespace CQRS_Sample.Data.QueryHandlers
{
    public interface IQueryHandler<in TQuery,TResult> where TQuery :IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}
