using Cheques.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cheques.Services
{
    public class NumberTextConverter : INumberTextConverter
    {
        public string Convert(decimal number)
        {
            var wholeNumber = Math.Floor(number);
            var decimals = (number - wholeNumber) * 100;

            var digitGroup = new DigitGroup(System.Convert.ToInt32(decimals), "cent");

            return digitGroup.ToString();
        }

        private class DigitGroup
        {
            private readonly Dictionary<int, string> _digitConversions = new Dictionary<int, string>()
            {
                [0] = "zero",
                [1] = "one",
                [2] = "two",
                [3] = "three",
                [4] = "four",
                [5] = "five",
                [6] = "six",
                [7] = "seven",
                [8] = "eight",
                [9] = "nine",
                [10] = "ten",
                [11] = "eleven",
                [12] = "twelve",
                [13] = "thirteen",
                [14] = "fourteen",
                [15] = "fifteen",
                [16] = "sixteen",
                [17] = "seventeen",
                [18] = "eighteen",
                [19] = "nineteen",
                [20] = "twenty",
                [30] = "thirty",
                [40] = "forty",
                [50] = "fifty",
                [60] = "sixty",
                [70] = "seventy",
                [80] = "eighty",
                [90] = "ninety",
            };

            public DigitGroup(int digits, string digitGroupName = null)
            {
                Digits = digits;
                DigitGroupName = digitGroupName;
            }

            public int Digits { get; }
            public string DigitGroupName { get; }

            public override string ToString()
            {
                var sb = new StringBuilder();

                if(_digitConversions.ContainsKey(Digits))
                {
                    if (DigitGroupName == null)
                    {
                        return _digitConversions[Digits];
                    }

                    var namedDigits = $"{_digitConversions[Digits]} {DigitGroupName}";
                    if(Digits == 1)
                    {
                        return namedDigits;
                    }

                    return $"{namedDigits}s";
                }

                return sb.ToString();
            }
        }
    }
}