public interface IEventHandler<in T>
where T: IDomainEvent
{
    Task Handle(T @event);
}