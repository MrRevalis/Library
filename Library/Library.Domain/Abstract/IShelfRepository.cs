using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Abstract
{
    public interface IShelfRepository
    {
        IEnumerable<Shelf> ShelfItems { get;}
        void AddBookToShelf(Shelf shelf);
        bool RemoveBookFromShelf(int shelfID);
        bool CheckExisting(string userID, int bookID);
        void RemoveBooksFromUser(string userID);
    }
}
