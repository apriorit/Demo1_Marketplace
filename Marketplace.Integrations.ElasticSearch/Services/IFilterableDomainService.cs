namespace Marketplace.Integrations.ElasticSearch.Services
{
    public interface IFilterableDomainService<TKey, TDto>
    {
        Task<IReadOnlyCollection<TDto>> Filter(string keyword);
    }
}
