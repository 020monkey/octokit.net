﻿using System;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    /// <summary>
    /// Client tests mostly just need to make sure they call the IApiConnection with the correct 
    /// relative Uri. No need to fake up the response. All *those* tests are in ApiConnectionTests.cs.
    /// </summary>
    public class TeamsClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void EnsuresNonNullArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new TeamsClient(null));
            }
        }

        public class TheGetMethod
        {
            [Fact]
            public void RequestsTheCorrectlUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.Get(1);

                connection.Received().Get<Team>(Arg.Is<Uri>(u => u.ToString() == "teams/1"), null);
            }
        }

        public class TheGetAllMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.GetAll("orgName");

                connection.Received().GetAll<Team>(Arg.Is<Uri>(u => u.ToString() == "orgs/orgName/teams"));
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var teams = new TeamsClient(Substitute.For<IApiConnection>());

                await Assert.ThrowsAsync<ArgumentNullException>(() => teams.GetAll(null));
            }
        }

        public class TheGetMembersMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.GetAllMembers(1);

                connection.Received().GetAll<User>(Arg.Is<Uri>(u => u.ToString() == "teams/1/members"));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                var team = new NewTeam("Octokittens");

                client.Create("orgName", team);

                connection.Received().Post<Team>(Arg.Is<Uri>(u => u.ToString() == "orgs/orgName/teams"), team);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                var team = new NewTeam("superstars");

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create(null, team));
                await Assert.ThrowsAsync<ArgumentException>(() => client.Create("", team));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Create("name", null));
            }
        }

        public class TheUpdateTeamMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                var team = new UpdateTeam("Octokittens");

                client.Update(1, team);

                connection.Received().Patch<Team>(Arg.Is<Uri>(u => u.ToString() == "teams/1"), team);
            }

            [Fact]
            public async Task EnsuresNonNullArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.Update(1, null));
            }
        }

        public class TheDeleteTeamMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                client.Delete(1);

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "teams/1"));
            }
        }

        public class TheAddMemberMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                client.AddMember(1, "user");

                connection.Received().Put(Arg.Is<Uri>(u => u.ToString() == "teams/1/memberships/user"));
            }

            [Fact]
            public void EnsuresNonNullOrEmptyLogin()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                AssertEx.Throws<ArgumentNullException>(() => client.AddMember(1, null));
                AssertEx.Throws<ArgumentException>(() => client.AddMember(1, ""));
            }
        }

        public class TheIsMemberMethod
        {
            [Fact]
            public void EnsuresNonNullLogin()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                AssertEx.Throws<ArgumentNullException>(() => client.IsMember(1, null));
            }

            [Fact]
            public void EnsuresNonEmptyLogin()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                AssertEx.Throws<ArgumentException>(() => client.IsMember(1, ""));
            }
        }

        public class TheRemoveMemberMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                client.RemoveMember(1, "user");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "teams/1/memberships/user"));
            }

            [Fact]
            public void EnsuresNonNullOrEmptyLogin()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                AssertEx.Throws<ArgumentNullException>(() => client.RemoveMember(1, null));
                AssertEx.Throws<ArgumentException>(() => client.RemoveMember(1, ""));
            }

        }

        public class TheGetRepositoriesMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                client.GetAllRepositories(1);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "teams/1/repos"));

                client.GetRepositories(1);

                connection.Received().GetAll<Repository>(Arg.Is<Uri>(u => u.ToString() == "teams/1/repos"));
            }
        }

        public class TheRemoveRepositoryMethod
        {
            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                client.RemoveRepository(1, "org", "repo");

                connection.Received().Delete(Arg.Is<Uri>(u => u.ToString() == "teams/1/repos/org/repo"));
            }
        }

        public class TheAddRepositoryMethod
        {
            [Fact]
            public async Task EnsuresNonNullOrEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                // Check owner arguments.
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveRepository(1, null, "repoName"));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveRepository(1, "", "repoName"));

                // Check repo arguments.
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.RemoveRepository(1, "ownerName", null));
                await Assert.ThrowsAsync<ArgumentException>(() => client.RemoveRepository(1, "ownerName", ""));
            }


            [Fact]
            public void RequestsTheCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);
                client.AddRepository(1, "org", "repo");

                connection.Received().Put(Arg.Is<Uri>(u => u.ToString() == "teams/1/repos/org/repo"));
            }

            [Fact]
            public void EnsureNonNullOrg()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                AssertEx.Throws<ArgumentException>(() => client.AddRepository(1, null, "Repo Name"));
            }
        }

        public class TheIsRepositoryManagedByTeamMethod
        {
            [Fact]
            public void EnsuresNonNullOrEmptyArguments()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new TeamsClient(connection);

                AssertEx.Throws<ArgumentException>(() => client.AddRepository(1, "org name", null));

                // Check owner arguments.
                AssertEx.Throws<ArgumentNullException>(() => client.IsRepositoryManagedByTeam(1, null, "repoName"));
                AssertEx.Throws<ArgumentException>(() => client.IsRepositoryManagedByTeam(1, "", "repoName"));

                // Check repo arguments.
                AssertEx.Throws<ArgumentNullException>(() => client.IsRepositoryManagedByTeam(1, "ownerName", null));
                AssertEx.Throws<ArgumentException>(() => client.IsRepositoryManagedByTeam(1, "ownerName", ""));
            }
        }
    }
}
