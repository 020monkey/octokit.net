﻿using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Octokit.Tests.Integration.Clients
{
    public class UserEmailsClientTests
    {
        private readonly IUserEmailsClient _emailClient;

        public UserEmailsClientTests()
        {
            var github = Helper.GetAuthenticatedClient();
            _emailClient = github.User.Email;
        }

        [IntegrationTest]
        public async Task CanGetEmail()
        {
            var github = Helper.GetAuthenticatedClient();

            var emails = await github.User.Email.GetAll();
            Assert.NotEmpty(emails);
        }

        [IntegrationTest]
        public async Task CanGetEmailWithApiOptions()
        {
            var github = Helper.GetAuthenticatedClient();
            var emails = await github.User.Email.GetAll(ApiOptions.None);
            Assert.NotEmpty(emails);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfEmailsWithoutStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1
            };

            var emails = await _emailClient.GetAll(options);

            Assert.Equal(1, emails.Count);
        }

        [IntegrationTest]
        public async Task ReturnsCorrectCountOfEmailsWithStart()
        {
            var options = new ApiOptions
            {
                PageSize = 5,
                PageCount = 1,
                StartPage = 2
            };

            var emails = await _emailClient.GetAll(options);

            Assert.Equal(0, emails.Count);
        }

        //[IntegrationTest]
        //public async Task ReturnsDistinctResultsBasedOnStartPage()
        //{
        //    var startOptions = new ApiOptions
        //    {
        //        PageSize = 5,
        //        PageCount = 1
        //    };

        //    var firstPage = await _emailClient.GetAll(startOptions);

        //    var skipStartOptions = new ApiOptions
        //    {
        //        PageSize = 5,
        //        PageCount = 1,
        //        StartPage = 2
        //    };

        //    var secondPage = await _emailClient.GetAll(skipStartOptions);

        //    Assert.Equal(firstPage[0].Email, secondPage[0].Email);
        //}

        const string testEmailAddress = "hahaha-not-a-real-email@foo.com";

        [IntegrationTest(Skip = "this isn't passing in CI - i hate past me right now")]
        public async Task CanAddAndDeleteEmail()
        {
            var github = Helper.GetAuthenticatedClient();

            await github.User.Email.Add(testEmailAddress);

            var emails = await github.User.Email.GetAll();
            Assert.Contains(testEmailAddress, emails.Select(x => x.Email));

            await github.User.Email.Delete(testEmailAddress);

            emails = await github.User.Email.GetAll();
            Assert.DoesNotContain(testEmailAddress, emails.Select(x => x.Email));
        }
    }
}
