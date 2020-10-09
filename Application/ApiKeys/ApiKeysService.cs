using Application.Common.Abstracts;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ApiKeys
{
    public class ApiKeysService : ServiceBase
    {
        private readonly ITollBoothDBContext _context;

        public ApiKeysService(ITollBoothDBContext context)
        {
            _context = context;
        }

        public async Task<ApiKey> FindApiKeyByKey(string key)
        {
            return await _context.ApiKeys.SingleOrDefaultAsync(x => x.Key == key);
        }

    }
}
