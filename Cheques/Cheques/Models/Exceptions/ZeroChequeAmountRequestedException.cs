using System;

namespace Cheques.Models.Exceptions
{
    public class ZeroChequeAmountRequestedException : Exception
    {
        public ZeroChequeAmountRequestedException()
        {
        }
    }
}