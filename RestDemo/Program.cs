using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;

namespace RestDemo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            /*var client = new RestClient(@"https://api.pwnedpasswords.com/range/B1B37");
            var request = new RestRequest();
            var response = await client.GetAsync(request);
            var valori = response.Content.Split('\n');*/

            /*var client = new RestClient(@"https://api.isevenapi.xyz/api/iseven/{number}");
            var request = new RestRequest();
            request.AddParameter("number", 12, ParameterType.UrlSegment);
            var response = await client.GetAsync<IsEvenResponse>(request);
            Console.WriteLine(response.Ad);*/

            /*var client = new RestClient("https://classify.yurace.pro/api/encrypt");
            var request = new RestRequest();
            request.AddBody(new EncryptMessage
            {
                Data = "Super secret message",
                Key = "lkajlkashj"
            });
            var response = client.PostAsync(request);*/

            var options = new RestClientOptions("https://80.211.144.168/api/v1")
            {
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };

            var client = new RestClient(options)
            {
                Authenticator = new JwtAuthenticator("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYW1lIjoiVXRlbnRlMDEiLCJuYmYiOjE2Njk4OTE0MDksImV4cCI6MTY2OTk3NzgwOSwiaWF0IjoxNjY5ODkxNDA5fQ.23HCnstgcXUjoLITw5RODaUVy-zcU6yX9z3Jch0Si2w")
            };

            var request = new RestRequest("Test/Private");
            var response = await client.GetAsync(request);


            SHA1 sha1 = SHA1.Create();
            string hash = BitConverter.ToString(sha1.ComputeHash(Encoding.ASCII.GetBytes("qwerty"))).Replace("-","");
        }
    }

    public class EncryptMessage
    {
        public string Data { get; set; }
        public string Key { get; set; }
    }

    public class IsEvenResponse
    {
        public string Ad { get; set; }
        public bool IsEven { get; set; }
    }
}
