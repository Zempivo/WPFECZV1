using System.Text.RegularExpressions;

namespace WPFECZV1
{
    public static class PhoneValidator
    {
        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;
            return Regex.IsMatch(phone, @"^\+?[0-9]{10,15}$");
        }

        public static string FormatPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return phone;
            string digits = Regex.Replace(phone, @"[^\d+]", "");
            if (!digits.StartsWith("+"))
            {
                if (digits.StartsWith("8") && digits.Length == 11)
                {
                    digits = "+7" + digits.Substring(1);
                }
                else if (digits.Length == 10)
                {
                    digits = "+7" + digits;
                }
            }

            return digits;
        }
    }
}