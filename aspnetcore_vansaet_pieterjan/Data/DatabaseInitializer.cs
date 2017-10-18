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

        DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }


        public static void InitializeDatabase(EntityContext entityContext)
        {
            entityContext.Database.EnsureCreated();

            var genres = new List<Genre>
            {
                new Genre() {Name = "Horror"},
                new Genre() {Name = "Romcom"},
                new Genre() {Name = "Klassieker"}
            };

            var authors = new List<Author>();
            for (var i = 0; i < 20; i++)
            {
                authors.Add(new Author { FirstName = $"Author First Name {i}", LastName = $"Author Last Name {i}"});
            }

            var books = new List<Book>();
            for (var i = 0; i < 20; i++)
            {
                var authorBook = new AuthorBook()
                {
                    Author = authors[i]
                };
                Genre genre = null;
                if (i % 4 == 0)
                {
                    genre = genres[0];
                }
                else if (i % 3 == 0)
                {
                    genre = genres[1];
                }
                else if (i % 2 == 0)
                {
                    genre = genres[2];
                }
                books.Add(new Book { Title = $"Book {i}", Authors = new List<AuthorBook> { authorBook }, Genre = genre, ISBN = RandomString(20)});
            }

            var me = new Author { FirstName = "Raf", LastName = "Ceuls" };
            books[0].Authors.Add(new AuthorBook() { Author = me });

            entityContext.Genre.AddRange(genres);
            entityContext.Authors.AddRange(authors);
            entityContext.Books.AddRange(books);
            entityContext.SaveChanges();
        }
    }
}
