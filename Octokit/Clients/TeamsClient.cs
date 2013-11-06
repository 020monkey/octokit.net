﻿#if NET_45
using System.Collections.Generic;
#endif
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Octokit
{

    /// <summary>
    /// A client for GitHub's Org Teams API.
    /// </summary>
    /// <remarks>
    /// See the <a href="http://developer.github.com/v3/orgs/teams/">Orgs API documentation</a> for more information.
    /// </remarks>
    public class TeamsClient : ApiClient, ITeamsClient
    {
        /// <summary>
        /// Initializes a new GitHub Orgs Team API client.
        /// </summary>
        /// <param name="apiConnection">An API connection.</param>
        public TeamsClient(IApiConnection apiConnection)
            : base(apiConnection)
        {
        }

        /// <summary>
        /// Returns all <see cref="Team" />s for the current org.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>A list of the orgs's teams <see cref="Team"/>s.</returns>
        public Task<IReadOnlyList<Team>> GetAllTeams(string org)
        {
            Ensure.ArgumentNotNullOrEmptyString(org, "org");

            var endpoint = "orgs/{0}/teams".FormatUri(org);

            return ApiConnection.GetAll<Team>(endpoint);
        }


        /// <summary>
        /// Returns newly created <see cref="Team" /> for the current org.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Newly created <see cref="Team"/></returns>
        public Task<Team> CreateTeam(string org, NewTeam team)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Returns updated <see cref="Team" /> for the current org.
        /// </summary>
        /// <exception cref="ApiException">Thrown when a general API error occurs.</exception>
        /// <returns>Updated <see cref="Team"/></returns>
        public Task<Team> UpdateTeam(string org, int id, UpdateTeam team)
        {
            throw new System.NotImplementedException();
        }
    }
}
