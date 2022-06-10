namespace OrderApi.Infrastructure
{
    public interface IQueryHandler<in TQuery, TResponse>
    {
        Task<TResponse> HandleAsync(TQuery query);
    }
}
