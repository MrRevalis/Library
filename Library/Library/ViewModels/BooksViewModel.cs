using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.ViewModels
{
    public class BooksViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int ID { get; set; }
        [Required(ErrorMessage = "Please enter a book title.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter an author.")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Please enter a description.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter a book genre.")]
        public string Classification { get; set; }
    }
}