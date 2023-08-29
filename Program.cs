using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Reddit;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;

namespace RedditListenerApp
{
    public class Program
    {
       

        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();


            string appId = "77Vhm2YGgk703wHWBmX9ig";
            string refreshToken = "760730588485-UfbKjs0RFOSqsyz-_rEG3iHHtgBROg";
            string accessToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IlNIQTI1NjpzS3dsMnlsV0VtMjVmcXhwTU40cWY4MXE2OWFFdWFyMnpLMUdhVGxjdWNZIiwidHlwIjoiSldUIn0.eyJzdWIiOiJ1c2VyIiwiZXhwIjoxNjkzMzM0NjA3Ljc0NDg0OSwiaWF0IjoxNjkzMjQ4MjA3Ljc0NDg0OSwianRpIjoiVGlWYklKNHIzZnVKTkc1TlVHMmlUOUZsUlRjc0hRIiwiY2lkIjoiNzdWaG0yWUdnazcwM3dIV0JtWDlpZyIsImxpZCI6InQyXzlwaDNjbXJwIiwiYWlkIjoidDJfOXBoM2NtcnAiLCJsY2EiOjE2MTAwMzUzMTMwMDAsInNjcCI6ImVKeEVqa0Z1eFRBSVJPX0NPamVxdXNBRzU2UGFJUUtjS3JldnFQUGIzV2dHWnQ0SFZHTWlDWWNOaGxMVkkwektETFhIR1NqOUwydXl3d1ktaTFlVHdxbkRabzFwVEI1MzUzeTZOREw1bGk5aGtzam4yMmY1M19GWnh2S1Z1dTVMbk9wdmlOWlJMT193NHVWb3ZQZ1h5QmdKTmpoTkxnd2U3STQ3ci1CVXkwNGhQa0xpaGcyNlhEend3RDFic0ZhZHg3TWFocTFKZlNpZjBqZXNVcHBMT2ZjR0c3ekVReTA3Rjl2blR3QUFBUF9fT1NCc2h3IiwicmNpZCI6IkNkLUtqNG5POUdZcDBPOTBZd2dpS0tZQ0FzUk03Rkw0TURreUFBT3dXSGsiLCJmbG8iOjh9.FN9ySCDOpD47GHqmZ3a__VP5Y0KgGVZw7GdN4P0GfUT_rNPyx9CFa9MBueQfVNi - Nca6S8o0YArvdt0DvxigafQiS54RBTSQ - cF7kA2ux6a5WKdhrMtbPhqKDjU_lr37ja - ueTBnr3yaQbz5m32WpYz17IVgbTbSsbKpf3y_oD6FlHyMICt5xNidCB4ecg4PL3Hi9bt - suvDd_D4rKq0svqMd0 - 10E4XGYmB25flZmpWQciYwil8g5StXM55A6gzmQjZGSOE4aoDaMauUTZd7UKfCo5mevTUxCMHDcubvQ2q2F3mKyO7iCyYD1Yn2u8_r6lrgFC1coEFpPuZYEX - mg";

            if (args.Length < 2)
            {
                Console.WriteLine("Usage: Example <Reddit App ID> <Reddit Refresh Token> [Reddit Access Token]");
            }
            else if(appId != null)
            {
                // Initialize the API library instance.  --Kris
                RedditClient reddit = new RedditClient(appId: appId, refreshToken: refreshToken, accessToken: accessToken);

                // Get info on the Reddit user authenticated by the OAuth credentials.  --Kris
                User me = reddit.Account.Me;

                Console.WriteLine("Username: " + me.Name);
                Console.WriteLine("Cake Day: " + me.Created.ToString("D"));

                // Get post and comment histories (note that pinned profile posts appear at the top even on new sort; use "newForced" sort as a workaround).  --Kris
                List<Post> postHistory = me.GetPostHistory(sort: "newForced");
                List<Comment> commentHistory = me.GetCommentHistory(sort: "new");

                if (postHistory.Count > 0)
                {
                    Console.WriteLine("Most recent post: " + postHistory[0].Title);
                }
                if (commentHistory.Count > 0)
                {
                    Console.WriteLine("Most recent comment: " + commentHistory[0].Body);
                }

                // Create a new subreddit.  --Kris
                //Subreddit newSub = reddit.Subreddit("RDNBotSub", "Test Subreddit", "Test sub created by Reddit.NET", "My sidebar.").Create();

                // Get the Reddit front page.  --Kris
                Console.WriteLine("Top 3 Posts on the Front Page:");
                foreach (Post frontPost in reddit.GetFrontPage(limit: 3))
                {
                    Console.WriteLine("[r/" + frontPost.Subreddit + "] [u/" + frontPost.Author + "] " + frontPost.Title);
                }

                // If you've already recently called GetFrontPage(), you can access the cached results via the FrontPage property.  If null or expired, it automatically calls GetFrontPage().  --Kris
                Console.WriteLine("Date of Best Post: " + reddit.FrontPage[0].Created.ToString("D"));

                // Get info about a subreddit.  --Kris
                Subreddit sub = reddit.Subreddit("AskReddit").About();

                Console.WriteLine("Subreddit Name: " + sub.Name);
                Console.WriteLine("Subreddit Fullname: " + sub.Fullname);
                Console.WriteLine("Subreddit Title: " + sub.Title);
                Console.WriteLine("Subreddit Description: " + sub.Description);

                // Get new posts from this subreddit.  --Kris
                List<Post> newPosts = sub.Posts.New;

                Console.WriteLine("Retrieved " + newPosts.Count.ToString() + " new posts.");

                // Monitor new posts on this subreddit for a minute.  --Kris
                Console.WriteLine("Monitoring " + sub.Name + " for new posts....");

                sub.Posts.NewUpdated += C_NewPostsUpdated;
                sub.Posts.MonitorNew();  // Toggle on.

                /*
                 * But wait, there's more!  We can monitor posts on multiple subreddits at once (delay is automatically multiplied to keep us under speed the limit).
                 * If you want to see something really crazy, check out Reddit.NETTests.ControllerTests.WorkflowTests.StressTests.AsyncTests.PoliceState().  That 
                 * test creates 60 new posts on the test subreddit (and 10 comments for each post), while simultaneously monitoring each of these 60 posts for new 
                 * comments, all while monitoring the test subreddit for new posts.  Of course, the delay between each monitoring thread's request would be roughly 
                 * 90 seconds in this case (the delay is the number of things being monitored times 1.5 seconds), but the library handles all that for you automatically.
                 * 
                 * I strongly recommend you closely examine the Reddit.NETTests project if you wish to become well-versed in developing using this library.
                 * 
                 * --Kris
                 */
                Subreddit funny = reddit.Subreddit("funny");
                Subreddit worldnews = reddit.Subreddit("worldnews");

                // Before monitoring, let's grab the posts once so we have a point of comparison when identifying new posts that come in.  --Kris
                funny.Posts.GetNew();
                worldnews.Posts.GetNew();

                Console.WriteLine("Monitoring funny for new posts....");

                funny.Posts.NewUpdated += C_NewPostsUpdated;
                funny.Posts.MonitorNew();  // Toggle on.

                Console.WriteLine("Monitoring worldnews for new posts....");

                worldnews.Posts.NewUpdated += C_NewPostsUpdated;
                worldnews.Posts.MonitorNew(10000);  // Toggle on with a custom delay of 10 seconds between each monitoring query.

                DateTime start = DateTime.Now;
                while (start.AddMinutes(1) > DateTime.Now) { }

                // Stop monitoring new posts.  --Kris
                sub.Posts.MonitorNew();  // Toggle off.
                sub.Posts.NewUpdated -= C_NewPostsUpdated;

                funny.Posts.MonitorNew();  // Toggle off.
                funny.Posts.NewUpdated -= C_NewPostsUpdated;

                worldnews.Posts.MonitorNew();  // Toggle off.
                worldnews.Posts.NewUpdated -= C_NewPostsUpdated;

                Console.WriteLine("Done monitoring!");

                // Grab today's top post in AskReddit and monitor its new comments.  --Kris
                Post post = sub.Posts.GetTop("day")[0];
                post.Comments.GetNew();

                Console.WriteLine("Monitoring today's top post on AskReddit....");

                post.Comments.MonitorNew();  // Toggle on.
                post.Comments.NewUpdated += C_NewCommentsUpdated;

                start = DateTime.Now;
                while (start.AddMinutes(1) > DateTime.Now) { }

                post.Comments.MonitorNew();  // Toggle off.
                post.Comments.NewUpdated -= C_NewCommentsUpdated;

                Console.WriteLine("Done monitoring!");

                // Now let's monitor r/all for a bit.  --Kris
                Subreddit all = reddit.Subreddit("all");
                all.Posts.GetNew();

                Console.WriteLine("Monitoring r/all for new posts....");

                all.Posts.MonitorNew();  // Toggle on.
                all.Posts.NewUpdated += C_NewPostsUpdated;

                start = DateTime.Now;
                while (start.AddMinutes(1) > DateTime.Now) { }

                all.Posts.MonitorNew();  // Toggle off.
                all.Posts.NewUpdated -= C_NewPostsUpdated;

                Console.WriteLine("Done monitoring!");
            }
        }


        /// <summary>
        /// Custom event handler for handling monitored new posts as they come in.
        /// See Reddit.NETTests for more complex examples.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void C_NewPostsUpdated(object sender, PostsUpdateEventArgs e)
        {
            foreach (Post post in e.Added)
            {
                Console.WriteLine("[" + post.Subreddit + "] New Post by " + post.Author + ": " + post.Title);
            }
        }

        /// <summary>
        /// Custom event handler for handling monitored new comments as they come in.
        /// See Reddit.NETTests for more complex examples.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void C_NewCommentsUpdated(object sender, CommentsUpdateEventArgs e)
        {
            foreach (Comment comment in e.Added)
            {
                Console.WriteLine("[" + comment.Subreddit + "/" + comment.Root.Title + "] New Comment by " + comment.Author + ": " + comment.Body);
            }
        }
    


    public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
