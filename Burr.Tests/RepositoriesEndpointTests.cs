using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Burr.Http;
using FluentAssertions;
using Moq;
using Xunit;

namespace Burr.Tests
{
    public class RepositoriesEndpointTests
    {
        static readonly Func<Task<IResponse<List<Repository>>>> fakeRepositoriesResponse =
            () => Task.FromResult<IResponse<List<Repository>>>(
                new Response<List<Repository>>
                {
                    BodyAsObject = new List<Repository> { new Repository() }
                });

        public class TheConstructor
        {
            [Fact]
            public void ThrowsForBadArgs()
            {
                Assert.Throws<ArgumentNullException>(() => new RepositoriesEndpoint(null));
            }
        }

        public class TheGetAllAsyncMethod
        {
            [Fact]
            public async Task GetsAListOfRepos()
            {
                const string endpoint = "/repos";
                var c = new Mock<IConnection>();
                c.Setup(x => x.GetAsync<List<Repository>>(endpoint)).Returns(fakeRepositoriesResponse);
                var client = new GitHubClient
                {
                    Login = "tclem",
                    Password = "pwd",
                    Connection = c.Object
                };

                var repos = await client.Repository.GetAllAsync();

                repos.Should().NotBeNull();
                repos.Items.Count.Should().Be(1);
                c.Verify(x => x.GetAsync<List<Repository>>(endpoint));
            }
        }
    }
}