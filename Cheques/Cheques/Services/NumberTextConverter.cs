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
                var wholeNumberGroups = wholeNumber.ToString("#,#").Split(",").ToList();
                wholeNumberGroups.Reverse();

                var groupNames = new[] { "", "thousand", "million", "billion", "trillion", "quadrillion", "quintillion", "sextillion", "septillion", "octillion" };

                var digitGroups = wholeNumberGroups.Zip(groupNames, (wholeNumberGroup, groupName) => new Tuple<string, string>(wholeNumberGroup, groupName)).ToList();

                digitGroups.Reverse();

                foreach(var digitGroup in digitGroups)
                {
                    if (System.Convert.ToInt32(digitGroup.Item1) != 0)
                    {
                        var text = ConvertGroup(digitGroup.Item1, digitGroup.Item2);
                        if (!string.IsNullOrEmpty(text))
                        {
                            yield return text;
                        }
                    }
                }
            }
        }

        private string ConvertGroup(string digitsString, string digitGroupName)
        {
            if (string.IsNullOrEmpty(digitsString))
            {
                return null;
            }

            var digitGroup = new DigitGroup(System.Convert.ToInt32(digitsString), digitGroupName);
            return digitGroup.ToString();
        }


        private string ConvertGroup(decimal number, int skip, int take, string digitGroupName)
        {
            var digitsString = new string(number.ToString().Reverse().Skip(skip).Take(take).ToArray());
            if (string.IsNullOrEmpty(digitsString))
            {
                return null;
            }

            var digitGroup = new DigitGroup(System.Convert.ToInt32(digitsString), digitGroupName);
            return digitGroup.ToString();
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

                var tensDigit = System.Convert.ToInt32(digitsString[digitsString.Length-2].ToString()) * 10;
                var unitsDigit = System.Convert.ToInt32(digitsString[digitsString.Length-1].ToString());

                if (digitsString.Length == 3)
                {
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
                    sb.Append(_digitConversions[tensDigit + unitsDigit]);
                }
                else
                {
                    sb.Append($"{_digitConversions[tensDigit]} {_digitConversions[unitsDigit]}");
                }

                return sb.ToString();
            }

            private string Convert2DigitNumber(string digitsString)
            {
                var tensDigit = System.Convert.ToInt32(digitsString[0].ToString()) * 10;
                var unitsDigit = System.Convert.ToInt32(digitsString[1].ToString());

                return $"{_digitConversions[tensDigit]} {_digitConversions[unitsDigit]}";
            }
        }
    }
}