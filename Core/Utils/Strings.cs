namespace Utils
{
    using System;
    using System.Linq;

    public class Strings
    {
        public static string RandomString(int length)
        {
            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const string chars = "0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RemoveCharacters(string param)
        {
            param = param.Replace("(", "");
            param = param.Replace(")", "");
            param = param.Replace("-", "");
            param = param.Replace(" ", "");
            return param;
        }

    }
}
