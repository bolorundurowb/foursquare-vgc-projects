using System.Linq;

namespace api.Shared.Extensions
{
    public static class StringExtensions
    {
        private static readonly char _plusChar = '+';

        public static string Normalize(this string phoneNumber)
        {
            var validChars = phoneNumber.ToCharArray()
                .Where(x => char.IsDigit(x) || x == _plusChar);
            return new string(validChars.ToArray());
        }
    }
}