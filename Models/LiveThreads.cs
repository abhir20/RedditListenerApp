using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reddit;
using RestSharp;

namespace RedditListenerApp.Models
{
    public class LiveThreads : Reddit.Models.LiveThreads
    {
        public LiveThreads(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null, string userAgent = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId, userAgent) { }


    }
}
