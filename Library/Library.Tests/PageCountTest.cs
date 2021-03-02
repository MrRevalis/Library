using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.Controllers;
using Library.ViewModels;

namespace Library.Tests
{
    [TestClass]
    public class PageCountTest
    {
        [TestMethod]
        public void Specific_Genre_Book_Count()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(x => x.Books).Returns(new Book[]{
                new Book(){Classification = "Fantasy"},
                new Book(){Classification = "Adventure"},
                new Book(){Classification = "Novel"},
                new Book(){Classification = "Fantasy"},
                new Book(){Classification = "Adventure"},
                new Book(){Classification = "Novel"},
                new Book(){Classification = "Novel"},
                new Book(){Classification = "Adventure"}
                });

            BookController bookController = new BookController(mock.Object);
            bookController.pageSize = 3;

            int fantasy = ((BooksListViewModel)bookController.List("Fantasy").Model).PagingInfo.TotalItems;
            int adventure = ((BooksListViewModel)bookController.List("Adventure").Model).PagingInfo.TotalItems;
            int novel = ((BooksListViewModel)bookController.List("Novel").Model).PagingInfo.TotalItems;
            int allItems = ((BooksListViewModel)bookController.List(null).Model).PagingInfo.TotalItems;

            Assert.AreEqual(2, fantasy);
            Assert.AreEqual(3, adventure);
            Assert.AreEqual(3, novel);
            Assert.AreEqual(8, allItems);
        }
    }
}
