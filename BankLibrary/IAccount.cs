using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public interface IAccount
    {
        void Put(decimal sum);//Put money to account
        decimal Withdraw(decimal sum);//Get money from account  
    }
}
