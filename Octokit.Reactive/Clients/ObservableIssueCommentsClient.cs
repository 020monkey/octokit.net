﻿using System;

namespace Octokit.Reactive.Clients
{
    class ObservableIssueCommentsClient : IObservableIssueCommentsClient
    {
        readonly IConnection _connection;

        public ObservableIssueCommentsClient(IGitHubClient client)
        {
            Ensure.ArgumentNotNull(client, "client");

            _connection = client.Connection;
        }
        /// <summary>
        /// Gets a single Issue Comment by number.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/comments/#get-a-single-comment
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue comment number</param>
        /// <returns>The <see cref="IssueComment"/>s for the specified Issue Comment.</returns>
        public IObservable<IssueComment> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _connection.Get<IssueComment>(ApiUrls.IssueComment(owner, name, number));
        }

        /// <summary>
        /// Gets a list of the Issue Comments in a specified repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/comments/#list-comments-in-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>The list of <see cref="IssueComment"/>s for the specified Repository.</returns>
        public IObservable<IReadOnlyList<IssueComment>> GetForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _connection.GetAll<IssueComment>(ApiUrls.IssueComments(owner, name));
        }

        /// <summary>
        /// Gets a list of the Issue Comments for a specified issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/comments/#list-comments-on-an-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns>The list of <see cref="IssueComment"/>s for the specified Issue.</returns>
        public IObservable<IReadOnlyList<IssueComment>> GetForIssue(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return _connection.GetAll<IssueComment>(ApiUrls.IssueComments(owner, name, number));
        }

        /// <summary>
        /// Creates a new Issue Comment in the specified Issue
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/comments/#create-a-comment
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="newComment">The text of the new comment</param>
        /// <returns>The <see cref="IssueComment"/>s for that was just created.</returns>
        public IObservable<IssueComment> Create(string owner, string name, int number, string newComment)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(newComment, "newComment");

            return _connection.Post<IssueComment>(ApiUrls.IssueComments(owner, name, number), newComment);
        }

        /// <summary>
        /// Updates a specified Issue Comment
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/comments/#edit-a-comment
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="commentUpdate">The text of the updated comment</param>
        /// <returns>The <see cref="IssueComment"/>s for that was just updated.</returns>
        public IObservable<IssueComment> Update(string owner, string name, int number, string commentUpdate)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(commentUpdate, "commentUpdate");

            return _connection.Patch<IssueComment>(ApiUrls.IssueComment(owner, name, number), commentUpdate);
        }
    }
}
