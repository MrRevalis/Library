using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.Models;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class AdminController : Controller
    {
        private IBookRepository repository;

        public AdminController(IBookRepository repositoryParam)
        {
            repository = repositoryParam;
        }

        public ActionResult Index()
        {
            IEnumerable<BooksViewModel> booksModel = repository.Books.
                Select(x => new BooksViewModel
                {
                    ID = x.BookID,
                    Title = x.Title,
                    Author = x.Author,
                    Description = x.Description.Substring(0, 50) + "...",
                    Classification = x.Classification
                });

            return View(booksModel);
        }

        public ActionResult Edit(int bookID)
        {
            Book book = repository.Books.FirstOrDefault(x => x.BookID == bookID);
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                repository.SaveBook(book);
                TempData["Message"] = new Message() { Text = "Success! <strong>You have successfully change book data.</strong>", ClassName = "alertMessage successful" };
                return RedirectToAction("Index");
            }
            else
            {
                return View(book);
            }
        }
    }
}