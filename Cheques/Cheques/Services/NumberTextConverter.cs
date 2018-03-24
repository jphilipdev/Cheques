using Cheques.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
                var namedDigits = $"{ConvertDigits(Digits)} {DigitGroupName}";
                if (Digits == 1)
                {
                    return namedDigits;
                }

                return $"{namedDigits}s";
            }

            private string ConvertDigits(int digits)
            {
                // handle simple conversions
                if (_digitConversions.ContainsKey(Digits))
                {
                    return _digitConversions[Digits];
                }

                var digitsString = digits.ToString();
                if(digitsString.Length == 2)
                {
                    return Convert2DigitNumber(digitsString);
                }
                else if(digitsString.Length == 3)
                {
                    return Convert3DigitNumber(digitsString);
                }

                throw new NotSupportedException($"Cannot converts digits '{digits}'");
            }            

            private string Convert2DigitNumber(string digitsString)
            {
                var tensDigit = System.Convert.ToInt32(digitsString.Substring(0,1)) * 10;
                var unitsDigit = System.Convert.ToInt32(digitsString.Substring(1));

                return $"{_digitConversions[tensDigit]} {_digitConversions[unitsDigit]}";
            }

            private string Convert3DigitNumber(string digitsString)
            {
                var hundredsDigit = System.Convert.ToInt32(digitsString.Substring(0, 1)) * 100;
                var tensDigit = System.Convert.ToInt32(digitsString.Substring(1, 1)) * 10;
                var unitsDigit = System.Convert.ToInt32(digitsString.Substring(2));

                return $"{_digitConversions[hundredsDigit]} hundred {_digitConversions[tensDigit]} {_digitConversions[unitsDigit]}";
            }
        }
    }
}