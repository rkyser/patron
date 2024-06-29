namespace UriSummarize.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // test for conflicting pattern match types
        //  /api/things/{thingId:guid}
        //  /api/things/{thingId:int}

        // test for conflicting pattern summary names
        //  /api/things/{thisId:guid}
        //  /api/things/{thatId:guid}

        // test for multiples of the same summary name in the pattern
        //  /api/things/{theId:guid}/stuff/{theId:guid}
    }
}