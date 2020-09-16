using System.Text.RegularExpressions;

namespace College.Database.Helper
{
    public class RandomStringBuilder
    {
        public enum PurposeOfString
        {
            PASSWORD,
            ID,
            TOKEN,
            DEFAULT
        }

        public string StripHtml(string input)
        {
            // Will this simple expression replace all tags???
            var tagsExpression = new Regex(@"</?.+?>");
            return tagsExpression.Replace(input, " ");
        }
    }
}