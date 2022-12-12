namespace Marketplace.Contracts.Models
{
    public interface IIdentifiable
    {
        object Id { get; }
    }

    public interface IIdentifiable<TKey> : IIdentifiable
    {
        new TKey Id { get; }
    }
}
