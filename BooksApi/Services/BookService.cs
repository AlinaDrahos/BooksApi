using BooksApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksApi.NewFolder;

namespace BooksApi.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _Books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get() =>
            _Books.Find(book => true).ToList();

        public Book Get(string id) =>
            _Books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _Books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _Books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Book bookIn) =>
            _Books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _Books.DeleteOne(book => book.Id == id);
    }
}
