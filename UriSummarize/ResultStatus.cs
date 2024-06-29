namespace UrlSummarize
{
    public enum ResultStatus
    {
        NoMatch,

        /// <summary>
        /// The beginning of the input matched the pattern
        /// </summary>
        BeginningMatch,

        /// <summary>
        /// All parts of the input matched the pattern
        /// </summary>
        FullMatch
    }
}
