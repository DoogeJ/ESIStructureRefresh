using System;
using System.Text;
using System.Net;
using Newtonsoft.Json;

public static class OAuth2
{
    public static RefreshTokenRequestResult GetAccessToken(string AuthenticationToken, string RefreshToken)
    {
        Console.Write("Requesting a new access token... ");
        RefreshTokenRequestResult refreshTokenRequestResult = new RefreshTokenRequestResult();

        using (WebClient wc = new WebClient())
        {
            string url = "https://login.eveonline.com/oauth/token";

            wc.Headers[HttpRequestHeader.Authorization] = "Basic " + AuthenticationToken;

            var reqparm = new System.Collections.Specialized.NameValueCollection();
            reqparm.Add("grant_type", "refresh_token");
            reqparm.Add("refresh_token", RefreshToken);

            refreshTokenRequestResult = JsonConvert.DeserializeObject<RefreshTokenRequestResult>(Encoding.UTF8.GetString(wc.UploadValues(url, "POST", reqparm)));
        }
        Console.WriteLine("succes!");
        return refreshTokenRequestResult;
    }
}
