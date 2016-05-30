﻿using NSubstitute;
using Octokit;
using Octokit.Tests;
using System;
using System.Threading.Tasks;
using Xunit;

public class ReactionsClientTests
{
    public class CommitComments
    {
        public class TheGetAllMethod
        {
            [Fact]
            public async Task RequestsCorrectUrl()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReactionsClient(connection);

                client.CommitComments.GetAll("fake", "repo", 42);

                connection.Received().GetAll<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments/1/reactions"), "application/vnd.github.squirrel-girl-preview");
            }

            [Fact]
            public async Task EnsuresArgumentsNotNull()
            {
                var connection = Substitute.For<IApiConnection>();
                var client = new ReactionsClient(connection);

                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CommitComments.CreateReaction(null, "name", 1, new NewReaction(ReactionType.Heart)));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CommitComments.CreateReaction("", "name", 1, new NewReaction(ReactionType.Heart)));
                await Assert.ThrowsAsync<ArgumentNullException>(() => client.CommitComments.CreateReaction("owner", null, 1, new NewReaction(ReactionType.Heart)));
                await Assert.ThrowsAsync<ArgumentException>(() => client.CommitComments.CreateReaction("owner", "", 1, new NewReaction(ReactionType.Heart)));
            }
        }

        public class TheCreateMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                NewReaction newReaction = new NewReaction(ReactionType.Heart);

                var connection = Substitute.For<IApiConnection>();
                var client = new ReactionsClient(connection);

                client.CommitComments.CreateReaction("fake", "repo", 1, newReaction);

                connection.Received().Post<Reaction>(Arg.Is<Uri>(u => u.ToString() == "repos/fake/repo/comments/1/reactions"), Arg.Any<object>(), "application/vnd.github.squirrel-girl-preview");
            }
        }
    }
}

