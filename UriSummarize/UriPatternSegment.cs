namespace UrlSummarize
{
    public abstract class UriPatternSegment
    {
        public abstract SegmentType Type { get; }
        /// <summary>
        /// returns true if this pattern segment matches the given value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool IsMatch(string value);
    }
}
