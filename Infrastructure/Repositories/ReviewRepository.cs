using Domain.Entities;
namespace Infrastructure.Repositories;

public interface IReviewRepository : IRepository<Review>
{
    
}
public class ReviewRepository : Repository<Review>, IReviewRepository
{
    public ReviewRepository(ApplicationDbContext dbContext) : base(dbContext) {}
}