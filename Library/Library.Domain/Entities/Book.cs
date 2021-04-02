using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Book
    {
        [Key]
        public int BookID { get; set; }

        [Required(ErrorMessage = "Please enter a book title.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter an author.")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter a book genre.")]
        public string Classification { get; set; }

        [Display(Name = "Book cover")]
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
