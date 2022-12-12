using Marketplace.Contracts;
using Marketplace.Data.Infrastructure;
using Marketplace.Data.Models.Stocks;
using Marketplace.Infrastructure.Exceptions;
using Marketplace.Models.Dto;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Services.Stock
{
    public class ProductCategoryService : DomainService<int, ProductCategory, ProductCategoryDto>
    {

        public ProductCategoryService(IRepository<ProductCategory> repository, IEntityConverter entityConverter)
            : base(repository, entityConverter)
        {
        }

        public override Task<ProductCategoryDto> CreateAsync(ProductCategoryDto model)
        {
            EnsureNameIsUnique(model.Name);
            return base.CreateAsync(model);
        }

        public override Task<ProductCategoryDto> UpdateAsync(int id, ProductCategoryDto model)
        {
            EnsureNameIsUnique(model.Name, id);
            return base.UpdateAsync(id, model);
        }

        private void EnsureNameIsUnique(string currentName, int? id = null)
        {
            var nameAlreadyExists = _repository.Get().Any(p => p.Id != id && p.Name == currentName);
            if (nameAlreadyExists)
                throw new ConflictException($"Entity with name '{currentName}' already exists");
        }
    }
}

