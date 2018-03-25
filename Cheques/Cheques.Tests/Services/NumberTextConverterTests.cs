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
        [TestCase(0.01, "One cent")]
        [TestCase(0.02, "Two cents")]
        [TestCase(0.03, "Three cents")]
        [TestCase(0.04, "Four cents")]
        [TestCase(0.05, "Five cents")]
        [TestCase(0.06, "Six cents")]
        [TestCase(0.07, "Seven cents")]
        [TestCase(0.08, "Eight cents")]
        [TestCase(0.09, "Nine cents")]

        // 2 digit cents
        [TestCase(0.10, "Ten cents")]
        [TestCase(0.11, "Eleven cents")]
        [TestCase(0.12, "Twelve cents")]
        [TestCase(0.13, "Thirteen cents")]
        [TestCase(0.14, "Fourteen cents")]
        [TestCase(0.15, "Fifteen cents")]
        [TestCase(0.16, "Sixteen cents")]
        [TestCase(0.17, "Seventeen cents")]
        [TestCase(0.18, "Eighteen cents")]
        [TestCase(0.19, "Nineteen cents")]
        [TestCase(0.20, "Twenty cents")]
        [TestCase(0.21, "Twenty one cents")]
        [TestCase(0.30, "Thirty cents")]
        [TestCase(0.40, "Forty cents")]
        [TestCase(0.50, "Fifty cents")]
        [TestCase(0.60, "Sixty cents")]
        [TestCase(0.70, "Seventy cents")]
        [TestCase(0.80, "Eighty cents")]
        [TestCase(0.90, "Ninety cents")]
        [TestCase(0.99, "Ninety nine cents")]

        // 1 digit dollars
        [TestCase(1.00, "One dollar")]
        [TestCase(1.01, "One dollar and one cent")]
        [TestCase(2.00, "Two dollars")]

        //2 digit dollars
        [TestCase(10.00, "Ten dollars")]
        [TestCase(10.01, "Ten dollars and one cent")]

        // 3 digit dollars
        [TestCase(100.00, "One hundred dollars")]
        [TestCase(100.01, "One hundred dollars and one cent")]
        [TestCase(101.00, "One hundred and one dollars")]
        [TestCase(101.01, "One hundred and one dollars and one cent")]

        // 4 digit dollars
        [TestCase(1000.00, "One thousand dollars")]
        [TestCase(1000.01, "One thousand dollars and one cent")]
        [TestCase(1100.00, "One thousand, one hundred dollars")]
        [TestCase(1100.01, "One thousand, one hundred dollars and one cent")]
        [TestCase(1110.00, "One thousand, one hundred and ten dollars")]
        [TestCase(1110.01, "One thousand, one hundred and ten dollars and one cent")]

        // 5 digit dollars
        [TestCase(10000.00, "Ten thousand dollars")]
        [TestCase(10000.01, "Ten thousand dollars and one cent")]
        [TestCase(10100.00, "Ten thousand, one hundred dollars")]
        [TestCase(10100.01, "Ten thousand, one hundred dollars and one cent")]
        [TestCase(10110.00, "Ten thousand, one hundred and ten dollars")]
        [TestCase(10110.01, "Ten thousand, one hundred and ten dollars and one cent")]

        // 6 digit dollars
        [TestCase(100000.00, "One hundred thousand dollars")]
        [TestCase(100000.01, "One hundred thousand dollars and one cent")]
        [TestCase(100100.00, "One hundred thousand, one hundred dollars")]
        [TestCase(100100.01, "One hundred thousand, one hundred dollars and one cent")]
        [TestCase(100110.00, "One hundred thousand, one hundred and ten dollars")]
        [TestCase(100110.01, "One hundred thousand, one hundred and ten dollars and one cent")]
        [TestCase(110000.00, "One hundred and ten thousand dollars")]
        [TestCase(110000.01, "One hundred and ten thousand dollars and one cent")]
        [TestCase(110100.00, "One hundred and ten thousand, one hundred dollars")]
        [TestCase(110100.01, "One hundred and ten thousand, one hundred dollars and one cent")]
        [TestCase(110110.00, "One hundred and ten thousand, one hundred and ten dollars")]
        [TestCase(110110.01, "One hundred and ten thousand, one hundred and ten dollars and one cent")]

        // 7 digit dollars
        [TestCase(1000000.00, "One million dollars")]
        [TestCase(1000000.01, "One million dollars and one cent")]
        [TestCase(1000100.00, "One million, one hundred dollars")]
        [TestCase(1000100.01, "One million, one hundred dollars and one cent")]
        [TestCase(1000110.00, "One million, one hundred and ten dollars")]
        [TestCase(1000110.01, "One million, one hundred and ten dollars and one cent")]
        [TestCase(1110000.00, "One million, one hundred and ten thousand dollars")]
        [TestCase(1110000.01, "One million, one hundred and ten thousand dollars and one cent")]
        [TestCase(1110100.00, "One million, one hundred and ten thousand, one hundred dollars")]
        [TestCase(1110100.01, "One million, one hundred and ten thousand, one hundred dollars and one cent")]
        [TestCase(1110110.00, "One million, one hundred and ten thousand, one hundred and ten dollars")]
        [TestCase(1110110.01, "One million, one hundred and ten thousand, one hundred and ten dollars and one cent")]
        public void GivenNumberInValidRange_WhenConverted_ThenNumberConvertedToText(decimal number, string expectedText)
        {
            // ARRANGE
            
            // ACT
            var result = _subject.Convert(number);

            // ASSERT
            Assert.AreEqual(expectedText, result);
        }

        [Test]
        public void GivenNumberIsDecimalMaxValue_WhenConverted_ThenNumberConvertedToText()
        {
            // ARRANGE
            var number = decimal.MaxValue;

            // ACT
            var result = _subject.Convert(number);

            // ASSERT
            Assert.AreEqual("Seventy nine octillion, two hundred and twenty eight septillion, one hundred and sixty two sextillion, five hundred and fourteen quintillion, two hundred and sixty four quadrillion, three hundred and thirty seven trillion, five hundred and ninety three billion, five hundred and forty three million, nine hundred and fifty thousand, three hundred and thirty five dollars", result);
        }
    }
}
