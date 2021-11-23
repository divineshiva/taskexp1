using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
namespace APIAssignment
{
    public static class RestHelper
    {
        private static IConfigurationRoot config = new ConfigurationBuilder()
        .AddJsonFile("testsettings.json")
        .Build();
        public static HttpResponseMessage GetAllUsers()
        {
            HttpClient httpClient = new HttpClient();
            var uri = new Uri(config["TestAPIUrl"]) + $"/users";
            try
            {
                var response = httpClient.GetAsync(uri).Result;
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while getting resource (uri: {uri}).", ex);
            }
        }
        public static HttpResponseMessage GetSpecificUserData(int userId)
        {
            HttpClient httpClient = new HttpClient();
            var uri = new Uri(config["TestAPIUrl"]) + $"/users/{userId}";
            try
            {
                var response = httpClient.GetAsync(uri).Result;
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while getting resource (uri: {uri}).", ex);
            }
        }


    }
}
