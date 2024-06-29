namespace UrlSummarize
{
    public class UriSummarizer
    {
        private readonly UriPatternTrie urlPatternTrie;

        public UriSummarizer()
        {
            urlPatternTrie = new UriPatternTrie();
        }

        public UriSummarizer(IEnumerable<string> patterns)
            : this()
        {
            foreach (var pattern in patterns)
            {
                AddPattern(pattern);
            }
        }

        public void AddPattern(string pattern)
        {
            urlPatternTrie.Insert(new UriPattern(pattern));
        }

        public UriSummary Summarize(string uri)
        {
            var summary = new UriSummary
            {
                Input = uri
            };

            var patternMatch = urlPatternTrie.Match(uri);

            // compute the summary of each segment result
            var summarizedSegments = new List<string>();
            foreach (var s in patternMatch.SegmentResults)
            {
                switch (s.MatchingPattern)
                {
                    case UriPatternRegexSegment regex:
                        // when a segment has been matched with a particular regex, we want
                        // to replace the input with the regex' name. This right here is the 
                        // primary reason for all of this code (and also trying to be as performant as possible).
                        //
                        // we want to be able to replace unique values in the URI, like GUIDs, with a simple label
                        // in order to reduce cardinality.
                        summarizedSegments.Add("{" + regex.SummaryName + "}");

                        // keep track of each replacement by its SummaryName
                        summary.Replacements.Add(regex.SummaryName, s.Input);
                        break;

                    case UriPatternLiteralSegment literal:
                        // literal is a direct match to the input, so just fall through and use the Input value.
                    default:
                        // If there's no match, then use the input value as the segment.
                        // A "no match" (default) scenario would happen for partial match results, where there
                        // are more input segments than any matching pattern
                        summarizedSegments.Add(s.Input);
                        break;
                }
            }

            summary.Summary = string.Join("/", summarizedSegments);

            return summary;
        }
    }
}
