using Library.Domain.Abstract;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Concrete
{
    public class EFAccountRepository : IAccountRepository
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Account> Accounts
        {
            get { return context.Accounts; }
        }

        public void CreateAccount(Account account)
        {
            context.Accounts.Add(account);
            context.SaveChanges();
        }

        public bool EmailExists(string email) => !context.Accounts.Any(x => x.Email == email);

        public bool UsernameExists(string username) => !context.Accounts.Any(x => x.Username == username);
    }
}
