using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class ShelfController : Controller
    {
        private IShelfRepository ShelfRepository;
        private AppUserManager UserManager;
        public ShelfController(IShelfRepository repository, AppUserManager userManager)
        {
            ShelfRepository = repository;
            UserManager = userManager;
        }

        [Authorize]
        public ActionResult Add(int bookID, string returnURL)
        {
            AppUser user = UserManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            if(user != null)
            {
                if (ShelfRepository.CheckExisting(user.Id, bookID))
                {
                    TempData["Message"] = new Message() { Text = "Info! <strong>You have already added this book to your shelf.</strong>", ClassName = "alertMessage info" };
                }
                else
                {
                    Shelf shelf = new Shelf() { BookID = bookID, UserID = user.Id };
                    ShelfRepository.AddBookToShelf(shelf);
                    TempData["Message"] = new Message() { Text = "Success! <strong>You have successfully added book to your shelf.</strong>", ClassName = "alertMessage successful" };
                }
            }
            else
            {
                TempData["Message"] = new Message() { Text = "Error! <strong>User is not logged in.</strong>", ClassName = "alertMessage error" };
            }


            return Redirect(returnURL);
        }

        [Authorize]
        public ActionResult Index()
        {
            AppUser user = UserManager.Users.FirstOrDefault(x => x.UserName == User.Identity.Name);
            IEnumerable<Shelf> bookShelf = ShelfRepository.ShelfItems.Where(x => x.UserID == user.Id);

            return View(bookShelf);
        }

        [Authorize]
        public ActionResult Delete(int shelfID)
        {
            bool deleted = ShelfRepository.RemoveBookFromShelf(shelfID);
            if (deleted)
            {
                TempData["Message"] = new Message() { Text = "Success! <strong>You have successfully delete item from your bookshelf.</strong>", ClassName = "alertMessage successful" };
            }
            else
            {
                TempData["Message"] = new Message() { Text = "Error! <strong>Cannot delete item!</strong>", ClassName = "alertMessage error" };
            }
            return RedirectToAction("Index");
        }
    }
}