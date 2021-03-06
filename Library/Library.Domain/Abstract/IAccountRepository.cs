﻿using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Abstract
{
    public interface IAccountRepository
    {
        IEnumerable<Account> Accounts { get; }
        void CreateAccount(Account account);
        bool EmailExists(string email);
        bool UsernameExists(string login);
        Account ValidAccount(string login, string password);
    }
}
