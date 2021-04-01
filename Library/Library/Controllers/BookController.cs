using Library.Domain.Abstract;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Library.Models;
using Library.Domain.Entities;

namespace Library.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository repository;
        public int pageSize = 9;

        public BookController(IBookRepository repositoryParam)
        {
            repository = repositoryParam;
        }

        public ViewResult List(string genre, int page = 1)
        {
            BooksListViewModel booksModel = new BooksListViewModel
            {
                Books = repository.Books
                .Where(x => genre == null || x.Classification == genre)
                .OrderBy(x => x.BookID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    TotalItems = genre == null ?
                    repository.Books.Count():
                    repository.Books.Where(x => x.Classification == genre).Count(),
                    ItemsPerPage = pageSize,
                    CurrentPage = page
                },
                BookGenre = genre
            };
            return View(booksModel);
        }

        public FileContentResult GetImage(int ID)
        {
            Book book = repository.Books.FirstOrDefault(x => x.BookID == ID);
            if(book != null)
            {
                return File(book.ImageData, book.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public ActionResult SearchResults(string name)
        {
            if (!String.IsNullOrEmpty(name))
            {
                IEnumerable<Book> book = repository.Books.Where(x => x.Title.ToLower().Contains(name.ToLower()));
                if (book.Any())
                {
                    return View(book);
                }
                else
                {
                    TempData["Message"] = new Message() { Text = "Info! <strong>No books with that title.</strong>", ClassName = "alertMessage info" };
                    return RedirectToAction("List", "Book");
                }
            }
            else
            {
                return RedirectToAction("List", "Book");
            }
            
        }

        public ActionResult Description(int? ID)
        {
            Book book = repository.Books.FirstOrDefault(x => x.BookID == ID);
            if(book != null)
            {
                return View(book);
            }
            else
            {
                ViewBag.Error = $"Book with ID {ID} cannot be found!";
                return View("Error");
            }
        }
    }
}