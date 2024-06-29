namespace UrlSummarize
{
    public class UriPatternMatchResult
    {
        public required string Input { get; set; }
        public ResultStatus Status { get; set; }
        public bool IsMatch => Status != ResultStatus.NoMatch;
        public List<UriPatternSegmentMatchResult> SegmentResults { get; set; } = new List<UriPatternSegmentMatchResult>();
    }
}
