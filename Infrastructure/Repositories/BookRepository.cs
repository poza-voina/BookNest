using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public interface IBookRepository : IRepository<Book>
{
    public Task<bool> IsBookAlreadyExistAsync(Book book);

}
public class BookRepository : Repository<Book>, IBookRepository 
{
    public BookRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<bool> IsBookAlreadyExistAsync(Book book)
    {
        return await DbContext.Books
            .AnyAsync(
                b => b.Title == book.Title && b.Author == book.Author
                );

    }
}