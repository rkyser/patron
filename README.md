# URL Summarizer

Experimental code to develop a technique for reducing the cardinality of URLs.

The goal was to take URIs that look like this:

```
/events/123e4567-e89b-12d3-a456-426614174005/register/50
```

and summarize them to this:

```
/events/{eventId}/register/{regId}
```

using developer-defined patterns such as:

```
/events/{eventId:guid}/register/{regId:int}
```

## Getting Started

First, you need to create a `UriSummarizer` and provide it with URI pattern(s) like so:

```C#
var uriSummarizer = new UriSummarizer(new List<string>
{
    "/users/{userId:guid}/profile/{profileId:int}"
});
```

Patterns can be added after the `UriSummarizer` is created too using the `AddPattern()` method.

```C#
var uriSummarizer = new UriSummarizer();
uriSummarizer.AddPattern("/users/{userId:guid}/profile/{profileId:int}");
```

Then to summarize a URI, use the `Summarize()` method which will return a `UriSummary`.

```C#
var summary = uriSummarizer.Summarize("/users/1234/profile/5678");

// prints "/users/1234/profile/5678"
Console.WriteLine(summary.Input);

// prints
Console.WriteLine(summary.Summary);
```