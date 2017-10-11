using aspnetcore_vansaet_pieterjan.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace aspnetcore_vansaet_pieterjan.Data
{
    public class DatabaseInitializer
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        public static void InitializeDatabase(EntityContext entityContext)
        {
            if (((entityContext.GetService<IDatabaseCreator>() as RelationalDatabaseCreator)?.Exists()).GetValueOrDefault(false))
            {
                return;
            }

            var authors = new List<Author>();
            for (var i = 0; i < 20; i++)
            {
                authors.Add(new Author { FirstName = $"Author First Name {i}", LastName = $"Author Last Name {i}" });
            }

            var books = new List<Book>();
            for (var i = 0; i < 20; i++)
            {
                var authorBook = new AuthorBook()
                {
                    Author = authors[i]
                };

                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var randIsbn = (Enumerable.Repeat(chars, 10)
                    .Select(s => s[new Random().Next(s.Length)]).ToArray());

                books.Add(new Book { Title = $"Book {i}", Authors = new List<AuthorBook> { authorBook }, ISBN = RandomString(10)});
            }

            var me = new Author { FirstName = "Raf", LastName = "Ceuls" };
            books[0].Authors.Add(new AuthorBook() { Author = me });

            entityContext.Database.EnsureCreated();
            entityContext.Authors.AddRange(authors);
            entityContext.Books.AddRange(books);
            entityContext.SaveChanges();
        }
    }
}
