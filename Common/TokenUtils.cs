using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

using Newtonsoft.Json;

namespace Common
{
    public class GoogleApiTokenInfo
    {
        public string email { get; set; }
    }

    public class TokenUtils
    {
        private const string GoogleApiTokenInfoUrl = "https://www.googleapis.com/oauth2/v3/tokeninfo?access_token={0}";

        // Taken from https://stackoverflow.com/questions/39061310/validate-google-id-token.
        // Also referenced https://www.googleapis.com/oauth2/v3/tokeninfo.
        public static string validAuthHeader(string authHeader)
        {
            if (authHeader == null)
            {
                return "TestUser";
            }

            if (!authHeader.StartsWith("Bearer "))
            {
                throw new Exception("Got invalid auth token type! Expect Bearer token.");
            }

            string token = authHeader.Substring("Bearer ".Length).Trim();

            var httpClient = new HttpClient();
            var requestUri = new Uri(string.Format(GoogleApiTokenInfoUrl, token));

            HttpResponseMessage httpResponseMessage;
            httpResponseMessage = httpClient.GetAsync(requestUri).Result;
            
            Console.WriteLine(httpResponseMessage);

            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"Token validation failed! Received code {httpResponseMessage.StatusCode}");
            }

            var response = httpResponseMessage.Content.ReadAsStringAsync().Result;

            //Console.WriteLine(response);

            return JsonConvert.DeserializeObject<GoogleApiTokenInfo>(response).email;
        }
    }
}
