namespace OrderApi.Infrastructure
{
    public interface ICommandHandler<in TCommand>
    {
        Task HandleAsync(TCommand command);
    }
}
