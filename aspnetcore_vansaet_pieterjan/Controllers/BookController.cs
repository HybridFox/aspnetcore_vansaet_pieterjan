using aspnetcore_vansaet_pieterjan.Data;
using aspnetcore_vansaet_pieterjan.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnetcore_vansaet_pieterjan.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace aspnetcore_vansaet_pieterjan.Controllers
{
    [Route("/books")]
    public class BookController : Controller
    {
        private readonly EntityContext _entityContext;

        public BookController(EntityContext entityContext)
        {
            _entityContext = entityContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new BookListViewModel();
            model.Books = new List<BookDetailViewModel>();
            var allBooks = _entityContext.Books.OrderBy(x => x.Title).Include(x => x.Genre).Include(x => x.Authors).ThenInclude(x => x.Author).ToList();
            foreach (var book in allBooks)
            {
                var vm = ConvertToNewModel(book);
                model.Books.Add(vm);
            }

            return View(model);
        }

        private static BookDetailViewModel ConvertToNewModel(Book book)
        {
            var vm = new BookDetailViewModel();
            vm.Title = book.Title;
            vm.Id = book.Id;
            vm.ISBN = book.ISBN;
            vm.CreationDate = book.CreationDate;
            vm.Genre = book.Genre?.Name;
            vm.Author = String.Join(", ", book.Authors.Select(x => x.Author.FullName));
            return vm;
        }

        [HttpPost("/books")]
        public IActionResult Update([FromForm] BookDetailViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var book = vm.Id == 0 ? new Book() : _entityContext.Books.FirstOrDefault(x => x.Id == vm.Id);

                book.Title = vm.Title;
                book.CreationDate = vm.CreationDate;
                book.Genre = vm.GenreId.HasValue ? _entityContext.Genre.FirstOrDefault(x => x.Id == vm.GenreId) : null;
                book.ISBN = vm.ISBN;

                if (vm.Id == 0)
                    _entityContext.Books.Add(book);
                else
                    _entityContext.Books.Update(book);

                _entityContext.SaveChanges();

                return Redirect("/books");
            }
            return View("ViewBook", vm);
        }

        [HttpGet("/books/{id}")]
        public IActionResult ViewBook([FromRoute]int id)
        {
            var instance = _entityContext.Books.Include(x => x.Authors).ThenInclude(x => x.Author).FirstOrDefault(x => x.Id == id);

        
            if (instance == null)
            {
                var vm = new BookDetailViewModel();
                vm.Title = "";
                vm.Id = 0;
                vm.ISBN = "";
                vm.CreationDate = DateTime.Now;
                vm.Genre = "";
                vm.Author = "";

                vm.Genres = _entityContext.Genre.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                    }
                ).ToList();

                return View(vm);
            }
            else
            {
                var vm = ConvertToNewModel(instance);

                vm.Genres = _entityContext.Genre.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                    }
                ).ToList();

                vm.Authors = _entityContext.Authors.Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.Id.ToString(),
                }
                ).ToList();

                return View(vm);
            }
        }

        [HttpGet("/book/create")]
        public IActionResult CreateBook([FromRoute]int id)
        {
            var vm = new BookDetailViewModel();
            vm.Title = "";
            vm.Id = 0;
            vm.ISBN = "";

            vm.Genres = _entityContext.Genre.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }
            ).ToList();

            vm.Authors = _entityContext.Authors.Select(x => new SelectListItem
                {
                    Text = x.FullName,
                    Value = x.Id.ToString(),
                }
            ).ToList();

            vm.CreationDate = new DateTime();

            return View("ViewBook", vm);
        }

        [HttpPost("/books/{id}/delete")]
        public IActionResult Delete([FromRoute]int id)
        {
            var book = _entityContext.Books.FirstOrDefault(x => x.Id == id);

            _entityContext.Books.Remove(book);
            _entityContext.SaveChanges();

            return Redirect("/books");
        }
    }
}
