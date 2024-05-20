using Castle.Components.DictionaryAdapter.Xml;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public interface IBaseUserRepository<TUser> : IRepository<TUser> where TUser : BaseUser
{
    public Task<TUser?> TryGetByEmailAsync(string email);
}
public abstract class BaseUserRepository<TUser> : Repository<TUser> where TUser : BaseUser
{
    public BaseUserRepository(ApplicationDbContext dbContext) : base(dbContext) {}

    public async Task<TUser?> TryGetByEmailAsync(string email)
    {
        return await Set.SingleOrDefaultAsync(user => user.Email == email);
    }
}