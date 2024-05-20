namespace Infrastructure.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

public interface IRepository<TEntity>
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<TEntity> GetAsync(int id);
    public IQueryable<TEntity> Items { get; }
    
    
}

public class Repository<TEntity> : IDisposable, IRepository<TEntity>, IAsyncDisposable where TEntity : BaseEntity
{
    protected ApplicationDbContext DbContext { get; }
    protected DbSet<TEntity> Set { get; }

    public IQueryable<TEntity> Items => Set.AsQueryable();
    
    public Repository(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
        Set = dbContext.Set<TEntity>();
    }
    
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        Set.Add(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        Set.Update(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(TEntity entity)
    {
        Set.Remove(entity);
        await DbContext.SaveChangesAsync();
    }

    public async Task<TEntity> GetAsync(int id)
    {
        return await Set.FindAsync(id) ?? throw new ArgumentException($"Entity with id = {id} not found");
    }

    public void Dispose()
    {
        DbContext.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await DbContext.DisposeAsync();
    }
}