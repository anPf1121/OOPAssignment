using System.Text.RegularExpressions;

namespace Standardize {
    public class Input {
        
        public static string NameStandardize(string? name)
        {
            if (name != null)
            {
                name = name.Trim();
                name = name.ToLower();
                while (name.IndexOf("  ") != -1)
                {
                    name = name.Remove(name.IndexOf("  "), 1);
                }
                string[] s = name.Split(' ');
                string afterFormat = "";
                for (int i = 0; i < s.Length; ++i)
                {
                    string first = s[i].Substring(0, 1);
                    string another = s[i].Substring(1, s[i].Length - 1);
                    afterFormat += first.ToUpper() + another + " ";
                }
                afterFormat = afterFormat.Remove(afterFormat.LastIndexOf(' '), 1);
                return afterFormat;
            }
            return "";
        }
        public static bool EmailStandardize(string? email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            return Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }
        public static bool PhoneNumberStandardize(ref string? phoneNumber)
        {
            if (phoneNumber == null)
                return false;

            if (phoneNumber.Length != 10)
                return false;

            if (Regex.IsMatch(phoneNumber, @"^\d+$"))
            {
                phoneNumber = Regex.Replace(phoneNumber, @"(\d{3})(\d{3})(\d{4})", "$1.$2.$3");
            }
            else
                return false;

            return true;
        }
        public static bool BirthDayStandardize(ref string? birthDay)
        {
            if (string.IsNullOrEmpty(birthDay)) return false;

            if (birthDay.Length != 10) return false;

            if (birthDay.Substring(2, 1) != "/" && birthDay.Substring(5, 1) != "/") return false;

            return Regex.IsMatch(birthDay, @"^\d");
        }
    }
}