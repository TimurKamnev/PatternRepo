using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System.Configuration;
using HW39.Books;

namespace HW39.Interfaces
{
    public class MongoBookRepository : IRepository<Book>
    {
        MongoClient client;
        MongoServer server;
        MongoDatabase database;

        public MongoBookRepository()
        {
            var con = new MongoConnectionStringBuilder(
                ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString);

            client = new MongoClient(con.ConnectionString);
            server = client.GetServer();
            database = server.GetDatabase(con.DatabaseName);
        }      

        public MongoCollection<Book> Books
        {
            get
            {
                return database.GetCollection<Book>("books");
            }
        }

        public IEnumerable<Book> GetBookList()
        {
            return Books.FindAll();
        }

        public Book GetBook(int id)
        {
            return Books.FindOneById(id);
        }

        public void Create(Book book)
        {
            try
            {
                int id = 0;
                if (Books.Count() > 0)
                    id = Books.FindAll().Max(x => x.Id);
                book.Id = ++id;
                Books.Insert(book);
            }
            catch { }
        }

        public void Update(Book book)
        {
            try
            {
                Books.Save(book);
            }
            catch { }
        }

        public void Delete(int id)
        {
            try
            {
                var query = Query<Book>.EQ(e => e.Id, id);
                if (query != null)
                {
                    Books.Remove(query);
                }
            }
            catch { }
        }
        public void Save() { }
        public void Dispose() { }
    }
}
