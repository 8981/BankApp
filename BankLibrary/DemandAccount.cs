using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public class DemandAccount : Account
    {
        public DemandAccount(decimal sum, int percentage) : base(sum, percentage)
        {
        }

        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArgs($"Opend new Demand account! Account Id: {this.Id}", this.Sum));
        }
    }
}
