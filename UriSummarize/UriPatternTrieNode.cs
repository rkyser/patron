namespace UrlSummarize
{
    public class UriPatternTrieNode
    {
        public Dictionary<UriPatternSegment, UriPatternTrieNode> Children { get; private set; }
        public UriPattern? Pattern { get; set; }

        public UriPatternTrieNode()
        {
            Children = new Dictionary<UriPatternSegment, UriPatternTrieNode>();
        }

        public UriPatternTrieNode GetOrCreateNextNode(UriPatternSegment segment)
        {
            if (!Children.ContainsKey(segment))
            {
                Children[segment] = new UriPatternTrieNode();
            }
            return Children[segment];
        }

        public bool TryMatch(string segment, out UriPatternTrieNode? nextNode)
        {
            foreach (var kvp in Children)
            {
                if (kvp.Key.IsMatch(segment))
                {
                    nextNode = kvp.Value;
                    return true;
                }
            }

            nextNode = null;
            return false;
        }
    }
}
