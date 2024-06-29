namespace UrlSummarize
{
    public class UriPatternSegmentMatchResult
    {
        public required string Input { get; set; }
        public UriPatternSegment? MatchingPattern { get; set; }
    }
}
