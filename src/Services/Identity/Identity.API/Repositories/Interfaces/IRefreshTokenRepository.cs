﻿using Identity.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Repositories.Interfaces
{
    interface IRefreshTokenRepository
    {
        public Task Create(RefreshToken refreshToken);
        public Task Remove(Guid id);
        public Task RemoveAll(Guid userId);
        public Task<RefreshToken> GetByToken(string token);
    }
}