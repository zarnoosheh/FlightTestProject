using System.Text.RegularExpressions;

namespace DataAccessLayer.Validation
{
    public static class Helpers
    {



        public static bool IsValidPhoneNumber(string input)
        {
            if (input.Length != 11)
                return false;
            input = input.Trim();

            Regex regex1 = new Regex(@"/([0-9\s\-]{7,})(?:\s*(?:#|x\.?|ext\.?|extension)\s*(\d+))?$/");

            // Step 2: call Match on Regex instance.
            Match match1 = regex1.Match(input);
            // Step 3: test for Success.

            if (match1.Success)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool IsNumber(string input)
        {
            var match = Regex.Match(input, @"^\d+$", RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                // does not match
                return false;
            }
            return true;
        }



    }
}
