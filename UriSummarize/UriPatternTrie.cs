namespace UrlSummarize
{
    public class UriPatternTrie
    {
        private readonly UriPatternTrieNode root;

        public UriPatternTrie()
        {
            root = new UriPatternTrieNode();
        }

        public void Insert(UriPattern pattern)
        {
            var node = root;

            foreach (var segment in pattern)
            {
                node = node.GetOrCreateNextNode(segment);
            }

            // each terminal node represents a unique pattern
            // if we see a duplicate pattern, let's treat that as an error
            if (node.Pattern != null)
            {
                throw new Exception("pattern already exists");
            }

            node.Pattern = pattern;
        }

        /// <summary>
        /// Search for a pattern that matches the given URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public UriPatternMatchResult Match(string url)
        {
            var node = root;
            var result = new UriPatternMatchResult
            {
                Input = url,
                SegmentResults = GetLiteralSegments(url).Select(s => new UriPatternSegmentMatchResult() {
                    Input = s
                }).ToList()
            };
            var segmentMatchCount = 0;

            // to match the pattern two conditions must be met:
            //  1. each segment of the URL must form a complete graph from the root to a node
            //  2. the resulting node must have a Pattern object. if the node has no Pattern object, it is an "unregistered" pattern.
            foreach (var segmentResult in result.SegmentResults)
            {
                // now traverse the Trie and populate as many SegmentResults as possible with the
                // corresponding UriSegmentPattern.

                foreach (var kvp in node.Children)
                {
                    if (kvp.Key.IsMatch(segmentResult.Input))
                    {
                        // if the pattern (key) matches the URL path segment, then we keep a reference
                        // to the matching pattern. Then we move to the next node (kvp.Value)
                        segmentResult.MatchingPattern = kvp.Key;
                        node = kvp.Value;
                        segmentMatchCount++;
                        break;
                    }
                }

                // stop if there was not a match at the last node
                if (segmentResult.MatchingPattern == null)
                    break;
            }

            if (segmentMatchCount > 0)
            {
                result.Status = segmentMatchCount == result.SegmentResults.Count
                    ? ResultStatus.FullMatch
                    : ResultStatus.BeginningMatch;
            }

            return result;
        }

        private List<string> GetLiteralSegments(string url)
        {
            var uri = new Uri(url);
            return new List<string>(uri.AbsolutePath.Trim('/').Split('/'));
        }

        public void Display()
        {
            DisplayNode(root, "");
        }

        private void DisplayNode(UriPatternTrieNode node, string indent)
        {
            foreach (var child in node.Children)
            {
                switch (child.Key)
                {
                    case UriPatternLiteralSegment literalSegment:
                        Console.WriteLine($"{indent}/{literalSegment.LiteralValue}");
                        break;
                        
                    case UriPatternRegexSegment regexSegment:
                        Console.WriteLine($"{indent}/{{{regexSegment.SummaryName}:{regexSegment.RegexName}}}");
                        break;
                }

                DisplayNode(child.Value, indent + "  ");
            }
        }

    }
}
