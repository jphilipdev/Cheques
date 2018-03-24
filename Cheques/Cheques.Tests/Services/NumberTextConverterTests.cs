using Cheques.Services;
using NUnit.Framework;

namespace Cheques.Tests
{
    [TestFixture]
    public class NumberTextConverterTests
    {
        private NumberTextConverter _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new NumberTextConverter();
        }

        // 1 digit cents
        [TestCase(0.01, "one cent")]
        [TestCase(0.02, "two cents")]
        [TestCase(0.03, "three cents")]
        [TestCase(0.04, "four cents")]
        [TestCase(0.05, "five cents")]
        [TestCase(0.06, "six cents")]
        [TestCase(0.07, "seven cents")]
        [TestCase(0.08, "eight cents")]
        [TestCase(0.09, "nine cents")]

        // 2 digit cents
        [TestCase(0.10, "ten cents")]
        [TestCase(0.11, "eleven cents")]
        [TestCase(0.12, "twelve cents")]
        [TestCase(0.13, "thirteen cents")]
        [TestCase(0.14, "fourteen cents")]
        [TestCase(0.15, "fifteen cents")]
        [TestCase(0.16, "sixteen cents")]
        [TestCase(0.17, "seventeen cents")]
        [TestCase(0.18, "eighteen cents")]
        [TestCase(0.19, "nineteen cents")]
        [TestCase(0.20, "twenty cents")]
        [TestCase(0.21, "twenty one cents")]
        [TestCase(0.30, "thirty cents")]
        [TestCase(0.40, "forty cents")]
        [TestCase(0.50, "fifty cents")]
        [TestCase(0.60, "sixty cents")]
        [TestCase(0.70, "seventy cents")]
        [TestCase(0.80, "eighty cents")]
        [TestCase(0.90, "ninety cents")]
        [TestCase(0.99, "ninety nine cents")]

        // 1 digit dollars
        [TestCase(1.00, "one dollar")]
        [TestCase(1.01, "one dollar and 1 cent")]
        [TestCase(2.00, "two dollars")]

        //2 digit dollars
        [TestCase(10.00, "ten dollars")]
        [TestCase(10.01, "ten dollars and 1 cent")]

        // 3 digit dollars
        [TestCase(100.00, "one hundred dollars")]
        [TestCase(100.01, "one hundred dollars and 1 cent")]
        [TestCase(101.00, "one hundred and one dollars")]
        [TestCase(100.01, "one hundred and one dollars and 1 cent")]

        // 4 digit dollars
        [TestCase(1000.00, "one thousand dollars")]
        [TestCase(1000.01, "one thousand dollars and 1 cent")]
        [TestCase(1100.00, "one thousand, one hundred dollars")]
        [TestCase(1100.01, "one thousand, one hundred dollars and 1 cent")]
        [TestCase(1110.00, "one thousand, one hundred and ten dollars")]
        [TestCase(1110.01, "one thousand, one hundred and ten dollars and 1 cent")]

        // 5 digit dollars
        [TestCase(10000.00, "ten thousand dollars")]
        [TestCase(10000.01, "ten thousand dollars and 1 cent")]
        [TestCase(10100.00, "ten thousand, one hundred dollars")]
        [TestCase(10100.01, "ten thousand, one hundred dollars and 1 cent")]
        [TestCase(10110.00, "ten thousand, one hundred and ten dollars")]
        [TestCase(10110.01, "ten thousand, one hundred and ten dollars and 1 cent")]

        // 6 digit dollars
        [TestCase(100000.00, "one hundred thousand dollars")]
        [TestCase(100000.01, "one hundred thousand dollars and 1 cent")]
        [TestCase(100100.00, "one hundred thousand, one hundred dollars")]
        [TestCase(100100.01, "one hundred thousand, one hundred dollars and 1 cent")]
        [TestCase(100110.00, "one hundred thousand, one hundred and ten dollars")]
        [TestCase(100110.01, "one hundred thousand, one hundred and ten dollars and 1 cent")]
        [TestCase(110000.00, "one hundred and ten thousand dollars")]
        [TestCase(110000.01, "one hundred and ten thousand dollars and 1 cent")]
        [TestCase(110100.00, "one hundred and ten thousand, one hundred dollars")]
        [TestCase(110100.01, "one hundred and ten thousand, one hundred dollars and 1 cent")]
        [TestCase(110110.00, "one hundred and ten thousand, one hundred and ten dollars")]
        [TestCase(110110.01, "one hundred and ten thousand, one hundred and ten dollars and 1 cent")]

        // 7 digit dollars
        [TestCase(1000000.00, "one million dollars")]
        [TestCase(1000000.01, "one million dollars and 1 cent")]
        [TestCase(1000100.00, "one million, one hundred dollars")]
        [TestCase(1000100.01, "one million, one hundred dollars and 1 cent")]
        [TestCase(1000110.00, "one million, one hundred and ten dollars")]
        [TestCase(1000110.01, "one million, one hundred and ten dollars and 1 cent")]
        [TestCase(1100000.00, "one million, one hundred and ten thousand dollars")]
        [TestCase(1110000.01, "one million, one hundred and ten thousand dollars and 1 cent")]
        [TestCase(1110100.00, "one million, one hundred and ten thousand, one hundred dollars")]
        [TestCase(1110100.01, "one million, one hundred and ten thousand, one hundred dollars and 1 cent")]
        [TestCase(1110110.00, "one million, one hundred and ten thousand, one hundred and ten dollars")]
        [TestCase(1110110.01, "one million, one hundred and ten thousand, one hundred and ten dollars and 1 cent")]
        public void GivenNumberInValidRange_WhenConverted_ThenNumberConvertedToText(decimal number, string expectedText)
        {
            // ARRANGE
            
            // ACT
            var result = _subject.Convert(number);

            // ASSERT
            Assert.AreEqual(expectedText, result);
        }
    }
}
