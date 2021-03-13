using Library.Domain.Abstract;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Concrete
{
    public class EFBookRepository : IBookRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Book> Books
        {
            get { return context.Books; }
        }

        public void SaveBook(Book book)
        {
            if(book.BookID == 0)
            {
                context.Books.Add(book);
            }
            else
            {
                Book dbBook = context.Books.Find(book.BookID);
                if(dbBook != null)
                {
                    dbBook.Title = book.Title;
                    dbBook.Author = book.Author;
                    dbBook.Description = book.Description;
                    dbBook.Classification = book.Classification;
                    dbBook.ImageData = book.ImageData;
                    dbBook.ImageMimeType = book.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
    }
}
