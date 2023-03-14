using ClientApp;

HttpClient client = new HttpClient();
client.DefaultRequestHeaders.Clear();
client.BaseAddress = new Uri("http://localhost:9000"); //gateway port

Console.WriteLine("\n>>>>>>>>>> OCELOT API GATEWAY - PORT 9000 <<<<<<<<<<<<");
// Wait for 5 seconds

// 1. without access_token will not access the service   and return 401 .
var resWithoutToken = client.GetAsync("/customers").Result;

Console.WriteLine($"Sending Request to /customers , without token.");
Console.WriteLine($"Result : {resWithoutToken.StatusCode}");


Console.WriteLine("\nPress any key to get a token");
Console.ReadLine();


//2. with access_token will access the service  and return result.
client.DefaultRequestHeaders.Clear();
Console.WriteLine("\nBegin Auth....");
var jwt = MockFrontendClient.GetJwtTokenFromAuthServer();
Console.WriteLine("End Auth....");
Console.WriteLine($"\nToken={jwt}");


Console.WriteLine("\nPress any key to call /Customers with the token");
Console.ReadLine();


client.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
var resWithToken = client.GetAsync("/customers").Result;

Console.WriteLine($"\nSend Request to /customers , with token.");
Console.WriteLine($"Result : {resWithToken.StatusCode}");
Console.WriteLine(resWithToken.Content.ReadAsStringAsync().Result);


Console.WriteLine("\nPress any key to call /Customers/1 with no auth");
Console.ReadLine();


//3. visit no auth service 
Console.WriteLine("\nNo Auth Service Here ");
client.DefaultRequestHeaders.Clear();
var res = client.GetAsync("/customers/1").Result;

Console.WriteLine($"Send Request to /customers/1");
Console.WriteLine($"Result : {res.StatusCode}");
Console.WriteLine(res.Content.ReadAsStringAsync().Result);


Console.WriteLine("\nPress any key to call the customers microservice without a token");
Console.ReadLine();


Console.WriteLine("\n>>>>>>>>>> CUSTOMERS MICROSERVICE - PORT 9002 <<<<<<<<<<<<");
HttpClient client2 = new HttpClient();
client2.DefaultRequestHeaders.Clear();
client2.BaseAddress = new Uri("http://localhost:9002"); //service

// 1. without access_token will not access the service   and return 401 .
var resServiceWithoutToken = client2.GetAsync("/api/customers").Result;

Console.WriteLine($"Sending Request to /api/customers , without token.");
Console.WriteLine($"Result : {resServiceWithoutToken.StatusCode}");


Console.WriteLine("\nPress any key to call the customers microservice with a bearer token");
Console.ReadLine();


//2. with access_token will access the service  and return result.
client2.DefaultRequestHeaders.Clear();
client2.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
var resServiceWithToken = client2.GetAsync("/api/customers").Result;

Console.WriteLine($"\nSend Request to /api/customers , with token.");
Console.WriteLine($"Result : {resServiceWithToken.StatusCode}");
Console.WriteLine(resServiceWithToken.Content.ReadAsStringAsync().Result);


Console.WriteLine("\nPress any key to call /api/customers/1");
Console.ReadLine();

//3. visit no auth service 
Console.WriteLine("\nNo Auth Service Here ");
client2.DefaultRequestHeaders.Clear();
var resService = client2.GetAsync("/api/customers/1").Result;

Console.WriteLine($"Send Request to /api/customers/1");
Console.WriteLine($"Result : {resService.StatusCode}");
Console.WriteLine(resService.Content.ReadAsStringAsync().Result);

Console.WriteLine("\nPress any key to call the Auth server's /api/demo with no token");
Console.ReadLine();

Console.WriteLine("\n>>>>>>>>>>>> AUTH SERVER - PORT 9001 <<<<<<<<<<<<<");

//4. without access_token will not access the Auth server    and return 401 
HttpClient client1 = new HttpClient();
client1.DefaultRequestHeaders.Clear();
client1.BaseAddress = new Uri("http://localhost:9001"); //AuthServer

var authServerResWithToken = client1.GetAsync("/api/demo").Result;

Console.WriteLine($"\nSend Request to /demo , without token.");
Console.WriteLine($"Result : {authServerResWithToken.StatusCode}");
Console.WriteLine(authServerResWithToken.Content.ReadAsStringAsync().Result);


Console.WriteLine("\nPress any key to call the Auth server's /api/demo with a bearer token");
Console.ReadLine();

//5. with access_token will access the Auth server and return result.
client.DefaultRequestHeaders.Clear();
client1.DefaultRequestHeaders.Add("Authorization", $"Bearer {jwt}");
var resAuthWithToken = client1.GetAsync("/api/demo").Result;

Console.WriteLine($"\nSend Request to /demo , with token.");
Console.WriteLine($"Result : {resAuthWithToken.StatusCode}");
Console.WriteLine(resAuthWithToken.Content.ReadAsStringAsync().Result);

Console.WriteLine("\nPress any key to call the Auth server's api/demo/1 endpoint - no auth needed");
Console.ReadLine();

//6. visit no auth service in Auth server
Console.WriteLine("\nNo Auth Service Here ");
client1.DefaultRequestHeaders.Clear();
var resAuth = client1.GetAsync("/api/demo/1").Result;

Console.WriteLine($"Send Request to /demo/1");
Console.WriteLine($"Result : {resAuth.StatusCode}");
Console.WriteLine(resAuth.Content.ReadAsStringAsync().Result);

Console.Read();

