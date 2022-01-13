using Newtonsoft.Json;
using System;

namespace Identity.Authorization
{
    public class JwtToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_at")]
        public DateTime ExpiredAt { get; set; }
    }
}
