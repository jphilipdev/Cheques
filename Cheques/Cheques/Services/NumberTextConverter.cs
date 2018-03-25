using Cheques.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cheques.Services
{
    public class NumberTextConverter : INumberTextConverter
    {
        public string Convert(decimal number)
        {
            var wholeNumber = Math.Floor(number);
            var decimals = (number - wholeNumber) * 100;

            var text = Convert(wholeNumber, decimals);
            return ToTitleCase(text);
        }        

        private string Convert(decimal wholeNumber, decimal decimals)
        {
            var pluralisedCurrencyName = wholeNumber == 1 ? "dollar" : "dollars";

            if (wholeNumber > 0 && decimals > 0)
            {
                return $"{GetWholeNumberText(wholeNumber)} {pluralisedCurrencyName} and {GetDecimalsText(decimals)}";
            }

            if (wholeNumber > 0)
            {
                return $"{GetWholeNumberText(wholeNumber)} {pluralisedCurrencyName}";
            }

            return GetDecimalsText(decimals);
        }

        private string GetWholeNumberText(decimal wholeNumber)
        {
            return string.Join(", ", BuildWholeNumberText(wholeNumber));
        }

        private string GetDecimalsText(decimal decimals)
        {
            var cents = new DigitGroup(System.Convert.ToInt32(decimals), "cent").ToString();
            return decimals == 1 ? cents : $"{cents}s";
        }

        private IEnumerable<string> BuildWholeNumberText(decimal wholeNumber)
        {
            if (wholeNumber > 0)
            {
                // convert 1234 to ["432", "1"]
                var wholeNumberGroups = wholeNumber.ToString("#,#").Split(",").ToList();
                wholeNumberGroups.Reverse();

                // convert to [{ Item1: "432", Item2: "", { Item1: "1", Item2: "thousand" }]
                var groupNames = new[] { "", "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion", "octillion" };
                var digitGroups = wholeNumberGroups.Zip(groupNames, (wholeNumberGroup, groupName) => new Tuple<string, string>(wholeNumberGroup, groupName)).ToList();

                // put back into the order the digit groups should be written
                digitGroups.Reverse();

                foreach(var digitGroup in digitGroups)
                {
                    // discard any digit groups that should not be written to avoid e.g. "zero thousand"
                    if (System.Convert.ToInt32(digitGroup.Item1) != 0)
                    {
                        var text = new DigitGroup(System.Convert.ToInt32(digitGroup.Item1), digitGroup.Item2).ToString();
                        if (!string.IsNullOrEmpty(text))
                        {
                            yield return text;
                        }
                    }
                }
            }
        }

        private string ToTitleCase(string text)
        {
            return text.First().ToString().ToUpper() + text.Substring(1);
        }

        private class DigitGroup
        {
            private readonly Dictionary<int, string> _digitConversions = new Dictionary<int, string>()
            {
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

            public DigitGroup(int digits, string digitGroupName)
            {
                Digits = digits;
                DigitGroupName = digitGroupName;
            }

            public int Digits { get; }
            public string DigitGroupName { get; }

            public override string ToString()
            {   
                if(string.IsNullOrEmpty(DigitGroupName))
                {
                    return ConvertDigits(Digits);
                }
                return $"{ConvertDigits(Digits)} {DigitGroupName}";
            }

            private string ConvertDigits(int digits)
            {
                if (_digitConversions.ContainsKey(Digits))
                {
                    return _digitConversions[Digits];
                }

                var sb = new StringBuilder();

                var digitsString = digits.ToString();

                // convert e.g. 2 to 20 so it can be looked up in _digitConversions
                var tensDigit = System.Convert.ToInt32(digitsString[digitsString.Length-2].ToString()) * 10;
                var unitsDigit = System.Convert.ToInt32(digitsString[digitsString.Length-1].ToString());

                if (digitsString.Length == 3)
                {
                    // if there is 3 digits in a group the leading digit is always "n hundred"
                    var hundredsDigit = System.Convert.ToInt32(digitsString[0].ToString());
                    sb.Append($"{_digitConversions[hundredsDigit]} hundred");

                    if (tensDigit == 0 && unitsDigit == 0)
                    {
                        return sb.ToString();
                    }

                    sb.Append(" and ");
                }

                if (_digitConversions.ContainsKey(tensDigit + unitsDigit))
                {
                    // convert e.g. the "12" from "112" 
                    sb.Append(_digitConversions[tensDigit + unitsDigit]);
                }
                else
                {
                    // convert e.g. the "21" from "121" 
                    sb.Append($"{_digitConversions[tensDigit]} {_digitConversions[unitsDigit]}");
                }

                return sb.ToString();
            }
        }
    }
}