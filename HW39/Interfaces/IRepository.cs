using HW39.Books;

namespace HW39.Interfaces
{
    public interface IRepository<T> : IDisposable
        where T : class
    {
        IEnumerable<Book> GetBookList();
        Book GetBook(int id);
        void Create(Book item);
        void Update(Book item);
        void Delete(int id);
        void Save();
    }
}
