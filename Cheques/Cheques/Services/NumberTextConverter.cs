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

            var wholeNumberText = string.Join(", ", BuildWholeNumberText(wholeNumber));
            var decimalsText = new DigitGroup(System.Convert.ToInt32(decimals), "cent").ToString();

            if (wholeNumber > 0 && decimals > 0)
            {
                return $"{wholeNumberText} and {decimalsText}";
            }

            if (wholeNumber > 0)
            {
                return wholeNumberText;
            }

            return decimalsText;
        }

        private IEnumerable<string> BuildWholeNumberText(decimal wholeNumber)
        {
            if (wholeNumber > 0)
            {
                var digitGroups = new Dictionary<string, int>();

                digitGroups["dollar"] = 0;
                digitGroups["thousand"] = 3;
                digitGroups["million"] = 6;
                digitGroups["billion"] = 9;
                digitGroups["trillion"] = 12;

                foreach (var kvp in digitGroups)
                {
                    var text = ConvertGroup(wholeNumber, kvp.Value, 3, kvp.Key);
                    if (!string.IsNullOrEmpty(text))
                    {
                        yield return text;
                    }
                }
            }
        }

        private string ConvertGroup(decimal number, int skip, int take, string digitGroupName)
        {
            var digitsString = new string(number.ToString().Skip(skip).Take(take).ToArray());
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

            public DigitGroup(int digits, string digitGroupName)
            {
                Digits = digits;
                DigitGroupName = digitGroupName;
            }

            public int Digits { get; }
            public string DigitGroupName { get; }

            public override string ToString()
            {
                var pluralisedDigitGroupName = Digits == 1 ? DigitGroupName : $"{DigitGroupName}s";
                return $"{ConvertDigits(Digits)} {pluralisedDigitGroupName}";
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

                    if (unitsDigit == 0)
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

            //private string Convert3DigitNumber(string digitsString)
            //{

            //    var tensDigit = System.Convert.ToInt32(digitsString[1].ToString()) * 10;
            //    var unitsDigit = System.Convert.ToInt32(digitsString[2].ToString());

            //    var sb = new StringBuilder();
            //    sb.Append($"{_digitConversions[hundredsDigit]} hundred");

            //    if (unitsDigit > 0)
            //    {
            //        if (_digitConversions.ContainsKey(tensDigit))
            //        {
            //            sb.Append($" and {_digitConversions[tensDigit]}");
            //        }
            //        else
            //        {
            //            sb.Append($"and {_digitConversions[tensDigit]} {_digitConversions[unitsDigit]}");
            //        }
            //    }

            //    return sb.ToString();
            //}
        }
    }
}