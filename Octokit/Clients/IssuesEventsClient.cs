﻿#if NET_45
using System.Threading.Tasks;
using System.Collections.Generic;
#endif

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's Issue Events API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/issues/events/">Issue Events API documentation</a> for more information.
    /// </remarks>
    public class IssuesEventsClient : ApiClient, IIssuesEventsClient
    {
        public IssuesEventsClient(IApiConnection apiConnection) : base(apiConnection)
        {
        }

        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns>A <see cref="IReadOnlyList{EventInfo}"/> of <see cref="EventInfo"/>s representing event information for specified number.</returns>
        public Task<IReadOnlyList<EventInfo>> GetAllForIssue(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAllForIssue(owner, name, number, ApiOptions.None);
        }

        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="number">The issue number</param>
        /// <returns>A <see cref="IReadOnlyList{EventInfo}"/> of <see cref="EventInfo"/>s representing event information for specified number.</returns>
        public Task<IReadOnlyList<EventInfo>> GetAllForIssue(int repositoryId, int number)
        {
            return GetAllForIssue(repositoryId, number, ApiOptions.None);
        }

        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A <see cref="IReadOnlyList{EventInfo}"/> of <see cref="EventInfo"/>s representing event information for specified number.</returns>
        public Task<IReadOnlyList<EventInfo>> GetAllForIssue(string owner, string name, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<EventInfo>(ApiUrls.IssuesEvents(owner, name, number), options);
        }

        /// <summary>
        /// Gets all events for the issue.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-an-issue
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="number">The issue number</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A <see cref="IReadOnlyList{EventInfo}"/> of <see cref="EventInfo"/>s representing event information for specified number.</returns>
        public Task<IReadOnlyList<EventInfo>> GetAllForIssue(int repositoryId, int number, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<EventInfo>(ApiUrls.IssuesEvents(repositoryId, number), options);
        }

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <returns>A <see cref="IReadOnlyList{IssueEvent}"/> of <see cref="IssueEvent"/>s representing issue events for specified repository.</returns>
        public Task<IReadOnlyList<IssueEvent>> GetAllForRepository(string owner, string name)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return GetAllForRepository(owner, name, ApiOptions.None);
        }

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <returns>A <see cref="IReadOnlyList{IssueEvent}"/> of <see cref="IssueEvent"/>s representing issue events for specified repository.</returns>
        public Task<IReadOnlyList<IssueEvent>> GetAllForRepository(int repositoryId)
        {
            return GetAllForRepository(repositoryId, ApiOptions.None);
        }

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A <see cref="IReadOnlyList{IssueEvent}"/> of <see cref="IssueEvent"/>s representing issue events for specified repository.</returns>
        public Task<IReadOnlyList<IssueEvent>> GetAllForRepository(string owner, string name, ApiOptions options)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<IssueEvent>(ApiUrls.IssuesEvents(owner, name), options);
        }

        /// <summary>
        /// Gets all events for the repository.
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#list-events-for-a-repository
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="options">Options for changing the API response</param>
        /// <returns>A <see cref="IReadOnlyList{IssueEvent}"/> of <see cref="IssueEvent"/>s representing issue events for specified repository.</returns>
        public Task<IReadOnlyList<IssueEvent>> GetAllForRepository(int repositoryId, ApiOptions options)
        {
            Ensure.ArgumentNotNull(options, "options");

            return ApiConnection.GetAll<IssueEvent>(ApiUrls.IssuesEvents(repositoryId), options);
        }

        /// <summary>
        /// Gets a single event
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#get-a-single-event
        /// </remarks>
        /// <param name="owner">The owner of the repository</param>
        /// <param name="name">The name of the repository</param>
        /// <param name="number">The event id</param>
        /// <returns>A <see cref="IssueEvent"/> representing issue event for specified number.</returns>
        public Task<IssueEvent> Get(string owner, string name, int number)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");

            return ApiConnection.Get<IssueEvent>(ApiUrls.IssuesEvent(owner, name, number));
        }

        /// <summary>
        /// Gets a single event
        /// </summary>
        /// <remarks>
        /// http://developer.github.com/v3/issues/events/#get-a-single-event
        /// </remarks>
        /// <param name="repositoryId">The ID of the repository</param>
        /// <param name="number">The event id</param>
        /// <returns>A <see cref="IssueEvent"/> representing issue event for specified number.</returns>
        public Task<IssueEvent> Get(int repositoryId, int number)
        {
            return ApiConnection.Get<IssueEvent>(ApiUrls.IssuesEvent(repositoryId, number));
        }
    }
}