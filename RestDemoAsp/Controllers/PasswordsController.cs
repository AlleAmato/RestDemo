using RestDemoAsp.ViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RestDemoAsp.Controllers
{
    [RoutePrefix("password")]
    public class PasswordsController : Controller
    {
        [Route("")]
        public ActionResult Check()
        {
            return View();
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> CheckResult(PasswordViewModel p)
        {
            SHA1 sha1 = SHA1.Create();
            string hash = BitConverter.ToString(sha1.ComputeHash(Encoding.ASCII.GetBytes(p.Password.Trim()))).Replace("-", "");

            using (var client = new RestClient("https://api.pwnedpasswords.com/range"))
            {
                var request = new RestRequest(hash.Substring(0, 5));
                var response = await client.GetAsync(request);

                var valori = response.Content.Split('\n');

                string valore = valori.FirstOrDefault(q => q.StartsWith(hash.Substring(5)));
                
                if(valore != null)
                {
                    return View((object)(valore.Split(':')[1]));
                }

                return View((object)"0");
            }
        }

    }
}