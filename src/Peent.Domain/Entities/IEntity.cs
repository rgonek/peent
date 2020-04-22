namespace Peent.Domain.Entities
{
    public interface IEntity<T>
    {
        T Id { get; }
    }
}
