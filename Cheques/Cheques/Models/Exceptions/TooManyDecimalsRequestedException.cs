using System;

namespace Cheques.Models.Exceptions
{
    public class TooManyDecimalsRequestedException : Exception
    {
        public TooManyDecimalsRequestedException(decimal amount)
        {
            Amount = amount;
        }

        public decimal Amount { get; }
    }
}