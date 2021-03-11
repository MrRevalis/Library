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
        public string Title { get; set; }
        public string Author { get; set; }
        [DataType(DataType.MultilineText), Display(Name = "Description")]
        public string Description { get; set; }
        public string Classification { get; set; }
    }
}