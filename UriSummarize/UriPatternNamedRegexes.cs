using System.Text.RegularExpressions;

namespace UrlSummarize
{
    public static class UriPatternNamedRegexes
    {
        private static readonly Dictionary<string, Regex> regexByMatchTypeName;

        static UriPatternNamedRegexes()
        {
            regexByMatchTypeName = new Dictionary<string, Regex>(StringComparer.OrdinalIgnoreCase)
            {
                { "guid", new Regex(@"[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}", RegexOptions.Compiled) },
                { "int", new Regex(@"\d+", RegexOptions.Compiled) }
                // Add more default named matchers here as needed
            };
        }

        public static bool TryGet(string matchTypeName, out Regex? regex)
        {
            if (regexByMatchTypeName.TryGetValue(matchTypeName, out var r))
            {
                regex = r;
                return true;
            }
            regex = null;
            return false;
        }

        public static void Add(string matchTypeName, string matchPattern)
        {
            if (string.IsNullOrWhiteSpace(matchTypeName) || string.IsNullOrWhiteSpace(matchPattern))
            {
                throw new ArgumentException("Name and pattern must be non-empty strings.");
            }

            regexByMatchTypeName[matchTypeName] = new Regex(matchPattern, RegexOptions.Compiled);
        }
    }
}
