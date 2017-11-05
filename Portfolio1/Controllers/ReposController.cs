using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class ReposController : Controller
    {
        public IActionResult Index()
        {
            var topRepos = Repos.GetRepos();
            return View(topRepos);
        }
    }
}
