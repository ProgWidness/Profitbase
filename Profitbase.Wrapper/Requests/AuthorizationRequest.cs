﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Profitbase.Wrapper.Parsers;

namespace Profitbase.Wrapper.Requests
{
    internal class AuthorizationRequest
    {
        private readonly IClient _client;
        public AuthorizationRequest(IClient client)
        {
            _client = client;
        }

        public async Task Execute(string login, string password)
        {
            var csrfToken = await GetCsrfToken();

            var parameters = new Dictionary<string, string>
            {
                {"_csrf_token", csrfToken},
                {"_username", login },
                {"_password", password },
                {"_remember_me", "on" }
            };

            var answer = await _client.ExecutePostRequest(Routes.LoginRequest, parameters);
            return;
        }

        private async Task<string> GetCsrfToken()
        {
            var content = await _client.ExecuteGetRequest(Routes.LoginPage);
            return new LoginPageToCsrfTokenParser().GetToken(content);
        }

    }
}
