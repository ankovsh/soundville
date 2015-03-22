﻿using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Soundville.Infrastructure.Constants;
using VKSharp;
using VKSharp.Data.Api;

namespace Soundville.Infrastructure.SocialNetwork
{
    public static class VkHelper
    {
        [DataContract]
        private class TokenInfo
        {
            [DataMember]
            public string AccessToken { get; set; }

            [DataMember]
            public object ExpiresIn { get; set; }
        }

        public static async Task<VKApi> GetApi(string code)
        {
            var webClient = new HttpClient();
            HttpResponseMessage response = await webClient.GetAsync(
                "https://oauth.vk.com/access_token?client_id=" + AppSettings.VkClientId +
                "&client_secret=" + AppSettings.VkClientSecret + 
                "&code=" + code + 
                "&redirect_uri=" + AppSettings.SiteUrl + "Profile/Test/");

            string responseBody = await response.Content.ReadAsStringAsync();

            var ser = new DataContractJsonSerializer(typeof(TokenInfo));
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(responseBody));
            var tokenInfo = (TokenInfo)ser.ReadObject(stream);

            var api = new VKApi();

            api.AddToken(new VKToken(tokenInfo.AccessToken));

            return api;
        }

    }
}
