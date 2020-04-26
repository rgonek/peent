namespace Peent.Domain.Common
{
    public interface IEntity<T>
    {
        T Id { get; }
    }
}
