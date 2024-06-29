using System.Collections;
using System.Net;
using System.Text.RegularExpressions;

namespace UrlSummarize
{
    /// <summary>
    /// The UriPattern class represents a pattern for matching and parsing the request URI of a URL.
    /// The request URI consists of the path and query arguments of the URL, which is everything after the domain part of the URL.
    /// 
    /// For example, given the URL:
    /// https://example.com/users/123/profile?sort=ascending&limit=10
    /// 
    /// - Scheme: https
    /// - Host/Domain: example.com
    /// - Path: /users/123/profile
    /// - Query: ?sort=ascending&limit=10
    /// - Request URI: /users/123/profile?sort=ascending&limit=10
    /// 
    /// The UriPattern class allows for defining and matching patterns within the request URI using literal segments and regex-based segments.
    /// </summary>
    public class UriPattern : IReadOnlyCollection<UriPatternSegment>
    {
        private static readonly Regex SegmentPattern = new Regex(@"\{(?<name>\w+):(?<match>\w+)\}", RegexOptions.Compiled);
        private List<UriPatternSegment> segments;

        public UriPattern(string pattern)
        {
            segments = SegmentUriPattern(pattern);
        }

        public int Count => segments.Count;
        
        private List<UriPatternSegment> SegmentUriPattern(string url)
        {
            var segments = ParseUrlSegments(url);
            var urlSegments = new List<UriPatternSegment>();

            foreach (var segment in segments)
            {
                var match = SegmentPattern.Match(segment);
                if (match.Success)
                {
                    var name = match.Groups["name"].Value;
                    var matchKey = match.Groups["match"].Value;

                    if (UriPatternNamedRegexes.TryGet(matchKey, out var regex))
                    {
                        if (regex == null) throw new Exception("got null regex unexpectedly");
                        
                        urlSegments.Add(new UriPatternRegexSegment
                        {
                            SummaryName = name,
                            RegexName = matchKey,
                            Regex = regex
                        });
                    }
                    else
                    {
                        // TODO: maybe we should test if the matchKey is a regex?
                        // Handle case where named matcher is not found
                        throw new Exception($"Named matcher '{matchKey}' is not defined.");
                    }
                }
                else
                {
                    urlSegments.Add(new UriPatternLiteralSegment
                    {
                        LiteralValue = segment
                    });
                }
            }
            return urlSegments;
        }

        private static List<string> ParseUrlSegments(string url)
        {
            var uri = new Uri(url);
            return new List<string>(uri.AbsolutePath.Trim('/').Split('/').Select(WebUtility.UrlDecode));
        }

        public IEnumerator<UriPatternSegment> GetEnumerator()
        {
            return segments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return segments.GetEnumerator();
        }
    }
}
