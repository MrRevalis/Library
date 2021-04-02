using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public class Shelf
    {
        public int ShelfID { get; set; }

        public int? BookID { get; set; }
        [ForeignKey("BookID")]
        public virtual Book Book { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual AppUser AppUser { get; set; }

    }
}
