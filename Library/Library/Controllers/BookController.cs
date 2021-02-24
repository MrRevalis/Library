using Library.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository repository;

        public BookController(IBookRepository repositoryParam)
        {
            repository = repositoryParam;
        }

        public ViewResult List()
        {
            return View(repository.Books);
        }
    }
}