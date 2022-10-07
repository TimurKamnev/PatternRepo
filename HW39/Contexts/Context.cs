using HW39.Books;
using System.Collections.Generic;
using System.Data.Entity;

namespace HW39.Contexts
{
    public class BookContext : DbContext
    {
        public BookContext() : base("DefaultConnection"){ }
        public DbSet<Book> Books { get; set; }
    }
}
