using aspnetcore_vansaet_pieterjan.Data;
using aspnetcore_vansaet_pieterjan.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspnetcore_vansaet_pieterjan.Entities;
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
            var allBooks = _entityContext.Books.OrderBy(x => x.Title).Include(x => x.Authors).ThenInclude(x => x.Author).ToList();
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
            vm.Author = "";
            vm.ISBN = book.ISBN;
            vm.Author = String.Join(", ", book.Authors.Select(x => x.Author.FullName));
            return vm;
        }

        [HttpGet("/books/{id}")]
        public IActionResult ViewBook([FromRoute]int id)
        {
            var instance = _entityContext.Books.Include(x => x.Authors).ThenInclude(x => x.Author).FirstOrDefault(x => x.Id == id);


            if (instance == null)
            {
                return NotFound("Not Found");
            }
            else
            {
                var vm = ConvertToNewModel(instance);
                return View(vm);
            }
        }
    }
}
