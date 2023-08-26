using System;
using Reddit;


namespace AuthenticationTokenRetriever
{
    class Program
    {
        static void Main(string[] args)
        {
            var reddit = new RedditClient(appId: "YourRedditAppID", appSecret: "YourRedditAppSecret", refreshToken: "YourBotUserRefreshToken");

        }
    }
}
