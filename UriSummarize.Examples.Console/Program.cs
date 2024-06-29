using Pastel;

namespace UrlSummarize
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var uriSummarizer = new UriSummarizer(new List<string>
            {
                "/users/{userId:guid}/profile/{profileId:int}",
                "/products/{productId:guid}/details/{detailId:int}",
                "/orders/{orderId:guid}/history/{historyId:int}",
                "/invoices/{invoiceId:guid}/details/{itemId:int}",
                "/projects/{projectId:guid}/tasks/{taskId:int}",
                "/events/{eventId:guid}/register/{registrationId:int}",
                "/sessions/{sessionId:guid}/data/{dataId:int}",
                "/transactions/{transactionId:guid}/details/{detailId:int}",
                "/blog/{postId:guid}/comments/{commentId:int}",
                "/support/{ticketId:guid}/comments/{commentId:int}"
            });

            var testValues = new List<(string value, bool isValid)>
            {
                ("/users/123e4567-e89b-12d3-a456-426614174000/profile/1", true),
                ("/users/123e4567-e89b-12d3-a456-42661417400z/profile/1", false),
                ("/users/123e4567-e89b-12d3-a456-426614174000/profile/abc", false),

                ("/products/123e4567-e89b-12d3-a456-426614174001/details/10", true),
                ("/products/123e4567-e89b-12d3-a456-42661417400x/details/10", false),
                ("/products/123e4567-e89b-12d3-a456-426614174001/details/xyz", false),

                ("/orders/123e4567-e89b-12d3-a456-426614174002/history/20", true),
                ("/orders/123e4567-e89b-12d3-a456-42661417400y/history/20", false),
                ("/orders/123e4567-e89b-12d3-a456-426614174002/history/def", false),

                ("/invoices/123e4567-e89b-12d3-a456-426614174003/details/30", true),
                ("/invoices/123e4567-e89b-12d3-a456-42661417400z/details/30", false),
                ("/invoices/123e4567-e89b-12d3-a456-426614174003/details/ghi", false),

                ("/projects/123e4567-e89b-12d3-a456-426614174004/tasks/40", true),
                ("/projects/123e4567-e89b-12d3-a456-42661417400a/tasks/40", true),
                ("/projects/123e4567-e89b-12d3-a456-426614174004/tasks/jkl", false),

                ("/events/123e4567-e89b-12d3-a456-426614174005/register/50", true),
                ("/events/123e4567-e89b-12d3-a456-42661417400b/register/50", true),
                ("/events/123e4567-e89b-12d3-a456-426614174005/register/mno", false),

                ("/sessions/123e4567-e89b-12d3-a456-426614174006/data/60", true),
                ("/sessions/123e4567-e89b-12d3-a456-42661417400c/data/60", true),
                ("/sessions/123e4567-e89b-12d3-a456-426614174006/data/pqr", false),

                ("/transactions/123e4567-e89b-12d3-a456-426614174007/details/70", true),
                ("/transactions/123e4567-e89b-12d3-a456-42661417400d/details/70", true),
                ("/transactions/123e4567-e89b-12d3-a456-426614174007/details/stu", false),

                ("/blog/123e4567-e89b-12d3-a456-426614174008/comments/80", true),
                ("/blog/123e4567-e89b-12d3-a456-42661417400e/comments/80", true),
                ("/blog/123e4567-e89b-12d3-a456-426614174008/comments/vwx", false),

                ("/support/123e4567-e89b-12d3-a456-426614174009/comments/90", true),
                ("/support/123e4567-e89b-12d3-a456-42661417400f/comments/90", true),
                ("/support/123e4567-e89b-12d3-a456-426614174009/comments/yz", false),

                // trailing slashes should be ignored
                ("/support/123e4567-e89b-12d3-a456-426614174009/comments/90/", true),

                // we want to support partial matches
                ("/support/123e4567-e89b-12d3-a456-426614174009/comments/90/more/things", true),
                ("/support/123e4567-e89b-12d3-a456-426614174009", true),
            };

            foreach (var test in testValues)
            {
                var summary = uriSummarizer.Summarize(test.value);
                Console.WriteLine($"input: {summary.Input}");
                Console.WriteLine($"summary: {summary.Summary}");
            }
        }
    }
}
