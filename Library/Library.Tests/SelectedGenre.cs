using Library.Controllers;
using Library.Domain.Abstract;
using Library.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Library.Tests
{
    [TestClass]
    public class SelectedGenre
    {
        [TestMethod]
        public void Selected_Book_Genre()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(x => x.Books).Returns(new Book[]{
                new Book(){Classification = "Fantasy"},
                new Book(){Classification = "Adventure"},
                new Book(){Classification = "Novel"},
                });

            NavController navController = new NavController(mock.Object);

            string genre = "Adventure";

            string result = navController.Menu(genre).ViewBag.genre;

            Assert.AreEqual(genre, result);
        }
    }
}
