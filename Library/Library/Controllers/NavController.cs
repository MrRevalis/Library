using Library.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class NavController : Controller
    {
        private IBookRepository repository;
        public NavController(IBookRepository repositoryParam)
        {
            repository = repositoryParam;
        }

        public PartialViewResult Menu(string genre = null)
        {
            ViewBag.genre = genre;

            IEnumerable<string> genres = repository.Books
                .Select(x => x.Classification)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(genres);
        }
    }
}