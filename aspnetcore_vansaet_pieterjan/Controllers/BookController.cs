using aspnetcore_vansaet_pieterjan.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore_vansaet_pieterjan.Controllers
{
    [Route("/books")]
    public class BookController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            var model = new DictionaryModel();

            return View(model);
        }

        [HttpGet("/books/{id}")]
        public IActionResult ViewBook([FromRoute]int id)
        {
            var model = new DictionaryModel().Books[id];

            System.Diagnostics.Debug.WriteLine(model);


            return View(model);
        }
    }
}
