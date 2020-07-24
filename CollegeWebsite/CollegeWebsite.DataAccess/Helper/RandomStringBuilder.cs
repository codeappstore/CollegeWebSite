using System;
using System.Text;

namespace CollegeWebsite.DataAccess.Helper
{
    public class RandomStringBuilder
    {
        public enum PorposeOfString
        {
            PASSWORD,
            ID,
            TOKEN,
            DEFAULT
        }
        string[] dictionary =
            {
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_-@!#+.",
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890",
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_"
        };
        private readonly int minSize = 6;

        // RandomStringBuilder random = new RandomStringBuilder();
        // Console.WriteLine("PASSWORD: " + random.RandStringGenerator((int) PorposeOfString.PASSWORD));
        // Console.WriteLine("ID: " + random.RandStringGenerator((int) PorposeOfString.ID));
        // Console.WriteLine("TOKEN: " + random.RandStringGenerator((int) PorposeOfString.TOKEN));
        // Console.WriteLine("DEFAULT: " + random.RandStringGenerator((int) PorposeOfString.DEFAULT));
        public string RandStringGenerator(short purpose, int maxSize = 52, bool capital = false, bool small = false)
        {
            switch (purpose)
            {
                case 0:
                    var password = Password(maxSize, purpose);
                    return capital == true ? (UpperCase(password)) : small == true ? (LowerCase(password)) : (password);
                case 1:
                    var id = Id(maxSize, purpose);
                    return capital == true ? (UpperCase(id)) : small == true ? (LowerCase(id)) : (id);
                case 2:
                    var token = Token(maxSize, purpose);
                    return capital == true ? (UpperCase(token)) : small == true ? (LowerCase(token)) : (token);
                default:
                    var defaultStr = "rAnD0mSt3n_g3V34at8O";
                    return capital == true ? (UpperCase(defaultStr)) : small == true ? (LowerCase(defaultStr)) : (defaultStr);
            }
        }

        private string Password(int maxSize, short purpose)
        {
            return ("p@sSw0rd" + RndString(maxSize, dictionary[purpose]));
        }
        private string Id(int maxSize, short purpose)
        {
            return ("uId" + RndString(maxSize, dictionary[purpose]));
        }
        private string Token(int maxSize, short purpose)
        {
            return ("tOkEN" + RndString(maxSize, dictionary[purpose]));
        }

        private string UpperCase(string input)
        {
            return input.ToUpperInvariant();
        }

        private string LowerCase(string input)
        {
            return input.ToLowerInvariant();
        }

        private string RndString(int maxSize, string dictionary)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();
            var stringLength = random.Next(minSize, maxSize + 1);
            while (stringLength-- > 0)
            {
                stringBuilder.Append(dictionary[random.Next(dictionary.Length)]);
            }
            var password = stringBuilder.ToString();
            return password;
        }
    }
}
