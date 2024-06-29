using System.Text.RegularExpressions;

namespace UrlSummarize
{
    public class UriPatternRegexSegment : UriPatternSegment, IEquatable<UriPatternRegexSegment>
    {
        public UriPatternRegexSegment()
        {
        }

        public override SegmentType Type => SegmentType.Pattern;
        public required string SummaryName {get; set;}
        public required string RegexName { get; set; }
        public required Regex Regex { get; set; }
        public override bool IsMatch(string value)
        {
            return Regex.IsMatch(value);
        }

        public override string ToString()
        {
            return $"Type: {Type}, SummaryName: {SummaryName}, RegexName: {RegexName}, RegexPattern: {Regex}";
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as UriPatternRegexSegment);
        }

        public bool Equals(UriPatternRegexSegment? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return SummaryName == other.SummaryName && RegexName == other.RegexName && Regex.ToString() == other.Regex.ToString() && Type == other.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SummaryName, RegexName, Regex.ToString(), Type);
        }

        public static bool operator ==(UriPatternRegexSegment left, UriPatternRegexSegment right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        public static bool operator !=(UriPatternRegexSegment left, UriPatternRegexSegment right)
        {
            return !(left == right);
        }
    }
}
