using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace APIAssignment
{
    [Binding]
    public sealed class APITestsStepDef
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;
        public string allUserData { get; set; }
        public HttpResponseMessage apiResponse {get;set;}

        public APITestsStepDef(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"Make a call to API to get all users")]
        public void MakeACallToAPIToGetAllUsers()
        {
            apiResponse = RestHelper.GetAllUsers();
            allUserData = apiResponse.Content.ReadAsStringAsync().Result;
        }

        [When(@"API Response is Received")]
        public void APIResponseIsReceived()
        {
            Assert.IsTrue(apiResponse.IsSuccessStatusCode);
        }
        
        [Given(@"Make a call to API to get specific user with id (.*)")]
        public void MakeACallToAPIToGetSpecificUserWithId(int userId)
        {
            apiResponse = RestHelper.GetSpecificUserData(userId);
            allUserData = apiResponse.Content.ReadAsStringAsync().Result;
        }

        [Then(@"Verify API Response has all the expected users data")]
        public void VerifyAPIResponseHasAllTheExpectedUsersData()
        {
            var expectedUserData = JsonConvert.DeserializeObject<UserGraph>(ResourceHelper.expectedAllUsersObject);
            var actualUserData = JsonConvert.DeserializeObject<UserGraph>(allUserData);
            //Verifying Page Object Data
            Assert.AreEqual(expectedUserData.page, actualUserData.page);
            Assert.AreEqual(expectedUserData.per_page, actualUserData.per_page);
            Assert.AreEqual(expectedUserData.total, actualUserData.total);
            Assert.AreEqual(expectedUserData.total_pages, actualUserData.total_pages);
            //Verifying Support Object Data...
            Assert.AreEqual(expectedUserData.support.url, actualUserData.support.url);
            Assert.AreEqual(expectedUserData.support.text, actualUserData.support.text);
            //Verifying User Object Data....
            foreach(var userData in expectedUserData.data)
            {
                var actUserData = actualUserData.data.First(a => a.id == userData.id);
                Assert.AreEqual(userData.email,actUserData.email);
                Assert.AreEqual(userData.first_name,actUserData.first_name);
                Assert.AreEqual(userData.last_name,actUserData.last_name);
                Assert.AreEqual(userData.avatar,actUserData.avatar);
            }
        }

        [Then(@"Verify API Response has following users data")]
        public void VerifyAPIResponseHasFollowingUsersData(Table userData)
        {
            var expectedUserData = userData.CreateInstance<UserData>();
            var expectedSupportData = userData.CreateInstance<Support>();
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            //Verifying User Object Data
            var actualUserData = JsonConvert.DeserializeObject<SpecificUserData>(allUserData, jsonSerializerSettings).data;
            var actualSupportData = JsonConvert.DeserializeObject<SpecificUserData>(allUserData, jsonSerializerSettings).support;
            Assert.AreEqual(expectedUserData.id, actualUserData.id);
            Assert.AreEqual(expectedUserData.email, actualUserData.email);
            Assert.AreEqual(expectedUserData.first_name, actualUserData.first_name);
            Assert.AreEqual(expectedUserData.last_name, actualUserData.last_name);
            Assert.AreEqual(expectedUserData.avatar, actualUserData.avatar);
            //Verifying Support Object Data...
            Assert.AreEqual(expectedSupportData.url, actualSupportData.url);
            Assert.AreEqual(expectedSupportData.text, actualSupportData.text);
        }

        [Then(@"Verify API Response throws (.*) Not Found")]
        public void ThenVerifyAPIResponseThrows(int expectedStatusCode)
        {
            Assert.IsTrue((int)apiResponse.StatusCode == expectedStatusCode);
        }

    }
}
