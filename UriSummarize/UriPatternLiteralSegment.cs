namespace UrlSummarize
{
    public class UriPatternLiteralSegment : UriPatternSegment, IEquatable<UriPatternLiteralSegment>
    {
        public override SegmentType Type => SegmentType.Literal;
        public required string LiteralValue { get; set; }

        public override bool IsMatch(string value)
        {
            return LiteralValue.Equals(value);
        }

        public override string ToString()
        {
            return $"Type: {Type}, LiteralValue: {LiteralValue}";
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as UriPatternLiteralSegment);
        }

        public bool Equals(UriPatternLiteralSegment? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return LiteralValue == other.LiteralValue && Type == other.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LiteralValue, Type);
        }

        public static bool operator ==(UriPatternLiteralSegment left, UriPatternLiteralSegment right)
        {
            if (left is null)
                return right is null;

            return left.Equals(right);
        }

        public static bool operator !=(UriPatternLiteralSegment left, UriPatternLiteralSegment right)
        {
            return !(left == right);
        }
    }
}
