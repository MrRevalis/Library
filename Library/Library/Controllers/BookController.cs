using Library.Domain.Abstract;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository repository;
        public int pageSize = 4;

        public BookController(IBookRepository repositoryParam)
        {
            repository = repositoryParam;
        }

        public ViewResult List(int page = 1)
        {
            BooksListViewModel booksModel = new BooksListViewModel
            {
                Books = repository.Books
                .OrderBy(x => x.BookID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    TotalItems = repository.Books.Count(),
                    ItemsPerPage = pageSize,
                    CurrentPage = page
                }
            };
            return View(booksModel);
        }
    }
}