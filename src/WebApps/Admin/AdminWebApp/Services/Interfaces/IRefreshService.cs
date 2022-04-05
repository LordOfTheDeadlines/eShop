using Identity.Core.Requests;
using Identity.Core.Responses;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminWebApp.Services.Interfaces
{
    public interface IRefreshService
    {
        [Post("/refresh")]
        Task<AuthenticatedUserResponse> Refresh([Body] RefreshRequest refreshRequest);
    }
}
