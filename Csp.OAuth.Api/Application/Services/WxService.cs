﻿using System.Net.Http;
using System.Threading.Tasks;
using Csp.Extensions;
using Csp.OAuth.Api.Models;

namespace Csp.OAuth.Api.Application.Services
{
    public class WxService : IWxService
    {
        private readonly string _remoteServiceBaseUrl;
        private readonly HttpClient _httpClient;

        public WxService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _remoteServiceBaseUrl = $"{API.Remote_Service_Base_Url}/api/v1/wx";
        }

        public async Task<User> GetLogin(string code, int tenantId, int webSiteId)
        {
            var uri = API.WeiXin.GetWxUserByCode(_remoteServiceBaseUrl, code);

            var response = await _httpClient.GetAsync(uri);

            var jsonString = await response.Content.ReadAsStringAsync();
            var login = jsonString.FromJson<dynamic>();

            var user = new User
            {
                Cell = "",
                ExternalLogin = new ExternalLogin
                {
                    Provide = "weixin",
                    OpenId = login.OpenId,
                    WebSiteId = webSiteId
                },
                NickName = login.NickName,
                HeadImgUrl = login.HeadImgUrl,
                Status = 1,
                TenantId = tenantId
            };

            return login;
        }
    }
}
