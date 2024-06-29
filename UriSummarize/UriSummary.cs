using System.Text;

namespace UrlSummarize
{
    public class UriSummary
    {
        public required string Input {get; set;}
        public string? Summary {get; set;}
        public Dictionary<string, string> Replacements {get;set;} = new Dictionary<string, string>();
    }
}
