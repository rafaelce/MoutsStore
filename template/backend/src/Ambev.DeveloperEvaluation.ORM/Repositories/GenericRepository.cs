using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly DefaultContext Context;
    protected readonly DbSet<T> DbSet;

    public GenericRepository(DefaultContext context)
    {
        Context = context;
        DbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(object id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        var result = await DbSet.AddAsync(entity);
        await Context.SaveChangesAsync();
        return result.Entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        DbSet.Update(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        DbSet.Remove(entity);
        await Context.SaveChangesAsync();
    }

    public virtual async Task<bool> ExistsAsync(object id)
    {
        var entity = await DbSet.FindAsync(id);
        return entity != null;
    }

    public virtual IQueryable<T> Query()
    {
        return DbSet.AsQueryable();
    }
}
