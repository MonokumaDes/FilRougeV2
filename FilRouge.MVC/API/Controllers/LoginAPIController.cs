using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using FilRouge.MVC.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace FilRouge.MVC.API.Controllers
{
    public class LoginAPIController : ApiController
    {
        private HttpClient _client;
        private string _token = "";
        // GET: api/LoginAPI
        public string Get()
        {
            return "saissir mot de passe et email";
        }

        // POST: api/LoginAPI
        public string Post(string username, string password)
        {
            /*var response = _client.PostAsFormAsync<object>(url, new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("username", username)
            }).Result;

            if (response != null)
            {
                return "token A";
            }
            else
            {
                //throw new System.Exception("Identifiant incorrecte");
            }*/
            return "token ????";
        }
        
    }
}
