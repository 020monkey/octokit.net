﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// A client for GitHub's repository deploy keys API.
    /// </summary>
    /// <remarks>
    /// See the <a href="https://developer.github.com/v3/repos/keys/">Repository deploy keys API documentation</a> for more information.
    /// </remarks>
    public class RepositoryDeployKeysClient : ApiClient, IRepositoryDeployKeysClient
    {
        /// <summary>
        /// Instantiates a new GitHub repository deploy keys API client.
        /// </summary>
        /// <param name="apiConnection">The API connection.</param>
        public RepositoryDeployKeysClient(IApiConnection apiConnection)
            : base(apiConnection)
        { 
        }

        /// <summary>
        /// Get a single deploy key by number for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="number">The id of the deploy key.</param>
        public Task<DeployKey> Get(string owner, string name, int number)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all deploy keys for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        public Task<IReadOnlyList<DeployKey>> GetForRepository(string owner, string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new deploy key for a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="newDeployKey">The deploy key to create for the repository.</param>
        /// <returns></returns>
        public Task<DeployKey> Create(string owner, string name, NewDeployKey newDeployKey)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deploy keys are immutable. If you need to update a key, remove the key and create a new one instead.
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="name"></param>
        /// <param name="number"></param>
        /// <param name="newDeployKey"></param>
        /// <returns></returns>
        /// Task<DeployKey> Update(string owner, string name, int number, NewDeployKey newDeployKey);
        /// <summary>
        /// Deletes a deploy key from a repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="number">The id of the deploy key to delete.</param>
        /// <returns></returns>
        public Task Delete(string owner, string name, int number)
        {
            throw new NotImplementedException();
        }
    }
}
