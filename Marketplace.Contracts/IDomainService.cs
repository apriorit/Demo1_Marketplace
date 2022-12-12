using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Contracts
{
    public interface IDomainService<TKey, TDto>
    {
        IEntityConverter EntityConverter { get; }
        Task<IEnumerable<TDto>> FindAsync();

        Task<TDto> FindAsync(TKey id);

        Task<TDto> CreateAsync(TDto model);

        Task<TDto> UpdateAsync(TKey id, TDto model);

        Task<TDto> DeleteAsync(TKey id);
        Task<TDto> GetFirstOrDefaultAsync(TKey id);
    }
}
