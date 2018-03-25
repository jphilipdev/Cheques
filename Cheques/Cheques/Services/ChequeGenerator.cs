using Cheques.Models;
using Cheques.Models.Exceptions;
using Cheques.Services.Interfaces;
using System;

namespace Cheques.Services
{
    public class ChequeGenerator : IChequeGenerator
    {
        private readonly INumberTextConverter _numberTextConverter;

        public ChequeGenerator(INumberTextConverter numberTextConverter)
        {
            _numberTextConverter = numberTextConverter;
        }

        public Cheque Generate(string name, decimal amount, DateTime date)
        {
            ValidateCheque(amount);

            var amountInWords = _numberTextConverter.Convert(amount);
            return new Cheque(name, amount, amountInWords, date);
        }

        private static void ValidateCheque(decimal amount)
        {
            if (amount < 0)
            {
                throw new NegativeChequeAmountRequestedException(amount);
            }

            if (amount == 0)
            {
                throw new ZeroChequeAmountRequestedException();
            }
        }
    }
}