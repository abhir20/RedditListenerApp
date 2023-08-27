using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Reddit;
using Reddit.Things;
using RestSharp;

namespace RedditListenerApp.Models
{
    public class Account : Reddit.Models.Account
    {
        public Account(string appId, string appSecret, string refreshToken, string accessToken, ref RestClient restClient, string deviceId = null, string userAgent = null)
            : base(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId, userAgent) {

            appId = appId;

            appSecret = appSecret;
            refreshToken = refreshToken;
            


        }

        //Reddit.Models.Account account = new Reddit.Models.Account(appId,appSecret,accessToken,restClient,deviceId,userAgent);

        //account.Me();
        public User Me()
        {
            return JsonConvert.DeserializeObject<User>(ExecuteRequest("api/v1/me.json"));
        }


    }
}
