using System;

namespace Cheques.Models
{
    public class Cheque
    {
        public Cheque(string name, decimal amount, string amountInWords, string formattedDate)
        {
            Name = name;
            Amount = amount;
            AmountInWords = amountInWords;
            FormattedDate = formattedDate;
        }

        public string Name { get; }
        public decimal Amount { get; }
        public string AmountInWords { get; }
        public string FormattedDate { get; }
    }
}