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
    }
}
