using Domain.Entities;

namespace Infrastructure.Repositories;

public interface IUserRepository : IBaseUserRepository<User>
{
    bool IsUserAlreadyExist(User user);
}

public class UserRepository : BaseUserRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    
    public bool IsUserAlreadyExist(User user)
    {
        return DbContext.Users.Any(u => u.Email == user.Email || u.Username == user.Username);
    }
}