using System;

namespace Cheques.Models.Exceptions
{
    public class NegativeChequeAmountRequestedException : Exception
    {
        public NegativeChequeAmountRequestedException(decimal amount)
        {
            Amount = amount;
        }

        public decimal Amount { get; }
    }
}