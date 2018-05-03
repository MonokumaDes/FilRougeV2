using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using FilRouge.MVC.Entities;
using FilRouge.MVC.ViewModels;
using Newtonsoft.Json;

namespace FilRouge.MVC.API
{
    public class HttpFRClient
    {
        private string _baseUri;
        private HttpClient _client;
        private string _token = "";

        public bool IsConnected()
        {
            bool etat = false;
            if (_token == null)
                etat = false;

            if (_token.Length > 0)
            {
                etat = true;
            }
            return etat;
        }

        public class TokenResponse
        {
            [JsonProperty(PropertyName = "access_token")]
            public string AccessToken { get; set; }
        }

        public HttpFRClient(string baseUri)
        {
            _baseUri = baseUri;
            _client = new HttpClient();
        }


        public bool LogIn(string username, string password)
        {
            var url = $"{_baseUri}/token";
            var response = _client.PostAsFormAsync<TokenResponse>(url, new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("username", username)
            }).Result;

            if (response != null)
            {
                _token = response.AccessToken;
                return true;
            }
            else
            {
                //throw new System.Exception("Identifiant incorrecte");
                return false;
            }
        }

        #region Contact
        public List<ContactViewModel> GetAllContact()
        {
            return _client.GetAsync<List<ContactViewModel>>(_baseUri + "/api/contact/", _token).Result;
        }

        public ContactViewModel GetContact(int id)
        {
            return _client.GetAsync<ContactViewModel>(_baseUri + "/api/contact/" + id, _token).Result;
        }
        #endregion

        #region Quizz
        public List<QuizzViewModel> GetAllQuizz()
        {
            return _client.GetAsync<List<QuizzViewModel>>(_baseUri + "/api/quizz/", _token).Result;
        }

        public QuizzViewModel GetQuizz(int id)
        {
            return _client.GetAsync<QuizzViewModel>(_baseUri + "/api/quizz/" + id, _token).Result;
        }
        #endregion

    }
}