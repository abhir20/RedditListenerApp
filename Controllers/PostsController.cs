using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reddit;
using Reddit.Controllers;
using Reddit.Inputs.Subreddits;
using Reddit.Models;
using RestSharp;

namespace RedditListenerApp.Controllers
{
    public class PostsController : Controller
        {

        static string appId = "77Vhm2YGgk703wHWBmX9ig";
        static string refreshToken = "760730588485-UfbKjs0RFOSqsyz-_rEG3iHHtgBROg";
        static string accessToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6IlNIQTI1NjpzS3dsMnlsV0VtMjVmcXhwTU40cWY4MXE2OWFFdWFyMnpLMUdhVGxjdWNZIiwidHlwIjoiSldUIn0.eyJzdWIiOiJ1c2VyIiwiZXhwIjoxNjkzMzM0NjA3Ljc0NDg0OSwiaWF0IjoxNjkzMjQ4MjA3Ljc0NDg0OSwianRpIjoiVGlWYklKNHIzZnVKTkc1TlVHMmlUOUZsUlRjc0hRIiwiY2lkIjoiNzdWaG0yWUdnazcwM3dIV0JtWDlpZyIsImxpZCI6InQyXzlwaDNjbXJwIiwiYWlkIjoidDJfOXBoM2NtcnAiLCJsY2EiOjE2MTAwMzUzMTMwMDAsInNjcCI6ImVKeEVqa0Z1eFRBSVJPX0NPamVxdXNBRzU2UGFJUUtjS3JldnFQUGIzV2dHWnQ0SFZHTWlDWWNOaGxMVkkwektETFhIR1NqOUwydXl3d1ktaTFlVHdxbkRabzFwVEI1MzUzeTZOREw1bGk5aGtzam4yMmY1M19GWnh2S1Z1dTVMbk9wdmlOWlJMT193NHVWb3ZQZ1h5QmdKTmpoTkxnd2U3STQ3ci1CVXkwNGhQa0xpaGcyNlhEend3RDFic0ZhZHg3TWFocTFKZlNpZjBqZXNVcHBMT2ZjR0c3ekVReTA3Rjl2blR3QUFBUF9fT1NCc2h3IiwicmNpZCI6IkNkLUtqNG5POUdZcDBPOTBZd2dpS0tZQ0FzUk03Rkw0TURreUFBT3dXSGsiLCJmbG8iOjh9.FN9ySCDOpD47GHqmZ3a__VP5Y0KgGVZw7GdN4P0GfUT_rNPyx9CFa9MBueQfVNi - Nca6S8o0YArvdt0DvxigafQiS54RBTSQ - cF7kA2ux6a5WKdhrMtbPhqKDjU_lr37ja - ueTBnr3yaQbz5m32WpYz17IVgbTbSsbKpf3y_oD6FlHyMICt5xNidCB4ecg4PL3Hi9bt - suvDd_D4rKq0svqMd0 - 10E4XGYmB25flZmpWQciYwil8g5StXM55A6gzmQjZGSOE4aoDaMauUTZd7UKfCo5mevTUxCMHDcubvQ2q2F3mKyO7iCyYD1Yn2u8_r6lrgFC1coEFpPuZYEX - mg";


        // Initialize the API library instance.  
        RedditClient reddit = new RedditClient(appId: appId, refreshToken: refreshToken, accessToken: accessToken);


        public void GetTopPosts()
        {

            // Get info on the Reddit user authenticated by the OAuth credentials.  --Kris
            User me = reddit.Account.Me;

            Console.WriteLine("Username: " + me.Name);
            Console.WriteLine("Cake Day: " + me.Created.ToString("D"));


            //Top posts
            Subreddit funny = reddit.Subreddit("funny");

            // Before monitoring, let's grab the posts once so we have a point of comparison when identifying new posts that come in. 

            Console.WriteLine("Top Posts with the high UpVotes :" + funny.Posts.Top.Count);

        }
    }
}