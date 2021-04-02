using Library.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Concrete
{
    public class EFDbContext : IdentityDbContext<AppUser>
    {
        public EFDbContext() : base("EFDbContext") 
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EFDbContext, Library.Domain.Migrations.Configuration>());
        }

        static EFDbContext()
        {
            Database.SetInitializer<EFDbContext>(new IdentityDbInit());
        }

        public static EFDbContext Create()
        {
            return new EFDbContext();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Shelf> ShelfBooks { get; set; }
    }

    public class IdentityDbInit: NullDatabaseInitializer<EFDbContext>
    {

    }
}
