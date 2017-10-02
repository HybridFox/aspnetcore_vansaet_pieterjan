using aspnetcore_vansaet_pieterjan.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore_vansaet_pieterjan.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Memes()
        {
            var model = new AboutModel();

            model.DaysUntillBirthday = (new DateTime(2017, 11, 20) - DateTime.Now).TotalDays;

            return View(model);
        }
    }
}
