using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{


    public abstract class Account : IAccount 
    {
        protected internal event AccountStateHandler Withdrawed; //event heppening by withdraw money
        protected internal event AccountStateHandler Added; //event heppening by add money
        protected internal event AccountStateHandler Opened; // event heppening by open account
        protected internal event AccountStateHandler Closed; //event heppening by close account
        protected internal event AccountStateHandler Calculated; //event heppening by added procents

        static int counter = 0;
        protected int _days = 0; //time after open account

        public Account(decimal sum, int percentage)
        {
            Sum = sum;
            Percentage = percentage;
            Id = ++counter;

        }

        public decimal Sum { get; private set; } //Current account amount
        public int Percentage { get; private set; } //Percentage of charges
        public int Id { get; private set; } //unic id of account

        //Call events
        private void CallEvent (AccountEventArgs e, AccountStateHandler handler)
        {
            if (e != null)
                handler?.Invoke(this, e);
        }

        //call single events. For each event, its own virtual method is defined.

        protected virtual void OnOpened (AccountEventArgs e)
        {
            CallEvent(e, Opened);
        }

        protected virtual void OnWithdrawed(AccountEventArgs e)
        {
            CallEvent(e, Withdrawed);
        }

        protected virtual void OnAdded(AccountEventArgs e)
        {
            CallEvent(e, Added);
        }
        protected virtual void OnClosed(AccountEventArgs e)
        {
            CallEvent(e, Closed);
        }
        protected virtual void OnCalculated(AccountEventArgs e)
        {
            CallEvent(e, Calculated);
        }
        public virtual void Put(decimal sum)
        {
            sum += sum;
            OnAdded(new AccountEventArgs("Money added to the account " + sum, sum));
        }

        public virtual decimal Withdraw(decimal sum) //method Withdraw returne how many mony Withdrawed from account
        {
            decimal result = 0;
            if(Sum >= sum)
            {
                Sum -= sum;
                result = sum;
                OnWithdrawed(new AccountEventArgs($"The sum {sum} withdraw from account {Id}", sum));
            }
            else
            {
                OnWithdrawed(new AccountEventArgs($"Not anough mony on the account {Id}", 0));
            }
            return result;
        }

        // opening of account
        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArgs($"Opened new account! Account Id: {Id}", Sum));
        }
        // closing of account
        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArgs($"Account  {Id} is closed. Ended sum: {Sum}", Sum));
        }

        protected internal void IncrementDays()
        {
            _days++;
        }
        // Interest accrual
        protected internal virtual void Calculate()
        {
            decimal increment = Sum * Percentage / 100;
            Sum = Sum + increment;
            OnCalculated(new AccountEventArgs($"Interest accrued in the amount of: {increment}", increment));
        }
    } 
}
