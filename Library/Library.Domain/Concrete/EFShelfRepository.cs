using Library.Domain.Abstract;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Concrete
{
    public class EFShelfRepository : IShelfRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Shelf> ShelfItems
        {
            get { return context.ShelfBooks; }
        }

        public void AddBookToShelf(Shelf shelf)
        {
            context.ShelfBooks.Add(shelf);
            context.SaveChanges();
        }

        public bool CheckExisting(string userID, int bookID)
        {
            return context.ShelfBooks.Any(x => x.BookID == bookID && x.UserID == userID);
        }

        public bool RemoveBookFromShelf(int shelfID)
        {
            Shelf shelfItem = context.ShelfBooks.Find(shelfID);
            if(shelfItem != null)
            {
                context.ShelfBooks.Remove(shelfItem);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
