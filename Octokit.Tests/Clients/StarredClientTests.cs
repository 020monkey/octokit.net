﻿using Octokit.Internal;
using System;
using System.Net;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using Xunit.Extensions;

namespace Octokit.Tests.Clients
{
    public class StarredClientTests
    {
        public class TheGetAllForCurrentMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("user/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                client.GetAllForCurrent();

                connection.Received().GetAll<Repository>(endpoint);
            }
        }

        public class TheGetAllForUserMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("users/banana/starred", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                client.GetAllForUser("banana");

                connection.Received().GetAll<Repository>(endpoint);
            }
        }

        public class TheGetAllStargazersForRepoMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var endpoint = new Uri("repos/fight/club/stargazers", UriKind.Relative);
                var connection = Substitute.For<IApiConnection>();
                var client = new StarredClient(connection);

                client.GetAllStargazers("fight", "club");

                connection.Received().GetAll<User>(endpoint);
            }
        }

        public class TheCheckStarredMethod
        {
            [Theory]
            [InlineData(HttpStatusCode.NoContent, true)]
            [InlineData(HttpStatusCode.NotFound, false)]
            public async Task RequestsCorrectValueForStatusCode(HttpStatusCode status, bool expected)
            {
                var response = Task.Factory.StartNew<IResponse<object>>(() =>
                    new ApiResponse<object> { StatusCode = status });
                var connection = Substitute.For<IConnection>();
                connection.GetAsync<object>(Arg.Is<Uri>(u => u.ToString() == "user/starred/yes/no"),
                    null, null).Returns(response);
                var apiConnection = Substitute.For<IApiConnection>();
                apiConnection.Connection.Returns(connection);
                var client = new StarredClient(apiConnection);

                var result = await client.CheckStarred("yes", "no");

                Assert.Equal(expected, result);
            }
        }
    }
}
