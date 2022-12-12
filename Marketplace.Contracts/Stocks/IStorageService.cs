using Marketplace.Models.Dto;
using System.Threading.Tasks;

namespace Marketplace.Contracts.Stocks
{
    public interface IStorageService
    {
        Task<StorageDto> FindStorageAsync(int id);

        Task<StorageDto> CreateStorageAsync(StorageDto storageDto);
        Task<StorageDto> FindStorageByProductIdAsync(int productId);

        Task<StorageDto> UpdateStorageAsync(int id, StorageDto storageDto);
    }
}
