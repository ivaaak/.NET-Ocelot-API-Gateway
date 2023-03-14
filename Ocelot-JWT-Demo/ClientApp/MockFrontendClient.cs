using Newtonsoft.Json;

namespace ClientApp;

public class MockFrontendClient
{
    public static string GetJwtTokenFromAuthServer()
    {
        var client = new HttpClient();

        client.BaseAddress = new Uri("http://localhost:9001"); //AuthServer
        client.DefaultRequestHeaders.Clear();

        var authResultObject = client.GetAsync("/api/auth?username=AuthUsername&password=AuthPassword").Result;

        dynamic jwtObject = JsonConvert.DeserializeObject(authResultObject.Content.ReadAsStringAsync().Result);

        return jwtObject.access_token;
    }
}