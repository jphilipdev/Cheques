using Cheques.Services.Interfaces;

namespace Cheques.Services
{
    public class NumberTextConverter : INumberTextConverter
    {
        public string Convert(decimal number)
        {
            var test = new DigitGroup();
            return null;
        }

        private class DigitGroup
        {

        }
    }
}