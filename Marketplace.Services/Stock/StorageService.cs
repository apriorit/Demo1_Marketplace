using Marketplace.Contracts;
using Marketplace.Contracts.Stocks;
using Marketplace.Data.Infrastructure;
using Marketplace.Data.Models.Stocks;
using Marketplace.Models.Dto;
using System.Threading.Tasks;

namespace Marketplace.Services.Stock
{
    public class StorageService : DomainService<int, Storage, StorageDto>, IStorageService
    {
        public StorageService(IRepository<Storage> repository, IEntityConverter entityConverter)
            : base(repository, entityConverter)
        {
        }

        public Task<StorageDto> CreateStorageAsync(StorageDto storageDto)
        {
            return CreateAsync(storageDto);
        }

        public Task<StorageDto> FindStorageAsync(int id)
        {
            return FindAsync(id);
        }

        public async Task<StorageDto> FindStorageByProductIdAsync(int productId)
        {
            var entity = await _repository.GetFirstOrDefaultAsync(model => model.ProductId == productId);
            return EntityConverter.ConvertTo<Storage, StorageDto>(entity);
        }

        public Task<StorageDto> UpdateStorageAsync(int id, StorageDto storageDto)
        {
            return UpdateAsync(id, storageDto);
        }
    }
}
