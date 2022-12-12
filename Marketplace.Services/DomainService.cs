using Marketplace.Contracts;
using Marketplace.Contracts.Models;
using Marketplace.Data.Infrastructure;
using Marketplace.Infrastructure.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marketplace.Services
{
    public class DomainService<TKey, TEntity, TDto> : IDomainService<TKey, TDto>
        where TEntity : class, IIdentifiable<TKey>
    {
        protected readonly IRepository<TEntity> _repository;
        public IEntityConverter EntityConverter { protected set; get; }

        public DomainService(IRepository<TEntity> repository, IEntityConverter entityConverter)
        {
            _repository = repository;
            EntityConverter = entityConverter;
        }

        protected virtual async Task<TEntity> Find(TKey id)
        {
            return await _repository.GetFirstOrDefaultAsync(model => model.Id.Equals(id)) ?? throw new NotFoundException($"Entity of type {typeof(TDto)} with id {id} doesn't exists");
        }

        public async Task<IEnumerable<TDto>> FindAsync()
        {
            var entities = await _repository.Get().ToListAsyncExt();
            return EntityConverter.ConvertTo<IEnumerable<TEntity>, IEnumerable<TDto>>(entities);
        }

        public async Task<TDto> GetFirstOrDefaultAsync(TKey id)
        {
            var entities = await _repository.GetFirstOrDefaultAsync(model => model.Id.Equals(id));
            return entities == null ? default : EntityConverter.ConvertTo<TEntity, TDto>(entities);
        }

        public async Task<TDto> FindAsync(TKey id)
        {
            var entity = await this.Find(id);
            return EntityConverter.ConvertTo<TEntity, TDto>(entity);
        }

        public virtual async Task<TDto> CreateAsync(TDto model)
        {
            var toCreate = EntityConverter.ConvertTo<TDto, TEntity>(model);
            await _repository.CreateAsync(toCreate);
            var created = EntityConverter.ConvertTo<TEntity, TDto>(toCreate);
            return created;
        }

        public virtual async Task<TDto> UpdateAsync(TKey id, TDto model)
        {
            var toUpdate = await Find(id);
            EntityConverter.Merge<TDto, TEntity>(model, toUpdate);
            await _repository.UpdateAsync(a => a.Id.Equals(id), toUpdate);
            var updated = EntityConverter.ConvertTo<TEntity, TDto>(toUpdate);
            return updated;
        }

        public virtual async Task<TDto> DeleteAsync(TKey id)
        {
            var toDelete = await Find(id);
            if (toDelete is IDeletable)
            {
                (toDelete as IDeletable).IsActive = false;
                await _repository.UpdateAsync(a => a.Id.Equals(id), toDelete);
            }
            else
            {
                await _repository.DeleteAsync(toDelete);
            }
            var deleted = EntityConverter.ConvertTo<TEntity, TDto>(toDelete);
            return deleted;
        }
    }
}
