using System;

namespace Cheques.Models
{
    public class Cheque
    {
        public Cheque(string name, decimal amount, string amountInWords, DateTime date)
        {
            Name = name;
            Amount = amount;
            AmountInWords = amountInWords;
            Date = date;
        }

        public string Name { get; }
        public decimal Amount { get; }
        public string AmountInWords { get; }
        public DateTime Date { get; }
    }
}