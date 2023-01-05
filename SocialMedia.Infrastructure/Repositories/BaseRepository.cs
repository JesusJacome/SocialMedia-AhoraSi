using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;

namespace SocialMedia.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly SocialMediaContext context;
        protected readonly DbSet<T> entities;
        public BaseRepository(SocialMediaContext context)
        {
            this.context = context;
            entities = context.Set<T>(); 
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await entities.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await entities.AddAsync(entity);

        }

        public void Update(T entity)
        {
            entities.Update(entity);

        }

        public async Task DeleteAsync(int id)
        {
            T entity = await GetByIdAsync(id);
            entities.Remove(entity);
        }

    }
}
