using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;


public interface IAuthorRepository : IRepository<Author>
{
    Task<bool> IsAuthorAlreadyExistAsync(Author author);
    Task<Author?> TryGetByDataAsync(string name, string secondName, string? patronomic, DateOnly? birth);
}


public class AuthorRepository : Repository<Author>, IAuthorRepository
{
    public AuthorRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
    
    public async Task<bool> IsAuthorAlreadyExistAsync(Author author)
    {
        return await DbContext.Authors.AnyAsync(a => a.Name == author.Name && a.SecondName == author.SecondName && a.Patronymic == author.Patronymic && a.BirthDate == author.BirthDate);
    }

    public async Task<Author?> TryGetByDataAsync(string name, string secondName, string? patronomic, DateOnly? birth)
    {
        return await DbContext.Authors.SingleOrDefaultAsync(a =>
            a.Name == name && a.SecondName == secondName && a.Patronymic == patronomic && a.BirthDate == birth);
    }
}