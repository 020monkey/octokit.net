﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Burr
{
    public interface IRepositoriesEndpoint
    {
        Task<List<Repository>> GetAllAsync();
    }

    public interface IAuthorizationsEndpoint
    {
        Task<List<Authorization>> GetAllAsync();
        Task<Authorization> GetAsync(int id);
        Task<Authorization> UpdateAsync(int id, AuthorizationUpdate auth);
        Task<Authorization> CreateAsync(AuthorizationUpdate auth);
        Task DeleteAsync(int id);
    }
}
